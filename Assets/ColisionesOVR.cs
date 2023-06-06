using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionesOVR : MonoBehaviour
{

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("NoColisionar"))
        {
            // Ignora la colisión con el objeto que tiene el Tag "NoColisionar"
            Physics.IgnoreCollision(characterController, hit.collider);
        }
    }
}