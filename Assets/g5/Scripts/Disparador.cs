using Oculus.Platform.Samples.VrBoardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Disparador : MonoBehaviour
{

    public GameObject Arma;

    public GameObject Balas;

    public GameObject municion; //cargador

    public GameObject sinMunicion; //cargador

    public bool verificarMunicion; //Verifica si hay un cargador en el arma 

    public Transform caidaMunicion; //agregamos el lugar en donde caera la municion

    private int municionRifle = 30;




    public AudioSource controlSonido; //Controlar el Sonido
    public AudioClip sonidoDisparo; //Reproducir Sonido
    public AudioClip sonidoSinBalas;
    public AudioClip sonidoRecarga;

    public bool sinmunicion;


    [Range(0, 20)]
    public float rangoRotacion = 3;

    void Start()
    {
        verificarMunicion = true;
    }
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            disparar();
            if (sinmunicion)
            {
                Debug.Log("Estamos sin municion");

                sinmunicion = false;
            }
        }
        if (municionRifle == 0 && verificarMunicion)
        {
            sinmunicion = true;
            controlSonido.PlayOneShot(sonidoSinBalas);
            Instantiate(sinMunicion, caidaMunicion.transform.position, transform.rotation);
            verificarMunicion = false;
        }



    }
    public bool ArmaCargada()
    {
        if (municionRifle > 0)
            return true;
        else return false;
    }
    void disparar()
    {
        if (municionRifle > 0)
        {
            sinmunicion = false;
            GameObject bullet =
               (GameObject)Instantiate(Balas,
                           transform.position,
                           transform.rotation);
            float rotY = Random.Range(-rangoRotacion, rangoRotacion);
            float rotX = Random.Range(-rangoRotacion, rangoRotacion);
            bullet.transform.Rotate(rotX, rotY, 0);
            municionRifle--;
            if (sinmunicion == false)
            {
                controlSonido.PlayOneShot(sonidoDisparo);
            }
        }
    }

    public void nuevaMunicion()
    {
        municionRifle = 30;
        verificarMunicion = true;
        Quaternion spawnCargador = Quaternion.Euler(0,0,90);
        StartCoroutine(CrearCargador(200, spawnCargador));
        controlSonido.PlayOneShot(sonidoRecarga);
    }
    IEnumerator CrearCargador(int segundos, Quaternion rotacionCargador)
    {
        yield return new WaitForSeconds(segundos);
        Instantiate(municion, new Vector3(0.966f, 0.771f, -0.465f), rotacionCargador);
    }
}
