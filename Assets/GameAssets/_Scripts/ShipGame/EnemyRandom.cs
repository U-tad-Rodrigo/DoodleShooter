using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandom : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f; // Velocidad del enemigo
    [SerializeField] private float directionChangeInterval = 2f; // Tiempo entre cambios de direcci�n

    private Vector2 currentDirection; // Direcci�n actual del enemigo
    private float directionChangeTimer;

    private Camera mainCamera; // C�mara principal para obtener los l�mites de la pantalla

    void Start()
    {
        // Obt�n la c�mara principal
        mainCamera = Camera.main;
        transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        // Establece una direcci�n inicial aleatoria
        SetRandomDirection();
        directionChangeTimer = directionChangeInterval;
    }

    void Update()
    {
        // Mueve al enemigo en la direcci�n actual
        transform.Translate(currentDirection * moveSpeed * Time.deltaTime);

        // Actualiza el temporizador
        directionChangeTimer -= Time.deltaTime;

        // Cambia de direcci�n cuando el temporizador llegue a 0
        if (directionChangeTimer <= 0f)
        {
            SetRandomDirection();
            directionChangeTimer = directionChangeInterval;
        }

        // Verifica si el enemigo se ha salido de los l�mites de la pantalla
        CheckBounds();
    }

    private void SetRandomDirection()
    {
        // Genera una direcci�n aleatoria (arriba, abajo, izquierda, derecha, diagonales)
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);

        // Normaliza para mantener una velocidad constante en cualquier direcci�n
        currentDirection = new Vector2(x, y).normalized;
    }

    private void CheckBounds()
    {
        // Obtiene los l�mites de la c�mara en el mundo
        Vector3 screenPos = mainCamera.WorldToViewportPoint(transform.position);

        // Si el enemigo se acerca al borde de la pantalla, cambia su direcci�n
        if (screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1)
        {
            SetRandomDirection(); // Cambia la direcci�n cuando est� fuera de los l�mites
        }
    }

    private void OnDrawGizmos()
    {
        // Dibuja una l�nea para mostrar la direcci�n en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)currentDirection);
    }
}
