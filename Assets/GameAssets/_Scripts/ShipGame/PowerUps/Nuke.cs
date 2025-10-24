using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Nuke : MonoBehaviour
{
    [SerializeField] private GameObject nukeEffectPrefab; // Objeto de la explosion del nuke
    [SerializeField] private float effectDuration = 0.01f;   // Duracion de la explosion antes de destruirse
    [SerializeField] private float fallSpeed = 0.5f; // Velocidad de caida del power-up

    private AudioSource _pickUpSound;

    private void Start()
    {
        _pickUpSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.position += Vector3.down * (fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _pickUpSound.pitch = Random.Range(0.9f, 1.1f);
        
        // Verifica si el objeto que colision� tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            _pickUpSound.Play();
            
            // Llama al metodo para activar la explosion de Nuke
            ActivateNukeEffect();
            Debug.Log("Nuke activated!");

            // Destruye el power-up despues de ser recogido
            Destroy(gameObject);
        }
    }

    public void ActivateNukeEffect()
    {
        if (nukeEffectPrefab != null)
        {
            // Instancia el efecto del nuke en la posicion actual

            // Destruye el nuke effect despues de un tiempo
            Destroy(Instantiate(nukeEffectPrefab, transform.position, Quaternion.identity), effectDuration);
        }
        else
        {
            Debug.LogWarning("Nuke effect prefab is not assigned!");
        }
    }
}

