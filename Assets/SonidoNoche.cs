using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoNoche : MonoBehaviour
{
    public Transform playerTransform;
    public AudioSource audioSource;
    public float maxDistance = 10f;
    public float minVolume = 0.2f;
    public float maxVolume = 0.6f;

    private void Update()
    {
        if (playerTransform == null) return;

        float distance = Vector3.Distance(playerTransform.position, transform.position);
        float t = Mathf.InverseLerp(0f, maxDistance, distance);
        float volume = Mathf.Lerp(maxVolume, minVolume, t);
        audioSource.volume = volume;
    }
}