using DG.Tweening;
using UnityEngine;

public class LocationHUD : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] RectTransform WiperTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();   
    }

    public void Show()
    {
        WiperTransform.DOSizeDelta(new Vector2(1100, 250), 8);
        Invoke("Hide", 8);
    }

    public void Hide()
    {
        rectTransform.DOSizeDelta(new Vector2(1000, 0), 3).SetEase(Ease.InOutCirc);
    }
}
