using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistance : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float initialDropDistance = 5f; // Distancia inicial hacia abajo
    [SerializeField] private float dropSpeed = 2f;          // Velocidad al bajar inicialmente
    [SerializeField] private float horizontalSpeed = 3f;    // Velocidad horizontal al moverse
    [SerializeField] private float moveRangeX = 3f;        // Rango de movimiento horizontal

    [Header("Spawner")]
    [SerializeField] private GameObject spawner;            // Objeto hijo que se activar�

    private bool isDropping = true;  // Indica si el enemigo est� bajando
    private float initialY;          // Posici�n inicial en Y
    private float startXPosition;    // Posici�n inicial en X
    private float moveDirection = 1f; // Direcci�n de movimiento en X (1 para derecha, -1 para izquierda)

    private void Start()
    {
        // Guarda la posici�n inicial en Y y X
        initialY = transform.position.y;
        startXPosition = transform.position.x;

        // Desactiva el spawner inicialmente
        if (spawner != null)
        {
            spawner.SetActive(false);
        }
    }

    private void Update()
    {
        if (isDropping)
        {
            HandleInitialDrop();
        }
        else
        {
            MoveHorizontally();
        }
    }

    private void HandleInitialDrop()
    {
        // Mueve hacia abajo hasta alcanzar la distancia especificada
        transform.Translate(Vector2.down * (dropSpeed * Time.deltaTime));
        if (transform.position.y <= initialY - initialDropDistance)
        {
            isDropping = false; // Cambia a movimiento horizontal
            ActivateSpawner();  // Activa el spawner
        }
    }

    private void MoveHorizontally()
    {
        // Mueve al enemigo horizontalmente dentro del rango de -3 a 3 respecto a su posici�n inicial
        float newXPosition = transform.position.x + (moveDirection * horizontalSpeed * Time.deltaTime);

        // Limita la posici�n X al rango entre -3 y 3
        if (newXPosition >= startXPosition + moveRangeX || newXPosition <= startXPosition - moveRangeX)
        {
            moveDirection = -moveDirection;  // Cambia la direcci�n cuando alcanza los l�mites
        }

        // Aplica la nueva posici�n X
        transform.position = new Vector2(newXPosition, transform.position.y);
    }

    private void ActivateSpawner()
    {
        if (spawner != null)
        {
            spawner.SetActive(true); // Activa el objeto hijo
        }
    }
}
