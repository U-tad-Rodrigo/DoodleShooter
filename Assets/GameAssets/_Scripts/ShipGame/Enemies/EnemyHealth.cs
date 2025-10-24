using UnityEngine;
using UnityEngine.Serialization;

public class EnemyHealth : HealthGlobal
{
    [Header("Enemy Stats")]
    [SerializeField] private int xpGiven = 50; // Experiencia al derrotar al enemigo
    [SerializeField] private int collisionDmg;

    [Header("Power-Up Drop Settings")]
    [SerializeField] private GameObject[] powerUps; // Lista de posibles power-ups
    [SerializeField] private float[] dropChances;  // Probabilidades de cada power-up (suma debe ser 1 o 100)

    private bool _diedByProjectile = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            var projectile = other.GetComponent<Projectile>();
            if (projectile != null)
            {
                float damage = projectile.GetDamage();
                Debug.Log($"Taken damage: {damage}");
                TakeDamage(damage);
                _diedByProjectile = true;
            }
        }
        else if (other.CompareTag("Player"))
        {
            _diedByProjectile= false;
            Death();
        } else if (other.CompareTag("ProjectileNuke")){
            _diedByProjectile = true;
            Death();
        }
    }

    protected override void Death()
    {
        // Añade experiencia al GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(xpGiven);
        }

        if (_diedByProjectile)
        {
            // Genera un power-up aleatorio
            SpawnPowerUp();
        }
        // Destruye al enemigo
        Destroy(gameObject);
    }

    private void SpawnPowerUp()
    {
        if (powerUps.Length == 0 || dropChances.Length != powerUps.Length)
        {
            Debug.LogWarning("PowerUps or DropChances are not properly configured.");
            return;
        }

        // Se asegura que las probabilidades sumen hasta 1. Si las probabilidades est�n en porcentaje (0-100), div�delas por 100.
        float randomValue = Random.value; // Valor entre 0 y 1
        float cumulativeProbability = 0f;

        for (int i = 0; i < powerUps.Length; i++)
        {
            // Si las probabilidades est�n entre 0 y 100, div�delas por 100 para normalizarlas a 0-1
            cumulativeProbability += dropChances[i] / 100f;

            // Si randomValue est� dentro del rango acumulado, se genera el power-up
            if (randomValue <= cumulativeProbability)
            {
                Instantiate(powerUps[i], transform.position, Quaternion.identity);
                Debug.Log($"Spawned {powerUps[i].name}");
                break;
            }
        }
    }
    public void setXPgiven(int newXP)
    {
        xpGiven = newXP;
    }
    public int getDamageCollision()
    {
        return collisionDmg;
    }
}
