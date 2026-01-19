using System;
using UnityEngine;

[Serializable]
public class FootstepSoundObject
{
    public AudioClip clip;
    [Range(0,1)]
    public float volume = 1;
    public GroundTexture groundTexture;
}

public enum GroundTexture
{
    Grass,
    Dirt,
    Sand,
    Road,
    Water,
    Stone,
    Wood,
    Unknown
}
