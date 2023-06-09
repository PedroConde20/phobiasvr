using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrigeradorControlador : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundClip;
    public Transform playerTransform;
    public float maxDistance = 5f;
    public float minVolume = 0.2f;
    public float maxVolume = 0.6f;

    private bool isPlayerInRange;

    private void Start()
    {
        audioSource.clip = soundClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void Update()
    {
        if (playerTransform == null) return;

        float distance = Vector3.Distance(playerTransform.position, transform.position);
        isPlayerInRange = distance <= maxDistance;

        if (isPlayerInRange)
        {
            float t = Mathf.InverseLerp(0f, maxDistance, distance);
            float volume = Mathf.Lerp(maxVolume, minVolume, t);
            audioSource.volume = volume;
        }
        else
        {
            audioSource.volume = 0f;
        }
    }
}