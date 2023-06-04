using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoBalas : MonoBehaviour
{
    public float velocidad = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, velocidad * Time.deltaTime);
        Destroy(this.gameObject, 5f);
    }
    private void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);
    }
}
