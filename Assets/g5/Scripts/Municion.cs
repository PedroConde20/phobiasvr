using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municion : MonoBehaviour
{

    public GameObject sinMunicion;
    // Start is called before the first frame update

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Magazine")
        {
            Destroy(other.gameObject);
            Instantiate(sinMunicion, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            FindObjectOfType<Disparador>().nuevaMunicion();
        }
    }
}
