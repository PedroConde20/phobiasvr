#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzScript : MonoBehaviour
{
    public Light light;
    public float minTime = 1f;
    public float maxTime = 5f;
    public AudioSource audioSource;
    public AudioClip soundClip;
    public Transform playerTransform;
    public float maxDistance = 10f;
    public float minVolume = 0.2f;
    public float maxVolume = 0.6f;
    public float flickerDuration = 0.1f;
    public int flickerCount = 5;

    private float timer;
    private bool isLightOn;

    private void Start()
    {
        timer = Random.Range(minTime, maxTime);
        isLightOn = true;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            StartCoroutine(ToggleLightWithFlicker());
            timer = Random.Range(minTime, maxTime);
        }

        UpdateSoundVolume();
    }

    private IEnumerator ToggleLightWithFlicker()
    {
        for (int i = 0; i < flickerCount; i++)
        {
            light.enabled = !light.enabled;
            yield return new WaitForSeconds(flickerDuration);
        }

        light.enabled = true;
        PlaySound();
    }

    private void PlaySound()
    {
        audioSource.PlayOneShot(soundClip);
    }

    private void UpdateSoundVolume()
    {
        if (playerTransform == null) return;

        float distance = Vector3.Distance(playerTransform.position, transform.position);
        float t = Mathf.InverseLerp(0f, maxDistance, distance);
        float volume = Mathf.Lerp(maxVolume, minVolume, t);
        audioSource.volume = volume;
    }
}
#endif