using UnityEngine;

public class AutoFireOn : MonoBehaviour
{
    [SerializeField] private float autoFireDuration = 5f; // Duraci�n del disparo autom�tico
    [SerializeField] private float fallSpeed = 0.5f; // Velocidad de ca�da
    [SerializeField] private AudioSource pickUpSound;

    private void Update()
    {
        // Mueve el objeto hacia abajo
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que colision� tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Busca al padre del objeto Player
            Transform parent = other.transform.parent;
            if (parent != null)
            {
                // Busca ProjectileSpawner en los hermanos del objeto colisionado
                foreach (Transform sibling in parent)
                {
                    var autofire = sibling.GetComponent<ProjectileSpawner>();
                    if (autofire != null)
                    {
                        // Activa el modo AutoFire en el ProjectileSpawner
                        autofire.AutoFireOn(autoFireDuration, gameObject);
                        Debug.Log("AutoFire activated!");
                        return; // Termina la funci�n si encuentra el componente
                    }
                }
                Debug.LogWarning("No ProjectileSpawner found in Player's siblings!");
            }
            else
            {
                Debug.LogWarning("Player object has no parent, cannot find siblings!");
            }
        }
    }
}
