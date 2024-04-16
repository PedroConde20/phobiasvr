using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InicioController : MonoBehaviour
{
    public Text welcomeText;

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
            welcomeText.text = "Bienvenido " + username + " "  +  "Rol: " + role;
        }
        else
        {
            // Si el nombre de usuario no está disponible, muestra un mensaje genérico
            welcomeText.text = "Usuario sin sesion";
        }
    }
}
