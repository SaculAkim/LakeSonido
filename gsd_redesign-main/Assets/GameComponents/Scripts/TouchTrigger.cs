using UnityEngine;
using UnityEngine.Events;

public class TouchTrigger : MonoBehaviour
{

    [SerializeField] UnityEvent OnTriggerEnterPass;
    [SerializeField] UnityEvent OnTriggerExitPass;
    [SerializeField] bool runOnce;

    int runCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!runOnce || (runOnce && runCount == 0))
            {
                OnTriggerEnterPass?.Invoke();
                runCount++;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!runOnce || (runOnce && runCount == 1))
            {
                OnTriggerExitPass?.Invoke();
                runCount++;
            }
        }
    }
}
