using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirObjetive : MonoBehaviour
{
    bool sumaPuntos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Objetive"){

            FindObjectOfType<Puntaje>().SumarPuntos(10);
            Destroy(other.gameObject);
            Destroy(gameObject);
            Debug.Log("Sumando Puntos");
        }
    }
}
