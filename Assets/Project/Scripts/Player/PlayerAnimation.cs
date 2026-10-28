using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PlayerController player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb= GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerController>();

    }
    private void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("VelocityY", rb.linearVelocity.y);
        animator.SetBool("Is Grounded", player.IsGrounded);
       
    }

}
