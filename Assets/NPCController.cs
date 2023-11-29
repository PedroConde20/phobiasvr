using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float velocidad = 2.0f;
    public float tiempo = 30f;

    void Start()
    {
        Invoke("DestruirNPC", tiempo);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    void DestruirNPC()
    {
        Destroy(gameObject);
    }
}