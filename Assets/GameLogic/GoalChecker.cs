using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalChecker : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<GameManager>().RestartGame();
    }       
}
