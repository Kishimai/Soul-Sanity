using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    // List of audio clips to play in the background
    public List<AudioClip> backgroundClips;

    // List of audio clips for sound effects
    public List<AudioClip> soundEffects;

    // List of audio clips for narrator
    public List<AudioClip> narratorDialogue;

    // The audio source component for background music
    private AudioSource backgroundAudioSource;

    // The audio source component for sound effects
    private AudioSource sfxAudioSource;

    // The audio source component for Narrator sound
    private AudioSource narratorAudioSource;

    // The duration of fade in and fade out in seconds
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;

    // Current volume level for background music
    public float backgroundVolume = 1f;

    // Current volume level for sound effects
    public float sfxVolume = 1f;

    void Start()
    {
        // Create AudioSource components if not already present
        backgroundAudioSource = gameObject.AddComponent<AudioSource>();
        sfxAudioSource = gameObject.AddComponent<AudioSource>();
        narratorAudioSource = gameObject.AddComponent<AudioSource>();

        // Set the initial volume for background music
        backgroundAudioSource.volume = backgroundVolume;

        // Set the audio to loop for background music
        backgroundAudioSource.loop = true;

        // Start playing the first background sound
        PlayNextBackgroundSound();
    }

    // Coroutine for fading in the background audio
    IEnumerator FadeInBackground(float duration)
    {
        backgroundAudioSource.volume = 0f;
        backgroundAudioSource.Play();

        float timer = 0f;
        while (timer < duration)
        {
            backgroundAudioSource.volume = Mathf.Lerp(0f, backgroundVolume, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        backgroundAudioSource.volume = backgroundVolume;
    }

    // Coroutine for fading out the background audio
    IEnumerator FadeOutBackground(float duration)
    {
        float startVolume = backgroundAudioSource.volume;

        float timer = 0f;
        while (timer < duration)
        {
            backgroundAudioSource.volume = Mathf.Lerp(startVolume, 0f, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        backgroundAudioSource.volume = 0f;
        backgroundAudioSource.Stop();
    }

    // Coroutine for fading in the SFX audio
    IEnumerator FadeInSFX(float duration)
    {
        sfxAudioSource.volume = 0f;

        float timer = 0f;
        while (timer < duration)
        {
            sfxAudioSource.volume = Mathf.Lerp(0f, sfxVolume, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        sfxAudioSource.volume = sfxVolume;
    }

    // Play a sound effect by index
    public void PlaySoundEffect(int index)
    {
        if (index >= 0 && index < soundEffects.Count)
        {
            sfxAudioSource.PlayOneShot(soundEffects[index]);
        }
        else
        {
            Debug.LogWarning("Invalid sound effect index.");
        }
    }

    public void PlaySoundNarration(int index)
    {
        if (index >= 0 && index < soundEffects.Count)
        {
            narratorAudioSource.PlayOneShot(narratorDialogue[index]);
        }
        else
        {
            Debug.LogWarning("Invalid sound effect index.");
        }
    }

    // Play the next background sound in the list
    public void PlayNextBackgroundSound()
    {
        StartCoroutine(PlayNextBackgroundWithFade());
    }

    // Coroutine for playing next background sound with fade
    IEnumerator PlayNextBackgroundWithFade()
    {
        if (backgroundClips.Count == 0)
        {
            Debug.LogWarning("No background clips assigned.");
            yield break;
        }

        // Choose a random background clip
        int randomIndex = Random.Range(0, backgroundClips.Count);
        AudioClip nextClip = backgroundClips[randomIndex];

        // Fade out current clip
        if (backgroundAudioSource.isPlaying)
        {
            StartCoroutine(FadeOutBackground(fadeOutDuration));
            yield return new WaitForSeconds(fadeOutDuration);
        }

        // Assign new clip and fade in
        backgroundAudioSource.clip = nextClip;
        StartCoroutine(FadeInBackground(fadeInDuration));
    }

    // Set the volume level for background music
    public void SetBackgroundVolume(float volume)
    {
        backgroundVolume = Mathf.Clamp01(volume);
        backgroundAudioSource.volume = backgroundVolume;
    }

    // Set the volume level for sound effects
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }
}
