using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Normalizamos para diagonales
        Vector2 move = new Vector2(moveX, moveY).normalized;

        // Pasamos valores al Animator
        animator.SetFloat("MoveX", move.x);
        animator.SetFloat("MoveY", move.y);

        // Si hay movimiento
        bool isMoving = move != Vector2.zero;
        animator.SetBool("IsMoving", isMoving);
    }
}
