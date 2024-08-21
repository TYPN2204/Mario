using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool moving;
    private float thrust = 1.5f;
    private float playerVx = 0f;
    public float jump = 3f;
    public float aV = 1f;
    public float maxVx = 4f;

    public GameObject winScreen, loseScreen;
    private Rigidbody2D _rb;
    private Vector2 _force;
    public Animator animator;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        animator.SetBool("isJump", false);
        animator.SetBool("isTurnRight", true);
    }

    void Update()
    {
        HandleMovementInput();
    }

    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            playerVx += Time.deltaTime * aV;
        }
        else
        {
            playerVx -= Time.deltaTime * aV;
        }
        
        playerVx = Mathf.Clamp(playerVx, 0, maxVx);

        if (moving)
        {
            Move();
        }
    }

    void Move()
    {
        animator.SetFloat("Speed", Mathf.Abs(playerVx));

        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            animator.Play("Jump Left Animation");
            animator.SetBool("isJump", true);
            animator.SetBool("isTurnRight", false);
            _rb.velocity = new Vector2(-playerVx, jump); 
            moving = false;
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            animator.Play("Jump Right Animation");
            animator.SetBool("isJump", true);
            animator.SetBool("isTurnRight", true);
            _rb.velocity = new Vector2(playerVx, jump);
            moving = false;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("isJump", true);
            _rb.velocity = new Vector2(0, jump);
            moving = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.Play("Go Left Animation");
            animator.SetBool("isTurnRight", false);
            animator.SetBool("isJump", false);
            animator.SetBool("isMoving", true);
            _rb.velocity = new Vector2(-playerVx, 0);
            moving = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.Play("Go Right Animation");
            animator.SetBool("isTurnRight", true);
            animator.SetBool("isJump", false);
            animator.SetBool("isMoving", true);
            _rb.velocity = new Vector2(playerVx, 0);
            moving = false;
        }
        else
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isJump", false);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJump", false);
            moving = true;
        }
        else if (collision.gameObject.CompareTag("Ground End"))
        {
            GroundColliderOff[] groundColliders = FindObjectsOfType<GroundColliderOff>();
            foreach (var ground in groundColliders)
            {
                ground.OffCollider();
            }
            moving = true;
            thrust = 7;
            _rb.velocity = Vector2.up;
            _rb.AddForce(Vector2.up * thrust, ForceMode2D.Impulse);
            StartCoroutine(Lose());
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            moving = false;
            GroundColliderOff[] groundColliders = FindObjectsOfType<GroundColliderOff>();
            foreach (var ground in groundColliders)
            {
                ground.gameObject.tag = "Untagged";
            }
            _rb.velocity = Vector2.zero;
            thrust = 1;
            _rb.AddForce(Vector2.left * thrust, ForceMode2D.Impulse);
            StartCoroutine(Win());
        }
        else moving = false;
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(3);
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator Lose()
    {
        yield return new WaitForSeconds(3);
        loseScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
