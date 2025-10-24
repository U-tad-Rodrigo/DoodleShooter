using System;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;     // Velocidad de movimiento

    private void Update()
    {
        // Encuentra el objeto con el tag "Player"
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            // Calcula la dirección hacia el jugador
            Vector2 direction = player.transform.position - transform.position;

            // Mueve al enemigo hacia el jugador
            transform.Translate(direction.normalized * (moveSpeed * Time.deltaTime));
        }
    }

    // Mostrar la dirección del movimiento en Gizmos
    private void OnDrawGizmosSelected()
    {
        // Encuentra al jugador para dibujar la línea
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.transform.position);
        }
    }
}
