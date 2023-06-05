using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public OVRInput.Controller controller;  // Controlador derecho de VR
    public Transform player;  // Transform del jugador
    public OVRCameraRig cameraRig;  // Referencia al OVRCameraRig

    public float teleportationRange = 10f;  // Rango m�ximo de teletransportaci�n

    private void Update()
    {
        // Obtener la posici�n y direcci�n del controlador
        Vector3 controllerPosition = OVRInput.GetLocalControllerPosition(controller);

        // Obtener la direcci�n hacia la que mira la c�mara OVRCameraRig
        Vector3 cameraDirection = cameraRig.centerEyeAnchor.forward;

        // Calcular la direcci�n del teletransporte como la suma del forward de la c�mara y el input de la palanca del joycon
        Vector2 thumbstickInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        Vector3 teleportDirection = cameraDirection + new Vector3(thumbstickInput.x, 0f, thumbstickInput.y);

        // Normalizar la direcci�n del teletransporte
        teleportDirection.Normalize();

        // Calcular la posici�n de destino multiplicando la direcci�n del teletransporte por el rango m�ximo
        Vector3 destination = controllerPosition + teleportDirection * teleportationRange;

        // Raycast desde la posici�n del controlador en la direcci�n de teletransporte
        Ray ray = new Ray(controllerPosition, teleportDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, teleportationRange))
        {
            // Teletransportar al jugador a la ubicaci�n del raycast hit
            player.position = hit.point;
        }
        else
        {
            // Teletransportar al jugador a la posici�n de destino
            player.position = destination;
        }
    }
}