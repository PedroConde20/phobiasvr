using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveGenerate : MonoBehaviour
{
    [SerializeField] float mov;
    
    bool right;
    bool left;

    Vector3 result;
    Vector3 a, b;
    float t;
    Vector3 izq, der;
    

    void Start() {
        izq = transform.position;
        der = new Vector3(transform.position.x, transform.position.y, transform.position.z + mov);
        /*while(true){
            yield return new WaitForSeconds(5);
            //Instantiate(objectivePrefab);
        }*/

    }

    void Update()
    {
        

        //t += Time.deltaTime * 0.1f;
        t = 5f * Time.deltaTime;
        

        
        if (transform.position == izq)
        {

            left = false;
            right = true;

        }
        if(transform.position == der){
            left = true;
            right = false;
        }
        if (right)
        {
            transform.position = Vector3.Lerp(transform.position,der,t);
        }

        if (left)
        {
            transform.position = Vector3.Lerp(transform.position,izq,t);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bala")
        {
            FindObjectOfType<Puntaje>().SumarPuntos(10);
        }
    }
}
