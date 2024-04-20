using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterUser : MonoBehaviour
{
    public string url = "https://unityvrproject.000webhostapp.com/RegisterUser.php";
    public Text usuarioInput;
    public Text nombreInput;
    public Text primerApellidoInput;
    public Text segundoApellidoInput;
    public Text contrase�aInput;
    public Text correoElectronicoInput;
    public float seactualiza;

    public void RegistrarUser()
    {
        string usuario = usuarioInput.text;
        string nombre = nombreInput.text;
        string primerApellido = primerApellidoInput.text;
        string segundoApellido = segundoApellidoInput.text;
        string contrase�a = contrase�aInput.text;
        string correoElectronico = correoElectronicoInput.text;

        // Verificar si alg�n campo est� vac�o
        // Obtiene el nombre de usuario y el rol
        string[] parts = Web.loggedInUsername.Split('.');
        string username = (parts.Length > 1) ? parts[1] : Web.loggedInUsername;
        string role = (parts.Length > 0) ? parts[0] : "Unknown Role";

        Debug.Log(usuarioInput.text + nombreInput.text
                + primerApellidoInput.text + segundoApellidoInput.text
                + contrase�aInput.text + correoElectronicoInput.text);

        if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(primerApellido) ||
            string.IsNullOrEmpty(segundoApellido) || string.IsNullOrEmpty(contrase�a) || string.IsNullOrEmpty(correoElectronico))
        {
            Debug.Log("Por favor completa todos los campos.");
            return;
        }

        StartCoroutine(RegistrarNuevoUser(usuario, nombre, primerApellido, segundoApellido, contrase�a, correoElectronico, username));

        SceneManager.LoadScene("MenuV");
    }

    IEnumerator RegistrarNuevoUser(string usuario, string nombre, string primerApellido, string segundoApellido, string contrase�a, string correoElectronico , string AdminUser)
    {
        // Obtiene el nombre de usuario y el rol del usuario activo
        string[] parts = Web.loggedInUsername.Split('.');
        string username = (parts.Length > 1) ? parts[1] : Web.loggedInUsername;
        string role = (parts.Length > 0) ? parts[0] : "Unknown Role";
        WWWForm form = new WWWForm();
        form.AddField("Usuario", usuario);
        form.AddField("Nombre", nombre);
        form.AddField("PrimerApellido", primerApellido);
        form.AddField("SegundoApellido", segundoApellido);
        form.AddField("Contrase�a", contrase�a);
        form.AddField("CorreoElectronico", correoElectronico);
        form.AddField("UsuarioAdministrador", username);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al registrar el administrador: " + www.error);
            }
            else
            {
                Debug.Log("Administrador registrado correctamente");
            }
        }
    }
}
