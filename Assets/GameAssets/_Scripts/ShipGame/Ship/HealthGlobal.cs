using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthGlobal : MonoBehaviour
{
    [SerializeField] protected int maxHealth; // Vida m�xima
    [SerializeField] protected float health;    // Vida actual
    [SerializeField] private Image healthBar; // Barra de vida

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        CheckDeath();
        UpdateHealthBar();
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth); // Evita exceder la vida m�xima
        UpdateHealthBar();
    }

    protected virtual void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = health / (float)maxHealth; // Actualiza la barra de vida
        }
    }
    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(int newHealth) 
    {
        health = newHealth;
        UpdateHealthBar();
    }

    public void SetMaxHealth()
    {
        health = maxHealth;
        UpdateHealthBar();
    }
    protected virtual void CheckDeath()
    {
        if (health <= 0)
        {
            SetHealth(0); 
            Death();
        }
    }

    protected virtual void Death()
    { 
        Debug.Log(transform.gameObject.name + " is dead."); // Mensaje generico de muerte
    }
}
