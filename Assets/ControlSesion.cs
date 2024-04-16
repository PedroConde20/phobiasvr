using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSesion : MonoBehaviour
{
    public string fobia = ""; 
    void Start()
    {
        // Verifica si el nombre de usuario está disponible
        if (!string.IsNullOrEmpty(Web.loggedInUsername))
        {
            // Obtiene el nombre de usuario y el rol
            string[] parts = Web.loggedInUsername.Split('.');
            string username = (parts.Length > 1) ? parts[1] : Web.loggedInUsername;
            string role = (parts.Length > 0) ? parts[0] : "Unknown Role";

            // Actualiza el texto de bienvenida con el nombre de usuario y el rol
            Debug.Log(username + " " + "Rol: " + role + " Esta entrando " + fobia);
        }
        else
        {
            // Si el nombre de usuario no está disponible, muestra un mensaje genérico
            Debug.Log("Usuario sin sesion en " + fobia);
        }
    }
}
