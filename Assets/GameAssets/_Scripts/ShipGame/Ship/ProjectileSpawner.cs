using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private Transform projectilePool;
    [SerializeField] private Transform activeProjectilePool;
    [SerializeField] private List<Transform> projectileSpawnPoints; // Lista de puntos de spawn
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float lifeTime, despawnLifetime;
    [SerializeField] private float fireRate;  // Tiempo entre disparos cuando no est� en "AutoFire"
    [SerializeField] private AudioSource shotSound;

    private bool _canSpawn = true;
    private bool _isAutoFiring = false;  // Indica si estamos en modo AutoFire
    private float _remainingAutoFireTime = 0;

    private void Start()
    {
        InputManager.Instance.FirePerformed += OnFirePerformed;
    }

    private void OnFirePerformed()
    {
        // Solo permite disparar si no est� en modo AutoFire o si est� listo para disparar
        if (!_canSpawn || _isAutoFiring) return;

        _canSpawn = false;
        StartCoroutine(SpawnCooldown());

        FireProjectiles(); // Cambiado para disparar desde multiples puntos
    }

    public void AutoFireOn(float autofiretime, GameObject powerUp)
    {
        Destroy(powerUp);  // Destruye el PowerUp al activarlo
        if (_isAutoFiring)
        {
            // Si ya est� en modo AutoFire, simplemente a�ade tiempo adicional
            _remainingAutoFireTime += autofiretime;
        }
        else
        {
            // Si no est� en modo AutoFire, inicia la corrutina
            _remainingAutoFireTime = autofiretime;
            StartCoroutine(ActivateAutoFire());
        }
    }

    private IEnumerator ActivateAutoFire()
    {
        _isAutoFiring = true;

        while (_remainingAutoFireTime > 0f)
        {
            // Dispara los proyectiles y espera 0.2 segundos
            FireProjectiles();
            yield return new WaitForSeconds(0.2f);

            // Resta el tiempo transcurrido
            _remainingAutoFireTime -= 0.2f;
        }

        // Termina el modo AutoFire
        _isAutoFiring = false;
    }

    private void FireProjectiles()
    {
        foreach (Transform spawnPoint in projectileSpawnPoints)
        {
            shotSound.pitch = Random.Range(0.9f, 1.1f);
            shotSound.Play();
            
            GameObject projectile;

            if (projectilePool.childCount <= 0)
            {
                projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
            }
            else
            {
                projectile = projectilePool.GetChild(0).gameObject;
                projectile.transform.position = spawnPoint.position;
                projectile.transform.rotation = spawnPoint.rotation;
                projectile.SetActive(true);
            }

            projectile.transform.SetParent(activeProjectilePool);

            StartCoroutine(DestroyProjectile(projectile.GetComponent<Projectile>()));
        }
    }

    private IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(fireRate);  // Tiempo entre disparos cuando no est� en "AutoFire"
        _canSpawn = true;
    }

    private IEnumerator DestroyProjectile(Projectile projectile)
    {
        yield return new WaitForSeconds(lifeTime);
        projectile.DestroyProjectile();
        yield return new WaitForSeconds(despawnLifetime);
        projectile.gameObject.SetActive(false);
        projectile.transform.SetParent(projectilePool);
    }

    public void MoveToObject(GameObject targetObject)
    {
        if (targetObject != null)
        {
            transform.position = targetObject.transform.position;
            Debug.Log($"Spawner movido a la posicion del objeto: {targetObject.name}");
        }
        else
        {
            Debug.LogWarning("El objeto objetivo es nulo. No se realiz� el movimiento.");
        }
    }

    public void SetSpawnPoints(List<Transform> newSpawnPoints)
    {
        projectileSpawnPoints = newSpawnPoints;
        Debug.Log("Nuevos puntos de spawn asignados.");
    }
}
