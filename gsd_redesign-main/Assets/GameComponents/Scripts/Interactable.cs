using DG.Tweening;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening.Core;
using System;
using UnityEngine.Audio;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] GameObject KeyIcon;
    [SerializeField] GameObject BlockedIcon;
    [SerializeField] bool interactable;
    [SerializeField] bool onlyOnce;
    [SerializeField] AudioClip successAudioClip;
    [SerializeField] AudioClip failedAudioClip;
    [SerializeField] UnityEvent activateEvent;
    [SerializeField] UnityEvent cannotActivateEvent;


    List<Material> materialList;
    AudioSource audioSource;
    bool disabled = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();  
        materialList = new List<Material>();
        foreach(Renderer renderer in KeyIcon.GetComponentsInChildren<Renderer>())
        {
            materialList.Add(renderer.material);
            renderer.material.DOColor(new Color(1, 1, 1, 0), 0.1f);
        }
    }

    public void Disable()
    {
        interactable = false;
        BlockedIcon.SetActive(!interactable);
    }
    public void Enable()
    {
        interactable = true;
        BlockedIcon.SetActive(!interactable);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (disabled) return;
        if (other.tag == "Player")
        {
            BlockedIcon.SetActive(!interactable);
            foreach (Material mat in materialList)
            {
                mat.DOKill();
                mat.DOColor(new Color(1, 1, 1, 1), 1f);
            }

            InteractionBus.OnInteractionKeyPressed += InteractionBus_OnInteractionKeyPressed;

        }
    }

    private void InteractionBus_OnInteractionKeyPressed(object sender, EventArgs e)
    {
        if (disabled) return;
        if (interactable)
        {
            if(successAudioClip != null)
                audioSource.PlayOneShot(successAudioClip);
            activateEvent?.Invoke();
            if (onlyOnce)
            {
                foreach (Material mat in materialList)
                {
                    mat.DOKill();
                    mat.DOColor(new Color(1, 1, 1, 0), 0.5f);
                }
                DisableInteraction();
            }
        }
        else
        {
            if (failedAudioClip != null) 
                audioSource.PlayOneShot(failedAudioClip);
            cannotActivateEvent?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (disabled) return;
        if (other.tag == "Player")
        {
            foreach(Material mat in materialList)
            {
                mat.DOKill();
                mat.DOColor(new Color(1, 1, 1, 0), 0.5f);
            }
            InteractionBus.OnInteractionKeyPressed -= InteractionBus_OnInteractionKeyPressed;
        }
    }
    private void OnDisable()
    {
        InteractionBus.OnInteractionKeyPressed -= InteractionBus_OnInteractionKeyPressed;
    }

    public void DisableInteraction()
    {
        disabled = true;        
        InteractionBus.OnInteractionKeyPressed -= InteractionBus_OnInteractionKeyPressed;
    }

    public void EnableInteraction()
    {
        disabled = false;
    }

}
