using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class RegistroAdmin : MonoBehaviour
{
    public string url = "https://unityvrproject.000webhostapp.com/Register.php";
    public Text usuarioInput;
    public Text nombreInput;
    public Text primerApellidoInput;
    public Text segundoApellidoInput;
    public Text contraseñaInput;
    public Text correoElectronicoInput;
    public float seactualiza;

    public void RegistrarAdmin()
    {
        string usuario = usuarioInput.text;
        string nombre = nombreInput.text;
        string primerApellido = primerApellidoInput.text;
        string segundoApellido = segundoApellidoInput.text;
        string contraseña = contraseñaInput.text;
        string correoElectronico = correoElectronicoInput.text;

        // Verificar si algún campo está vacío

        Debug.Log(usuarioInput.text + nombreInput.text
                + primerApellidoInput.text + segundoApellidoInput.text
                + contraseñaInput.text + correoElectronicoInput.text);
        if (string. IsNullOrEmpty(usuario) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(primerApellido) ||
            string.IsNullOrEmpty(segundoApellido) || string.IsNullOrEmpty(contraseña) || string.IsNullOrEmpty(correoElectronico))
        {
            Debug.Log("Por favor completa todos los campos.");
            return;
        }

        StartCoroutine(RegistrarNuevoAdmin(usuario, nombre, primerApellido, segundoApellido, contraseña, correoElectronico));

        SceneManager.LoadScene("Login 1");
    }

    IEnumerator RegistrarNuevoAdmin(string usuario, string nombre, string primerApellido, string segundoApellido, string contraseña, string correoElectronico)
    {
        WWWForm form = new WWWForm();
        form.AddField("Usuario", usuario);
        form.AddField("Nombre", nombre);
        form.AddField("PrimerApellido", primerApellido);
        form.AddField("SegundoApellido", segundoApellido);
        form.AddField("Contraseña", contraseña);
        form.AddField("CorreoElectronico", correoElectronico);

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