using UnityEngine;
using UnityEngine.UI;

public class InputFieldManager : MonoBehaviour
{
    public InputField inputField1;
    public InputField inputField2;

    public void Start()
    {
        // Asigna eventos de clic a cada campo de entrada de texto
        Button inputField1Button = inputField1.GetComponent<Button>();
        inputField1Button.onClick.AddListener(() => ActivateInputField(inputField1));

        Button inputField2Button = inputField2.GetComponent<Button>();
        inputField2Button.onClick.AddListener(() => ActivateInputField(inputField2));

    }

    // Método para activar un campo de entrada de texto específico
    public void ActivateInputField(InputField inputField)
    {
        inputField.Select();
        inputField.ActivateInputField();
    }
}
