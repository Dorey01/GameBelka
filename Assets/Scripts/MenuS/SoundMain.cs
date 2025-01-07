using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMain : MonoBehaviour
{
    public AudioClip[] chorusFilter;
    public AudioSource chorusSource;

    // Update is called once per frame
    void Update()
    {
        chorusSource.clip = chorusFilter[0];
        chorusSource.Play();
    }
}
