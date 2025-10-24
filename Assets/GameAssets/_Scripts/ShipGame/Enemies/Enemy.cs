using System;
using System.Transactions;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float enemyDamage = 10f;       // Daño que inflige el enemigo
    [SerializeField] private int experienceGiven = 50; // Experiencia dada al derrotar al enemigo

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;     // Velocidad de movimiento

    [Header("Extras")]
    [SerializeField] private Transform target;

    private void Update()
    {
        Vector2 direction = target.position - transform.position;
        transform.Translate(direction.normalized * (moveSpeed * Time.deltaTime));
    }

    // Mostrar la dirección del movimiento en Gizmos
    private void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}