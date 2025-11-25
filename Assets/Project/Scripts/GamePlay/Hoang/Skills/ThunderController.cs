using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderController : MonoBehaviour
{
    [SerializeField]GameObject[] vfxs;
    float duration = 8;
    float durationTimer = 0;
    float timer = 0;
    float timeBetween2Thunder = 0.2f;
    int index = 0;

    private void Start()
    {
        foreach (var vfx in vfxs)
        {
            vfx.GetComponent<ParticleSystem>()?.Stop();
        }
    }
    public void Update()
    {
        timer += Time.deltaTime;
        durationTimer += Time.deltaTime;
        if (durationTimer >= duration)
        {
            return;
        }
        if (timer >= timeBetween2Thunder)
        {
            timer = 0;
            vfxs[index].GetComponent<ParticleSystem>()?.Play();
            if (index >= vfxs.Length-1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }
    }

}
