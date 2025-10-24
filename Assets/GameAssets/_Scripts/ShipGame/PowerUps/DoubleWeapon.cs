using UnityEngine;

public class DoubleWeapon : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 0.5f; // Velocidad de ca�da

    private AudioSource _pickUpSound;

    private void Start()
    {
        _pickUpSound = transform.GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Hacer que el power-up caiga lentamente
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _pickUpSound.pitch = Random.Range(0.9f, 1.1f);
        // Verifica si el objeto que colision� tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            _pickUpSound.pitch = Random.Range(0.9f, 1.1f);
            _pickUpSound.Play();
            
            // Busca el componente ShipClassController en el jugador
            var shipClassController = other.gameObject.GetComponentInParent<ShipClassController>();
            if (shipClassController != null)
            {
                // Llama al m�todo DoubleWeapon del controlador del jugador
                shipClassController.DoubleWeapon();
                Debug.Log("Double Weapon activated!");
            }
            else
            {
                Debug.LogWarning("No ShipClassController found in Player!");
            }

            // Destruye el power-up despu�s de ser recogido
            Destroy(gameObject);
        }
    }
}

