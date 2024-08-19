using UnityEngine;

public class Movement1 : MonoBehaviour
{
    public float runSpeed = 40;
    public float horizontalMove = 0;
    public Animator animator;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed",Mathf.Abs(horizontalMove));
    }
}
