using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] Animator LightSwitchAnimator;
    public void LightsOn()
    {
        LightSwitchAnimator.SetBool("LightsOn", !LightSwitchAnimator.GetBool("LightsOn"));
    }
}
