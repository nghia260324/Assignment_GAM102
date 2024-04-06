using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Single Footstep")]
    public List<AudioClip> footsteps;
    [Header("Landing")]
    public AudioClip landing;

    public float volumeSfx;


    private void Awake()
    {
        instance = this;
    }
    public void PlaySFXFootsteps()
    {
        int index = Random.Range(0, footsteps.Count);
        PlaySFX(footsteps[index], volumeSfx);
    }
    public void PlaySFXLanding()
    {
        PlaySFX(landing, volumeSfx);
    }
    private void PlaySFX(AudioClip clip,float volume)
    {
        if (clip == null) return;

        GameObject sfx = new GameObject();
        sfx.transform.position = transform.position;
        sfx.name = clip.name;
        AudioSource source = sfx.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.Play();
        Destroy(sfx, clip.length);
    }
}
