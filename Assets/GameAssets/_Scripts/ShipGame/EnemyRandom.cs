using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandom : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f; // Velocidad del enemigo
    [SerializeField] private float directionChangeInterval = 2f; // Tiempo entre cambios de dirección

    private Vector2 currentDirection; // Dirección actual del enemigo
    private float directionChangeTimer;

    private Camera mainCamera; // Cámara principal para obtener los límites de la pantalla

    void Start()
    {
        // Obtén la cámara principal
        mainCamera = Camera.main;
        transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        // Establece una dirección inicial aleatoria
        SetRandomDirection();
        directionChangeTimer = directionChangeInterval;
    }

    void Update()
    {
        // Mueve al enemigo en la dirección actual
        transform.Translate(currentDirection * moveSpeed * Time.deltaTime);

        // Actualiza el temporizador
        directionChangeTimer -= Time.deltaTime;

        // Cambia de dirección cuando el temporizador llegue a 0
        if (directionChangeTimer <= 0f)
        {
            SetRandomDirection();
            directionChangeTimer = directionChangeInterval;
        }

        // Verifica si el enemigo se ha salido de los límites de la pantalla
        CheckBounds();
    }

    private void SetRandomDirection()
    {
        // Genera una dirección aleatoria (arriba, abajo, izquierda, derecha, diagonales)
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);

        // Normaliza para mantener una velocidad constante en cualquier dirección
        currentDirection = new Vector2(x, y).normalized;
    }

    private void CheckBounds()
    {
        // Obtiene los límites de la cámara en el mundo
        Vector3 screenPos = mainCamera.WorldToViewportPoint(transform.position);

        // Si el enemigo se acerca al borde de la pantalla, cambia su dirección
        if (screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1)
        {
            SetRandomDirection(); // Cambia la dirección cuando esté fuera de los límites
        }
    }

    private void OnDrawGizmos()
    {
        // Dibuja una línea para mostrar la dirección en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)currentDirection);
    }
}
