using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCrates : MonoBehaviour
{
    [SerializeField] GameObject FullCrate;
    [SerializeField] GameObject BrokenCrate;

    private void Start()
    {
        FullCrate.SetActive(true);
        BrokenCrate.SetActive(false);
    }

    public void BreakCrate()
    {
        FullCrate.SetActive(false);
        BrokenCrate.SetActive(true);   
    }
}
