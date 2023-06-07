using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArañaScript : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 120f;

    private Vector3 randomDirection;
    private bool isTurning = false;

    private void Start()
    {
        // Inicia la araña con una dirección aleatoria
        SetRandomDirection();
    }

    private void Update()
    {
        if (!isTurning)
        {
            // Mueve la araña hacia adelante en la dirección actual
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
        else
        {
            // Rotación suave hacia la dirección aleatoria
            Quaternion toRotation = Quaternion.LookRotation(randomDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si colisiona con otro objeto, cambia de dirección
        if (!isTurning && other.CompareTag("Colision"))
        {
            SetRandomDirection();
        }
    }

    private void SetRandomDirection()
    {
        // Genera una dirección aleatoria en el plano XZ (horizontal)
        randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        // Reinicia la rotación
        isTurning = true;
    }
}