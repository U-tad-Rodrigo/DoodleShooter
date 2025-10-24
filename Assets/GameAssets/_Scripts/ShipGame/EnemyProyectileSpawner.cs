using System.Collections;
using UnityEngine;

public class EnemyProyectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;   // Prefab del proyectil
    [SerializeField] private Transform projectileSpawnPoint; // Punto de aparici�n del proyectil
    [SerializeField] private float fireRate = 1f;           // Tiempo entre cada disparo (1 segundo)
    [SerializeField] private float lifeTime = 5f;           // Tiempo de vida del proyectil

    private void Start()
    {
        // Comienza a disparar autom�ticamente cada segundo
        StartCoroutine(SpawnProjectiles());
    }

    private IEnumerator SpawnProjectiles()
    {
        while (true)
        {
            // Instancia el proyectil en el punto de spawn
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

            // Destruye el proyectil despu�s del tiempo de vida especificado
            Destroy(projectile, lifeTime);

            // Espera el tiempo especificado antes de disparar nuevamente
            yield return new WaitForSeconds(fireRate);
        }
    }
}
