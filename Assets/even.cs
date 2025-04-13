using UnityEngine;

public class PlayerAnimatorEvents : MonoBehaviour
{
    private PlayerAttack attackScript;

    void Start()
    {
        attackScript = GetComponentInChildren<PlayerAttack>();
    }

    public void StartAttack()
    {
        attackScript?.StartAttack();
    }

    public void EndAttack()
    {
        attackScript?.EndAttack();
    }
}
