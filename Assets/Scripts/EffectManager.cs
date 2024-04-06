using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    public GameObject effectBomb;

    private void Awake()
    {
        instance = this;
    }

    public void PlayEffectBomb(Transform pos)
    {
        GameObject newEffect = Instantiate(effectBomb, pos.position, Quaternion.identity);
        ParticleSystem par = newEffect.GetComponent<ParticleSystem>();
        Destroy(newEffect,par.main.duration);
    }
}
