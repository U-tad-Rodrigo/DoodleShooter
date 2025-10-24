using UnityEngine;

public class PlayerHealth : HealthGlobal
{
    [SerializeField] private GameObject deathCanvas;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private ShipClassController shipClassController;
    
    
    private void Start()
    {
        deathCanvas.SetActive(false);
        inputManager.SwitchUiToPlayer(); //Asegura que el jugador tenga control al empezar la ronda
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (shipClassController.GetIsShielded() == false)
        {
            if (other.CompareTag("Enemy"))
            {
                var enemy = other.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    int damageCol = enemy.getDamageCollision(); // Obtén el daño del enemigo
                    Debug.Log($"Taken damage: {damageCol}");
                    TakeDamage(damageCol); // Pasa el daño al método TakeDamage
                }
            } 
            else if (other.CompareTag("ProjectileEn"))
            {
                var enProjectile = other.GetComponent<Projectile>();
                if(enProjectile != null)
                {
                    float damage = enProjectile.GetDamage();
                    Debug.Log($"Taken damage: {damage}");
                    TakeDamage(damage);
                }
            } 
            else
            {
                Debug.Log("No Damage Taken");
            }
        }
    }

    protected override void Death() //Al morir el jugador se desactivan sus controles y se matan a todos los enemigos
    {
        if (shipClassController.GetIsShielded() == false && shipClassController.GetIsDouble() == false)
        {
            Debug.Log("Player is dead");
            inputManager.SwitchPlayerToUi();
            deathCanvas.SetActive(true);
        }
    }
}