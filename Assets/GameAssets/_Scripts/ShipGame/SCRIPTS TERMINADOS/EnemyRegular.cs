using System;
using System.Transactions;
using UnityEngine;

public class EnemyRegular : MonoBehaviour
{

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;      // Velocidad de movimiento
    [SerializeField] private float maxHorizontalSpeed = 1f; // Velocidad m�xima en el eje X (desviaci�n horizontal)
    [SerializeField] private float destroyAfterSeconds = 5f; // Tiempo antes de destruir

    private Vector2 movementDirection;

    private void Start()
    {
        // Define la direcci�n diagonal con una desviaci�n horizontal aleatoria
        float horizontalSpeed = UnityEngine.Random.Range(-maxHorizontalSpeed, maxHorizontalSpeed); // Velocidad horizontal aleatoria
        float verticalSpeed = -moveSpeed; // Siempre baja hacia abajo
        movementDirection = new Vector2(horizontalSpeed, verticalSpeed).normalized; // Normaliza el vector para mantener consistencia
        DestroyAfterTime(destroyAfterSeconds);

    }
    private void Update()
    {
        // Mueve el objeto en la direcci�n calculada
        transform.Translate(movementDirection * (moveSpeed * Time.deltaTime));
    }
    private void DestroyAfterTime(float seconds)
    {
        Destroy(gameObject, seconds); // Destruye el objeto despu�s del tiempo especificado
    }


}
