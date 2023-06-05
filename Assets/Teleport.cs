using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform playerCamera;
    public OVRInput.Controller controller = OVRInput.Controller.RTouch;
    public float teleportRange = 10f;

    private RaycastHit hit;
    private bool isTeleporting = false;

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, controller))
        {
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, teleportRange))
            {
                OVRManager.instance.enabled = false;
                transform.position = hit.point;
                isTeleporting = true;
            }
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstick, controller) && isTeleporting)
        {
            OVRManager.instance.enabled = true;
            isTeleporting = false;
        }
    }
}
