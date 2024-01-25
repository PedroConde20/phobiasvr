using System.Collections;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float velocidad = 2.0f;


    void Update()
    {
        MoverNPC();
    }

    void MoverNPC()
    {
        // Mover hacia adelante
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }


}
