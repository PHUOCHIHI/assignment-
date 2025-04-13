using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 20;
    public bool isAttacking = false;
    private bool hasDealtDamage = false;

    void Start()
    {
        // Đảm bảo Collider đang tắt lúc đầu
        GetComponent<Collider2D>().enabled = false;
    }

    public void StartAttack()
    {
        isAttacking = true;
        hasDealtDamage = false;

        GetComponent<Collider2D>().enabled = true;
    }

    public void EndAttack()
    {
        isAttacking = false;
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isAttacking || hasDealtDamage) return;

        if (other.CompareTag("Enemy"))
        {
            QuaiController enemy = other.GetComponent<QuaiController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                hasDealtDamage = true;
            }
        }
    }
}
