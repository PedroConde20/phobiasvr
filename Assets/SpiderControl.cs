using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderControl : MonoBehaviour
{
    public float speed = 5f;
    public float rotationChangeInterval = 2f; // Intervalo de tiempo para cambiar la rotaci�n
    public string boxTag = "Caja"; // Tag de la caja

    private Quaternion targetRotation;
    private float rotationTimer = 0f;
    private bool isMoving = true; // Indica si la ara�a est� en movimiento

    private Animation animation; // Referencia al componente Animation

    private GameObject dentroObject; // Objeto "Dentro" en la jerarqu�a

    void Start()
    {
        GenerateRandomRotation();
        animation = GetComponent<Animation>(); // Obtener el componente Animation

        dentroObject = GameObject.Find("Dentro"); // Encontrar el objeto "Dentro" en la jerarqu�a
    }

    void Update()
    {
        if (isMoving)
        {
            MoveForward();

            rotationTimer += Time.deltaTime;
            if (rotationTimer >= rotationChangeInterval)
            {
                GenerateRandomRotation();
                rotationTimer = 0f;
            }

            RotateModel();
        }
    }

    void MoveForward()
    {
        Vector3 movement = transform.forward * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }

    void GenerateRandomRotation()
    {
        float randomYRotation = Random.Range(-90f, 90f); // Rango m�s limitado de rotaci�n
        targetRotation = Quaternion.Euler(0f, randomYRotation, 0f);
    }

    void RotateModel()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2f); // Suavizado de rotaci�n m�s lento
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(boxTag))
        {
            Debug.Log("Colisiono");
            isMoving = false; // Detener la ara�a cuando colisiona con la caja
            animation.Stop("run");
            animation.Play("idle"); // Activar la transici�n a la animaci�n "idle"

            // Establecer la posici�n relativa de la ara�a dentro del objeto "Dentro"
            transform.SetParent(dentroObject.transform);
            transform.localPosition = Vector3.zero;
        }
    }
}