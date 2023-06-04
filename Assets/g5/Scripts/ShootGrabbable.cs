using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootGrabbable : MonoBehaviour
{
    private SimpleShoot simpleShoot;
    private OVRGrabbable grabbable;
    public OVRInput.Button shoottingButton;
    public int maxNumberBullet;
    public Text message;
    public Text message2;
    // Start is called before the first frame update
    void Start()
    {
        maxNumberBullet = 10;
        simpleShoot = GetComponent<SimpleShoot>();
        grabbable = GetComponent<OVRGrabbable>();
        message.text = maxNumberBullet.ToString();
        message2.text = "Puntaje: 100";
    }

    // Update is called once per frame
    void Update()
    {
        if(grabbable.isGrabbed && OVRInput.GetDown(shoottingButton, grabbable.grabbedBy.GetController()) && maxNumberBullet > 0)
        {
            simpleShoot.TriggerShoot();
            maxNumberBullet--;
            message.text = maxNumberBullet.ToString();
        }
    }
}
