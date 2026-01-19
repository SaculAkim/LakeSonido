using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using StarterAssets;
public class FootstepSounds : MonoBehaviour
{
    [SerializeField] List<FootstepSoundObject> footstepSoundClips;

    [Range(1f, 3f)]
    [SerializeField] float FootstepInterval;
    [SerializeField] AudioClip impactHitClip;

    private float playerVelocity;
    private CharacterController characterController;
    private FirstPersonController playerController;
    private AudioSource audioPlayer;
    private void Start()
    {
        playerController = GetComponentInParent<FirstPersonController>();
        characterController = GetComponentInParent<CharacterController>();
        audioPlayer = GetComponent<AudioSource>();
    }

    private void Update()
    {
        playerVelocity += characterController.velocity.magnitude * Time.deltaTime;

        if (playerVelocity > FootstepInterval)
        {
            if (playerController.Grounded)
            {
                float impactMagnitude = (playerVelocity - FootstepInterval);
                if (impactMagnitude > 3)
                {
                    audioPlayer.volume = 1;
                    if(impactHitClip != null) audioPlayer.PlayOneShot(impactHitClip, Mathf.Clamp01((impactMagnitude-3)/5f));
                }
                playerVelocity = 0;
                PlayNextSound();
            }
        }
    }

    void PlayNextSound()
    {
        GroundTexture currentGroundTexture;
        if (Physics.Raycast(transform.position + Vector3.up / 2f, -transform.up, out RaycastHit hitInfo, 1f))
        {

            if (hitInfo.collider.tag == "Terrain")
            {
                currentGroundTexture = (GroundTexture)TerrainSurface.GetMainTexture(transform.position);
            }
            else if (hitInfo.collider.tag == "Stone" || hitInfo.collider.tag == "Wood")
            {
                currentGroundTexture = (GroundTexture)Enum.Parse(typeof(GroundTexture), hitInfo.collider.tag);
            }
            else
                currentGroundTexture = GroundTexture.Unknown;


            List<FootstepSoundObject> usableFootsteps = footstepSoundClips.Where(x => x.clip != null && x.groundTexture == currentGroundTexture).ToList();
            if (usableFootsteps == null || usableFootsteps.Count == 0)
            {
                return;
            }
            FootstepSoundObject nextFootstep = usableFootsteps[Random.Range(0, usableFootsteps.Count)];
            audioPlayer.volume = nextFootstep.volume;
            audioPlayer.pitch = Random.Range(0.9f, 1.06f);
            audioPlayer.PlayOneShot(nextFootstep.clip);
        }
    }
}
