using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyboardSetting : MonoBehaviour
{
    public Text textUser;
    public Text textPassword;
    private Text currentText; // Referencia al texto actual
    public char key;
    private float timer = -1;
    public float timeDelay = 0.25f;
    public enum Text_Type { Normal, Enter, Backspace , Delete , Uppercase , Lowercase };
    public Text_Type TextType;
    public GameObject lowercaseKeyboard;
    public GameObject uppercaseKeyboard;
    public Text placeholderUser;
    public Text placeholderPassword;
    void Start()
    {
        // Establecer el texto actual inicialmente al campo de usuario
        currentText = textUser;

        // Agregar listeners de eventos a los campos de texto
        AddEventTrigger(textUser);
        AddEventTrigger(textPassword);
    }

    void AddEventTrigger(Text textField)
    {
        // Obtener o agregar el componente EventTrigger
        EventTrigger eventTrigger = textField.gameObject.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = textField.gameObject.AddComponent<EventTrigger>();
        }

        // Agregar listener de evento para el puntero
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { OnTextFieldSelected(textField); });
        eventTrigger.triggers.Add(entry);
    }

    void OnTextFieldSelected(Text textField)
    {
        // Establecer el campo de texto actual al campo de texto seleccionado
        currentText = textField;
    }

    public void AddText()
    {
        if (Time.time - timer > timeDelay)
        {
            switch (TextType)
            {
                case Text_Type.Backspace:
                    if (currentText.text.Length > 0)
                    {
                        currentText.text = currentText.text.Substring(0, currentText.text.Length - 1);
                    }
                    break;
                case Text_Type.Enter:
                    currentText.text += "\r\n";
                    break;
                case Text_Type.Delete:
                    // Eliminar la última letra del texto
                    if (currentText.text.Length > 0)
                    {
                        currentText.text = currentText.text.Substring(0, currentText.text.Length - 1);
                    }
                    if (currentText == textUser)
                    {
                        if (currentText.text.Length > 0)
                        {
                            // Desactivar el placeholder correspondiente si existe
                            placeholderUser.enabled = false;
                        }
                        else
                        {
                            placeholderUser.enabled = true;
                        }
                    }
                    if (currentText == textPassword)
                    {
                        if (currentText.text.Length > 0)
                        {
                            // Desactivar el placeholder correspondiente si existe
                            placeholderPassword.enabled = false;

                        }
                        else
                        {
                            placeholderPassword.enabled = true;
                        }
                    }
                    break;
                case Text_Type.Uppercase:
                    // Desactivar GameObject del teclado en minúscula
                    if (lowercaseKeyboard != null)
                    {
                        lowercaseKeyboard.SetActive(false);
                        uppercaseKeyboard.SetActive(true);
                    }
                    break;
                case Text_Type.Lowercase:
                    // Desactivar GameObject del teclado en mayúscula
                    if (uppercaseKeyboard != null)
                    {
                        uppercaseKeyboard.SetActive(false);
                        lowercaseKeyboard.SetActive(true);
                    }
                    break;
                default:
                    currentText.text += key;
                    if (currentText == textUser)
                    {
                        if (currentText.text.Length > 0)
                        {
                            // Desactivar el placeholder correspondiente si existe
                            placeholderUser.enabled = false;
                        }
                        else
                        {
                            placeholderUser.enabled = true;
                        }
                    }
                    if (currentText == textPassword)
                    {
                        if (currentText.text.Length > 0)
                        {
                            // Desactivar el placeholder correspondiente si existe
                            placeholderPassword.enabled = false;

                        }
                        else
                        {
                            placeholderPassword.enabled = true;
                        }
                    }
                    break;

            }

            timer = Time.time;
        }
    }
}
