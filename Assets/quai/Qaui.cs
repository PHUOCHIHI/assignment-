using UnityEngine;
using UnityEngine.UI;

public class QuaiController : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Transform player;
    private Animator animator;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthBar;

    [Header("Detection Settings")]
    public float detectionRange = 5f; // Khoảng cách phát hiện player
    private bool isTouchingPlayer = false;
    private bool isDead = false;

    [Header("Knockback Settings")]
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.2f;
    private bool isKnockback = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
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
        if (isDead || isKnockback) return;

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
        if (isDead) return;

        currentHealth -= damage;
        if (healthBar != null)
            healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(ApplyKnockback());
        }
    }

    System.Collections.IEnumerator ApplyKnockback()
    {
        isKnockback = true;

        Vector2 direction = (transform.position - player.position).normalized;
        rb.velocity = direction * knockbackForce;

        yield return new WaitForSeconds(knockbackDuration);

        rb.velocity = Vector2.zero;
        isKnockback = false;
    }

    void Die()
    {
        isDead = true;

        animator.SetTrigger("isdie"); // Gọi animation chết
        animator.SetBool("isrun", false);
        ResetAttack();

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        this.enabled = false;

        Destroy(gameObject, 2f); // Hủy sau 2 giây
    }
}
