using UnityEngine;

public class AdminView : MonoBehaviour
{
    public GameObject adminObjectsParent; // Este es el objeto padre que contiene los objetos que se mostrarán u ocultarán para el administrador
    public GameObject[] adminObjects; // Array de objetos que se mostrarán u ocultarán para el administrador

    void Start()
    {
        // Verifica si el nombre de usuario está disponible
        if (!string.IsNullOrEmpty(Web.loggedInUsername))
        {
            // Obtiene el nombre de usuario y el rol
            string[] parts = Web.loggedInUsername.Split('.');
            string username = (parts.Length > 1) ? parts[1] : Web.loggedInUsername;
            string role = (parts.Length > 0) ? parts[0] : "Unknown Role";


            Debug.Log(role);
            // Si el rol del usuario es "Admin", muestra los objetos
            if (role == "administrator")
            {
                ShowAdminObjects();
            }
            else
            {
                HideAdminObjects();
            }
        }
        else
        {
            // Si el nombre de usuario no está disponible, oculta los objetos
            HideAdminObjects();
        }
    }

    void ShowAdminObjects()
    {
        // Activa el objeto padre y todos los objetos hijos
        adminObjectsParent.SetActive(true);
        foreach (GameObject obj in adminObjects)
        {
            obj.SetActive(true);
        }
    }

    void HideAdminObjects()
    {
        // Desactiva el objeto padre y todos los objetos hijos
        adminObjectsParent.SetActive(false);
        foreach (GameObject obj in adminObjects)
        {
            obj.SetActive(false);
        }
    }
}
