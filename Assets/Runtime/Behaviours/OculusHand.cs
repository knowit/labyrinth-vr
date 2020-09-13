using UnityEngine;

public class OculusHand : MonoBehaviour
{
    public const string ANIM_LAYER_NAME_POINT = "Point Layer";
    public const string ANIM_LAYER_NAME_THUMB = "Thumb Layer";
    public const string ANIM_PARAM_NAME_FLEX = "Flex";
    public const string ANIM_PARAM_NAME_POSE = "Pose";
    public const float THRESH_COLLISION_FLEX = 0.9f;

    public const float INPUT_RATE_CHANGE = 20.0f;

    public const float TRIGGER_DEBOUNCE_TIME = 0.05f;
    public const float THUMB_DEBOUNCE_TIME = 0.15f;

    [SerializeField]
    private OVRInput.Controller m_controller = OVRInput.Controller.None;
    [SerializeField]
    private Animator m_animator = null;

    private int m_animLayerIndexThumb = -1;
    private int m_animLayerIndexPoint = -1;
    private int m_animParamIndexFlex = -1;
    private int m_animParamIndexPose = -1;

    private bool m_isPointing = false;
    private bool m_isGivingThumbsUp = false;
    private float m_pointBlend = 0.0f;
    private float m_thumbsUpBlend = 0.0f;

    private void Start()
    {
        // Get animator layer indices by name, for later use switching between hand visuals
        m_animLayerIndexPoint = m_animator.GetLayerIndex(ANIM_LAYER_NAME_POINT);
        m_animLayerIndexThumb = m_animator.GetLayerIndex(ANIM_LAYER_NAME_THUMB);
        m_animParamIndexFlex = Animator.StringToHash(ANIM_PARAM_NAME_FLEX);
        m_animParamIndexPose = Animator.StringToHash(ANIM_PARAM_NAME_POSE);
    }

    private void Update()
    {
        UpdateCapTouchStates();

        m_pointBlend = InputValueRateChange(m_isPointing, m_pointBlend);
        m_thumbsUpBlend = InputValueRateChange(m_isGivingThumbsUp, m_thumbsUpBlend);

        UpdateAnimStates();
    }

    // Just checking the state of the index and thumb cap touch sensors, but with a little bit of
    // debouncing.
    private void UpdateCapTouchStates()
    {
        m_isPointing = !OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, m_controller);
        m_isGivingThumbsUp = !OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, m_controller);
    }

    private float InputValueRateChange(bool isDown, float value)
    {
        float rateDelta = Time.deltaTime * INPUT_RATE_CHANGE;
        float sign = isDown ? 1.0f : -1.0f;
        return Mathf.Clamp01(value + rateDelta * sign);
    }

    private void UpdateAnimStates()
    {
        m_animator.SetInteger(m_animParamIndexPose, 0);

        // Flex
        // blend between open hand and fully closed fist
        float flex = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller);
        m_animator.SetFloat(m_animParamIndexFlex, flex);

        // Point
        m_animator.SetLayerWeight(m_animLayerIndexPoint, m_pointBlend);

        // Thumbs up
        m_animator.SetLayerWeight(m_animLayerIndexThumb, m_thumbsUpBlend);

        float pinch = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, m_controller);
        m_animator.SetFloat("Pinch", pinch);
    }
}
