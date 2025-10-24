using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 0.5f; // Velocidad de ca�da del power-up

    private void Update()
    {
        // Hacer que el power-up caiga lentamente hacia abajo
        transform.position += Vector3.down * (fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que colision� tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Busca el componente ShipClassController en el jugador
            var shipClassController = other.gameObject.GetComponentInParent<ShipClassController>();
            if (shipClassController != null)
            {
                // Llama al metodo Shield del controlador del jugador
                shipClassController.Shield();
                Debug.Log("Shield activated!");
            }
            else
            {
                Debug.LogWarning("No ShipClassController found in Player!");
            }

            // Destruye el power-up despues de ser recogido
            Destroy(gameObject);
        }
    }
}
