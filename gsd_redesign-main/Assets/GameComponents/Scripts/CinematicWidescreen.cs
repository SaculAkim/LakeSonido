using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class CinematicWidescreen : MonoBehaviour
{
    [SerializeField] List<GameObject> Bars;
        

    public void EnableCinematicBars()
    {
        Bars.ForEach(gameobject => gameobject.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0, 300), 2f));
    }

    public void EnableCinematicBars(float time)
    {
        Bars.ForEach(gameobject => gameobject.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0, 300), 2f));
        Invoke("DisableCinematicBars", time);
    }

    public void DisableCinematicBars()
    {
        Bars.ForEach(gameobject => gameobject.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0, 0), 2f));

    }
}
