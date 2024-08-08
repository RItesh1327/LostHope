using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // The audio sources to use for background music
    public AudioSource bgmSource1;
    public AudioSource bgmSource2;

    // The audio source to use for sound effects
    public AudioSource sfxSource;

    // The audio clips to use for different actions
    public AudioClip clipPickup;
 
    public AudioClip ClipJet;

    // The maximum volume of the sound effects
    public float maxVolume = 1.0f;

    // A flag indicating whether the sound manager is currently enabled
    private bool isEnabled;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial state of the sound manager
        isEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {


        // If the sound manager is enabled...
        if (isEnabled)
        {
            // Define the minimum and maximum distances for the sound effects
            float minDistance = 1.0f;
            float maxDistance = 10.0f;


            // ... update the volume of the sound effects based on the distance to the player
            float distanceToPlayer = Vector3.Distance(sfxSource.transform.position, Camera.main.transform.position);
            float volume = Mathf.Lerp(maxVolume, 0.0f, Mathf.InverseLerp(minDistance, maxDistance, distanceToPlayer));
            sfxSource.volume = volume;
        }
    }

    // Function to play the pickup sound
    public void PlayPickupSound()
    {
        sfxSource.clip = clipPickup;
        sfxSource.Play();
    }
    public void PlayJetStartSound()
    {
        sfxSource.clip = ClipJet;
        sfxSource.Play();
    }

   

    // Function to play a background music track
    public void PlayBGM(AudioSource bgmSource, AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    // Function to stop a background music track
    public void StopBGM(AudioSource bgmSource)
    {
        bgmSource.Stop();
    }

    // Function to enable the sound manager
    public void Enable()
    {
        isEnabled = true;
    }

    // Function to disable the sound manager
    public void Disable()
    {
        isEnabled = false;
        sfxSource.Stop();
        bgmSource1.Stop();
    }
}

