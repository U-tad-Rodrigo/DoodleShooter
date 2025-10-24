using UnityEngine;
using UnityEngine.Serialization;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private int hpCure = 50; // Cantidad de salud que restaura la pocion
    [SerializeField] private float fallSpeed = 0.5f; // Velocidad de ca�da
    [SerializeField] private AudioSource pickUpSound;
    
    private void Update()
    {
        transform.position += Vector3.down * (fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        pickUpSound.pitch = Random.Range(0.8f, 1.2f);
        pickUpSound.Play();
        
        // Verifica si el objeto que colisiona tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Usa GetComponentInParent sobre el GameObject que est� en el collider
            var playerHealth = other.gameObject.GetComponentInParent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Llama a un m�todo para curar al jugador
                playerHealth.Heal(hpCure);
                Debug.Log($"Player healed by {hpCure} points.");
            }
            else
            {
                Debug.LogError("PlayerHealth component not found on the Player or its parent objects.");
            }

            // Destruye la poci�n despu�s de ser recogida
            Destroy(gameObject);
        }
    }
}
