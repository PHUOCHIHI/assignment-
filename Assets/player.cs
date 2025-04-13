using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float lastXTime = 0f;
    private float doubleTapThreshold = 0.3f;
    public Transform attackArea; // Gán trong Inspector

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    // Di chuyển
    moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
            attackArea.localPosition = new Vector3(1f, 0, 0); // Đặt phía phải Player
        }
        else if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
            attackArea.localPosition = new Vector3(-1f, 0, 0); // Đặt phía trái Player
        }



        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        animator.SetBool("IsRunning", isMoving);

        // --- Tấn công ---
        if (Input.GetKeyDown(KeyCode.F)) TriggerAttack("Isatk1");
        else if (Input.GetKeyDown(KeyCode.G)) TriggerAttack("Isatk2");
        else if (Input.GetKeyDown(KeyCode.X))
        {
            if (Time.time - lastXTime < doubleTapThreshold)
                TriggerAttack("Iscombo");

            lastXTime = Time.time;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput.normalized * speed;
    }

    void TriggerAttack(string paramName)
    {
        StartCoroutine(AttackCoroutine(paramName));
    }

    System.Collections.IEnumerator AttackCoroutine(string paramName)
    {
        ResetAllAttackBools();
        yield return null;

        animator.SetBool(paramName, true);
        yield return null;
        animator.SetBool(paramName, false);
    }
        
    void ResetAllAttackBools()
    {
        animator.SetBool("Isatk1", false);
        animator.SetBool("Isatk2", false);
        animator.SetBool("Iscombo", false);
    }
}
