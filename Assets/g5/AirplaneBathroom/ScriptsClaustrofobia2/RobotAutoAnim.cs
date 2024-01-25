using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RobotAutoAnim : MonoBehaviour
{
    public string sigueactivados = "sigue activo";
    public Vector3 rot = Vector3.zero;
    public float rotSpeed = 40f;
    public float rotationLerpSpeed = 5f;
    public Animator anim;
    public float velocidad = 2.0f;
    private CharacterController characterController;

    private bool isMoving = false;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        gameObject.transform.eulerAngles = rot;

        // Iniciar el movimiento automático
        Invoke("StartAutoMovement", 3f);
    }

    void StartAutoMovement()
    {
        // Iniciar el movimiento automático
        StartCoroutine(AutoMoveSequence());
    }

    IEnumerator AutoMoveSequence()
    {
        // Mover hacia adelante durante 4 segundos
        anim.SetBool("Walk_Anim", true);
        isMoving = true;
        yield return new WaitForSeconds(4f);

        // Detener el movimiento
        anim.SetBool("Walk_Anim", false);
        isMoving = false;

        // Girar a la izquierda antes de Roll_Anim
        float targetRotation = rot.y - 30f;
        while (Mathf.Abs(rot.y - targetRotation) > 0.01f)
        {
            rot.y = Mathf.Lerp(rot.y, targetRotation, Time.deltaTime * rotationLerpSpeed);
            yield return null;
        }

        // Realizar el Roll_Anim
        anim.SetBool("Roll_Anim", true);
        // Girar hacia el otro costado después de Roll_Anim
        targetRotation = rot.y + 100f;
        while (Mathf.Abs(rot.y - targetRotation) > 0.01f)
        {
            rot.y = Mathf.Lerp(rot.y, targetRotation, Time.deltaTime * rotationLerpSpeed);
            // Aumentar la velocidad durante Roll_Anim
            velocidad = 2.6f; // Ajusta este valor según tus necesidades
            yield return null;
        }

        isMoving = true;
        yield return new WaitForSeconds(1.0f);

        // Detener el Roll_Anim
        anim.SetBool("Roll_Anim", false);

        // Restaurar la velocidad después de Roll_Anim
        velocidad = 2.0f; // Ajusta este valor según tus necesidades

        // Realizar el Open_Anim
        // anim.SetBool("Open_Anim", true);
        isMoving = false;

        // Rotar gradualmente hacia la derecha después de que Roll_Anim se establezca en falso
        float initialRotation = rot.y;
        float targetRotationRoll = initialRotation + 110f;
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            rot.y = Mathf.Lerp(initialRotation, targetRotationRoll, elapsedTime);
            elapsedTime += Time.deltaTime * rotationLerpSpeed;
            yield return null;
        }
        rot.y = targetRotationRoll; // Asegurarse de que la rotación sea exacta al final
    }

    void Update()
    {
        MoveRobot();
        gameObject.transform.eulerAngles = rot;
    }

    void MoveRobot()
    {
        if (isMoving)
        {
            // Mover con CharacterController
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            characterController.Move(forward * velocidad * Time.deltaTime);
        }
    }
}
