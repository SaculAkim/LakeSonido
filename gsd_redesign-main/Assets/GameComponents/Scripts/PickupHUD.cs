using System.Collections;
using UnityEngine;

public class PickupHUD : MonoBehaviour
{
    [SerializeField] GameObject pickupPrefab;

    public void PickupItemDelayed(int delay)
    {
        Invoke("PickupItem", delay / 1000f);
    }
    public void PickupItem()
    {
        GameObject newUIElement = Instantiate(pickupPrefab, transform);
        Destroy(newUIElement, 5f);
    }
}
