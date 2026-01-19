using UnityEngine;

public class FanSoundPlayer : MonoBehaviour
{
    [SerializeField] AudioSource fanAudioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FanSpeedController.FanSpeedChanged += FanSpeedController_FanSpeedChanged;    
    }
    private void OnDisable()
    {
        FanSpeedController.FanSpeedChanged += FanSpeedController_FanSpeedChanged;
    }

    private void FanSpeedController_FanSpeedChanged(object sender, float FanSpeed01)
    {

        //TODO: Change this script do the sound dynamically scales with the variable FanSpeed01. 0 means the fan is off, 1 means the fan is spinning full speed;
        if(FanSpeed01 > 0)
        {
            fanAudioSource.Play();
        }
        else
        {
            fanAudioSource.Stop();
        }
    }

}
