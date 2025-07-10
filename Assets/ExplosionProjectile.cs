using UnityEngine;

public class ExplosionProjectile : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    [SerializeField] int damage = 4;
    [SerializeField] float explosionRadius = 3f;
    [SerializeField] float lifetime = 3f;
    [SerializeField] GameObject explosionEffect; // Efecto visual de explosi�n

    private Vector2 direction;
    private Rigidbody2D rb;
    private bool hasExploded = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Explotar autom�ticamente despu�s del tiempo de vida
        Invoke("Explode", lifetime);
    }

    private void Start()
    {
        // Mover el proyectil en la direcci�n establecida
        rb.linearVelocity = direction * speed;
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    public void SetExplosionRadius(float radius)
    {
        explosionRadius = radius;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Explotar al impactar con el jugador o obst�culos
        if (other.CompareTag("Player"))
        {
            if (!hasExploded)
            {
                Explode();
            }
        }
    }

    private void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        // Crear efecto visual de explosi�n
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Buscar todos los objetos en el radio de explosi�n
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D obj in objectsInRange)
        {
            // Da�ar al jugador si est� en el rango
            if (obj.CompareTag("Player"))
            {
                Character character = obj.GetComponent<Character>();
                if (character != null)
                {
                    character.TakeDamage(damage);
                }
            }
        }

        // Destruir el proyectil
        Destroy(gameObject);
    }

    // Visualizar el radio de explosi�n en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}