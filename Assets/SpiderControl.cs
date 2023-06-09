using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderControl : MonoBehaviour
{
    public float speed = 5f;
    public float rotationChangeInterval = 2f; // Intervalo de tiempo para cambiar la rotación
    public string boxTag = "Caja"; // Tag de la caja
    public string tapaTag = "Tapa";
    public Transform[] targetPoints; // Puntos de destino que la araña seguirá

    private int currentTargetIndex = 0;
    private Quaternion targetRotation;
    private float rotationTimer = 0f;
    private bool isMoving = true; // Indica si la araña está en movimiento

    private Animation animation; // Referencia al componente Animation

    private Rigidbody rb; // Referencia al componente Rigidbody

    void Start()
    {
        animation = GetComponent<Animation>(); // Obtener el componente Animation
        rb = GetComponent<Rigidbody>(); // Obtener el componente Rigidbody

        MoveToNextTarget(); // Mover a la posición inicial
    }

    void Update()
    {
        if (isMoving)
        {
            MoveTowardsTarget();

            rotationTimer += Time.deltaTime;
            if (rotationTimer >= rotationChangeInterval)
            {
                RotateTowardsNextTarget();
                rotationTimer = 0f;
            }
        }
    }

    void MoveTowardsTarget()
    {
        if (targetPoints.Length == 0) return;

        Vector3 targetPosition = targetPoints[currentTargetIndex].position;
        Vector3 movement = (targetPosition - transform.position).normalized * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            MoveToNextTarget();
        }
    }

    void RotateTowardsNextTarget()
    {
        if (targetPoints.Length == 0) return;

        Vector3 nextTargetPosition = targetPoints[(currentTargetIndex + 1) % targetPoints.Length].position;
        Vector3 direction = (nextTargetPosition - transform.position).normalized;
        targetRotation = Quaternion.LookRotation(direction);
    }

    void MoveToNextTarget()
    {
        currentTargetIndex = (currentTargetIndex + 1) % targetPoints.Length;

        RotateTowardsNextTarget();
        Vector3 eulerRotation = targetRotation.eulerAngles;
        eulerRotation.x = 0f;
        eulerRotation.z = 0f;
        targetRotation = Quaternion.Euler(eulerRotation);

        // Invertir la rotación para que la araña mire hacia adelante
        Vector3 invertedEulerRotation = targetRotation.eulerAngles;
        invertedEulerRotation.y += 180f;
        targetRotation = Quaternion.Euler(invertedEulerRotation);

        transform.rotation = targetRotation; // Establecer la rotación hacia el siguiente punto

        animation.Play("run");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(boxTag) || other.CompareTag(tapaTag))
        {
            Debug.Log("Colisiono");

            // Destruir el objeto cuando colisiona con la caja o la tapa
            Destroy(gameObject);
        }
    }
}