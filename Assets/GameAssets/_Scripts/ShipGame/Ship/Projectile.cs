using UnityEngine;
using static GlobalVariables;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject explosion;

    private readonly int _explosionHash = Animator.StringToHash("Explosion");

    private AudioSource _explosionSfx;
    private Rigidbody2D _rb;
    private Collider2D _col;
    private Animator _anim;

    private void Awake()
    {
        _rb = transform.GetComponent<Rigidbody2D>();
        _col = transform.GetComponent<Collider2D>();
        _anim = transform.GetComponent<Animator>();
        _explosionSfx = transform.GetComponent<AudioSource>();
    }

    private void Start()
    {
        ActivateProjectile();
    }

    private void OnEnable()
    {
        ActivateProjectile();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && transform.CompareTag("Projectile"))
        {
            DestroyProjectile();
        }

        if (other.CompareTag("Player") && transform.CompareTag("ProjectileEn"))
        {
            DestroyProjectile();
        }
    }

    public float GetDamage()
    {
        if (transform.CompareTag("ProjectileEn") && GlobalVariables.GetDifficulty() != 0)
        {
            damage *= GlobalVariables.GetDifficulty(); //Si es el enemigo hacen mas daño segun la dificultad
        }
        
        return damage;
    }

    public void DestroyProjectile()
    {
        _rb.velocity = Vector2.zero;
        _col.enabled = false;
        projectile.SetActive(false);
        explosion.SetActive(true);
        _explosionSfx.pitch = Random.Range(0.8f, 1.2f);
        _explosionSfx.Play();
    }

    private void ActivateProjectile()
    {
        projectile.SetActive(true);
        explosion.SetActive(false); 
        _rb.velocity = transform.up * speed;
        _col.enabled = true;
    }
}
