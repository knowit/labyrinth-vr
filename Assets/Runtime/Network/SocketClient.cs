﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Google.Protobuf;
using UnityEngine;

public class SocketConnection
{
    private readonly Socket _connection;
    private readonly ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[4096]);

    public Action OnClose { private get; set; }

    public bool Connected => _connection.Connected;

    public SocketConnection(Socket connection)
    {
        _connection = connection;
    }

    public void Close()
    {
        _connection.Disconnect(false);
    }

    public async Task<int> Send(GameUpdate data)
    {
        if (!Connected)
        {
            OnClose();
            return 0;
        }

        using (var ms = new MemoryStream())
        {
            data.WriteDelimitedTo(ms);

            var bytes = ms.ToArray();

            try
            {
                return await _connection.SendAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
            }
            catch(SocketException e)
            {
                Debug.LogError($"Socket error: {e.Message}");
                return 0;
            }
        }
    }

    public async Task<GameUpdate> Receive()
    {
        using (var ms = new MemoryStream())
        {
            while(true)
            {
                if (!Connected)
                {
                    OnClose();
                    return new GameUpdate { Event = GameEvent.Finish };
                }

                try
                {
                    var bytes = await _connection.ReceiveAsync(buffer, SocketFlags.None);
                    if (bytes == 0)
                        continue;

                    ms.Write(buffer.Array, 0, bytes);
                    ms.Position = 0;

                    try
                    {
                        return GameUpdate.Parser.ParseDelimitedFrom(ms);
                    }
                    catch (InvalidProtocolBufferException e)
                    {
                        Debug.LogError($"Failed to parse package {e.Message}");
                    }
                    await Task.Yield();
                } 
                catch (SocketException e)
                {
                    Debug.LogError($"Socket error: {e.Message}");
                    continue;
                }
            }
        }
    }
}

public class SocketClient
{
    public async Task<SocketConnection> Connect(string host, int port, int retries = 3)
    {
        var ipAddress = host == "localhost" ? IPAddress.Loopback : IPAddress.Parse(host);
        var remoteEp = new IPEndPoint(ipAddress, port);

        Debug.Log($"Connecting to {remoteEp.Address}:{remoteEp.Port}");

        var client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);


        while (retries-- > 0)
        {
            try
            {
                await client.ConnectAsync(remoteEp);
                break;
            }
            catch (SocketException e)
            {
                Debug.LogWarning($"Failed to connect {e.Message}");
                await Task.Yield();
            } 
        }

        if (client.Connected)
            return new SocketConnection(client);

        Debug.Log("Connection failed");
        return null;
    }
}

