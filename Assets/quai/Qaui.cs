using UnityEngine;
using UnityEngine.UI;

public class QuaiController : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Transform player;
    private Animator animator;
    private SpriteRenderer sprite;

    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthBar;

    [Header("Detection Settings")]
    public float detectionRange = 5f; // Khoảng cách phát hiện player
    private bool isTouchingPlayer = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && !isTouchingPlayer)
        {
            Vector3 dir = player.position - transform.position;
            transform.position += dir.normalized * moveSpeed * Time.deltaTime;

            sprite.flipX = dir.x < 0;

            animator.SetBool("isrun", true);
            ResetAttack();
        }
        else if (isTouchingPlayer)
        {
            animator.SetBool("isrun", false);

            if (Random.value < 1f)
                animator.SetBool("isatk1", true);
            else
                animator.SetBool("isatk2", true);
        }
        else
        {
            animator.SetBool("isrun", false);
            ResetAttack();
        }
    }

    void ResetAttack()
    {
        animator.SetBool("isatk1", false);
        animator.SetBool("isatk2", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isTouchingPlayer = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isTouchingPlayer = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthBar != null)
            healthBar.value = currentHealth;

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
