using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvionCon : MonoBehaviour
{
    public float velocidadInicial = 5f; // Velocidad inicial de desplazamiento recto.
    public float velocidadHorizontal = 10f; // Velocidad horizontal después de 5 segundos.
    public Rigidbody rb;
    public bool volar = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Iniciar el movimiento recto.
        rb.velocity = transform.forward * velocidadInicial;
        // Establecer una función de retardo de 5 segundos para cambiar al vuelo horizontal.
        Invoke("ActivarVueloHorizontal", 5f);
    }

    void FixedUpdate()
    {
        if (volar)
        {
            // Cambiar al vuelo horizontal.
            rb.velocity = transform.forward * velocidadHorizontal;
        }
    }

    void ActivarVueloHorizontal()
    {
        volar = true;
    }

    void Awake()
    {
        volar = false; // Asegurarse de que esté inicializado como falso al principio.
    }
}