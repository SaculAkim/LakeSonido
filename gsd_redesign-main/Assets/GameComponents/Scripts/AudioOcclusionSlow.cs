using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioOcclusionSlow : MonoBehaviour
{
    [SerializeField] OcclusionData[] occlusionData;
    [SerializeField] AudioMixerSnapshot[] snapshotOutside;
    [SerializeField] float transitionTimeOut;
    [Range(0.1f, 1f), SerializeField] float checkInterval = 0.5f;


    float timer;
    bool occluded = false;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > checkInterval) {
            timer = 0;
            Vector3 cameraPosition = Camera.main.transform.position;
            OcclusionData chosenOcclusionData = null;
            foreach (var occlusionData in occlusionData)
            {
                foreach (Collider col in occlusionData.occlusionSpace)
                {
                    Vector3 closestPoint = col.ClosestPoint(cameraPosition);
                    Vector3 difference = cameraPosition - closestPoint;
                    difference.y = 0;
                    if (difference.magnitude < 0.2f)
                    {
                        if (chosenOcclusionData == null)
                        {
                            chosenOcclusionData = occlusionData;
                        }
                        else if (chosenOcclusionData.Priority < occlusionData.Priority)
                        {
                            chosenOcclusionData = occlusionData;
                        }
                    }
                }
            }
            if(chosenOcclusionData == null)
            {
                if (occluded) 
                    foreach(var snap in snapshotOutside)
                        snap.TransitionTo(transitionTimeOut);

                occluded = false;
                return;
            }
            if (!occluded) 
                foreach (var snap in chosenOcclusionData.snapshotInside)
                    snap.TransitionTo(chosenOcclusionData.transitionTime);
            occluded = true;
            
        }
    }
}


[Serializable]
public class OcclusionData
{
    [SerializeField] public Collider[] occlusionSpace;
    [SerializeField] public float transitionTime;
    [SerializeField] public AudioMixerSnapshot[] snapshotInside;
    [SerializeField] public int Priority;
}