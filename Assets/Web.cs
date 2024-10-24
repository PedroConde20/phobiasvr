        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;
        using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Web : MonoBehaviour
        {
    // Variables de sesi�n para almacenar los detalles del inicio de sesi�n
    public static string loggedInUsername;
    void Start()
            {
                // A correct website page.
                //StartCoroutine(GetRequest("http://localhost:8080/unity/Select.php"));

                //StartCoroutine(Login("Pedro", "12345"));

                //StartCoroutine(RegisterUser("Pedro2","12345","pedro2@gmail.com"));
            }

            public IEnumerator GetRequest(string uri)
            {
                using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
                {
                    // Request and wait for the desired page.
                    yield return webRequest.SendWebRequest();

                    string[] pages = uri.Split('/');
                    int page = pages.Length - 1;

                    switch (webRequest.result)
                    {
                        case UnityWebRequest.Result.ConnectionError:
                        case UnityWebRequest.Result.DataProcessingError:
                            Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                            break;
                        case UnityWebRequest.Result.ProtocolError:
                            Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                            break;
                        case UnityWebRequest.Result.Success:
                            Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                            break;
                    }
                }
            }
    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        UnityWebRequest www = UnityWebRequest.Post("https://unityvrprojectfobia.com/login.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string response = www.downloadHandler.text;
            if (response.StartsWith("Login Success"))
            {
                // Store the full response in loggedInUsername
                loggedInUsername = response.Substring(response.LastIndexOf(' ') + 1);

                // Extract the username only and store it in a separate variable
                string[] parts = loggedInUsername.Split('.');
                if (parts.Length > 1)
                {
                    string usernameOnly = parts[1];
                    Debug.Log("Logged in as: " + usernameOnly);
                }

                SceneManager.LoadScene("MenuV");
            }
            else
            {
                Debug.Log(response);
            }
        }
    }
    /*
    public IEnumerator RegisterUser(string username, string password, string email)
            {
                WWWForm form = new WWWForm();
                form.AddField("loginUser", username);
                form.AddField("loginPass", password);
                form.AddField("loginEmail", email);

                UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/unity/register.php", form);
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                }
            }
    */
        }
    