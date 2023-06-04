using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunicionBaja : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Cargador(5));
    }
    IEnumerator Cargador(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        Destroy(gameObject);
    }
}
