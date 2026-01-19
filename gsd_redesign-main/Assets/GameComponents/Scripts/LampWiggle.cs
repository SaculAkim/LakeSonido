using UnityEngine;

public class LampWiggle : MonoBehaviour
{
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        FanSpeedController.FanSpeedChanged += FanSpeedController_FanSpeedChanged;
    }

    private void FanSpeedController_FanSpeedChanged(object sender, float e)
    {
        anim.SetFloat("Strength", e);
    }
    private void OnDisable()
    {
        FanSpeedController.FanSpeedChanged -= FanSpeedController_FanSpeedChanged;
    }
}
