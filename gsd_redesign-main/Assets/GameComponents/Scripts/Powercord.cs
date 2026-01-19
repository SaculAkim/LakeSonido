using NUnit.Framework;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Powercord : MonoBehaviour
{
    [SerializeField] GameObject Plug_Out;
    [SerializeField] GameObject Plug_In;
    [SerializeField] PlayableDirector DirectorAsset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Plug_Out.SetActive(true);
        Plug_In.SetActive(false);
    }
    public void PlugInCord()
    {
        Plug_Out.SetActive(false);
        Plug_In.SetActive(true);
        DirectorAsset.Play();
        GetComponent<Interactable>().Disable();
        Destroy(this);
    }

}
