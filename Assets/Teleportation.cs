using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public OVRInput.Controller controller;  // Controlador derecho de VR
    public Transform player;  // Transform del jugador
    public OVRCameraRig cameraRig;  // Referencia al OVRCameraRig

    public float teleportationRange = 10f;  // Rango máximo de teletransportación

    private void Update()
    {
        // Obtener la posición y dirección del controlador
        Vector3 controllerPosition = OVRInput.GetLocalControllerPosition(controller);

        // Obtener la dirección hacia la que mira la cámara OVRCameraRig
        Vector3 cameraDirection = cameraRig.centerEyeAnchor.forward;

        // Calcular la dirección del teletransporte como la suma del forward de la cámara y el input de la palanca del joycon
        Vector2 thumbstickInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        Vector3 teleportDirection = cameraDirection + new Vector3(thumbstickInput.x, 0f, thumbstickInput.y);

        // Normalizar la dirección del teletransporte
        teleportDirection.Normalize();

        // Calcular la posición de destino multiplicando la dirección del teletransporte por el rango máximo
        Vector3 destination = controllerPosition + teleportDirection * teleportationRange;

        // Raycast desde la posición del controlador en la dirección de teletransporte
        Ray ray = new Ray(controllerPosition, teleportDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, teleportationRange))
        {
            // Teletransportar al jugador a la ubicación del raycast hit
            player.position = hit.point;
        }
        else
        {
            // Teletransportar al jugador a la posición de destino
            player.position = destination;
        }
    }
}