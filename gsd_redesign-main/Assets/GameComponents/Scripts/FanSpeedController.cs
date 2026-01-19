using System;
using UnityEngine;

public class FanSpeedController : MonoBehaviour
{
    [SerializeField] AnimationCurve easeInCurve;
    [Range(0,1)] public float currentFanSpeed;
    Animator fanAnimator;
    float fanSpeed;
    bool fanEnabled;
    float timer;
    bool fanPluggedIn;

    public static event EventHandler<float> FanSpeedChanged;


    void Start()
    {
        fanAnimator = GetComponent<Animator>();
        fanEnabled = false;
        fanPluggedIn = false;
        fanSpeed = 0;
    }

    private void Update()
    {
        if (fanEnabled && fanSpeed < 1) 
        {
            timer += Time.deltaTime;
            fanSpeed = easeInCurve.Evaluate(timer);
        }
        else if(!fanEnabled && fanSpeed > 0)
        {
            timer -= Time.deltaTime;
            fanSpeed -= (0.3f * Time.deltaTime);
        }
        fanSpeed = Mathf.Clamp01(fanSpeed);
        fanAnimator.SetFloat("Speed", fanSpeed);
        currentFanSpeed = fanSpeed;
        FanSpeedChanged?.Invoke(this, currentFanSpeed);
    }

    public void StartFan()
    {
        fanEnabled = true;
        fanPluggedIn = true;
    }

    public void StopFan()
    {
        fanEnabled = false;
    }

    public void ToggleFan()
    {
        if (fanPluggedIn)
            fanEnabled = !fanEnabled;
    }
}
