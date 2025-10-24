using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] HealthGlobal _health;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _health.TakeDamage(10); //daño temporal (luego se cambia)
        Debug.Log("Taking Damage");
    }
}
