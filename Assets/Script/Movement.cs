using System.Collections;
using UnityEngine;
public class Movement : MonoBehaviour
{
    public bool moving;
    public float thrust = 1.5f;

    public GameObject winScreen, loseScreen;
    private Rigidbody2D _rb;
    private Vector2 _force;
    public Animator animator;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        animator.SetBool("isJump",false);
        animator.SetBool("isTurnRight",true);
    }

    void Update()
    {
        if (moving)
        {
            Move();
        }
    }

    void Move()
    {
        animator.SetFloat("Speed",Mathf.Abs(_force.x*thrust));

        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            animator.Play("Jump Left Animation");
            animator.SetBool("isJump",true);
            animator.SetBool("isTurnRight",false);
            _rb.velocity = new Vector2(-1, 1); 
            thrust = 3;
            _force = new Vector2(-1, 1);
            _rb.AddForce(_force * thrust, ForceMode2D.Impulse);
            thrust = 1.5f;
            moving = false;
        }

        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            animator.Play("Jump Right Animation");
            animator.SetBool("isJump",true);
            animator.SetBool("isTurnRight",true);
            _rb.velocity = Vector2.one;
            thrust = 3;
            _force = Vector2.one;
            _rb.AddForce(_force * thrust, ForceMode2D.Impulse);
            thrust = 1.5f;
            moving = false;
        }

        else if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("isJump",true);
            _rb.velocity = Vector2.up;
            thrust = 3;
            _force = Vector2.up;
            _rb.AddForce(_force * thrust, ForceMode2D.Impulse);
            thrust = 1.5f;
            moving = false;
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.Play("Go Left Animation");
            animator.SetBool("isTurnRight",false);
            animator.SetBool("isJump",false);
            _rb.velocity = Vector2.left;
            moving = false;
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        { 
            animator.Play("Go Right Animation");
            animator.SetBool("isTurnRight",true);
            animator.SetBool("isJump",false);
            _rb.velocity = Vector2.right;
            moving = false;
        }

        else
        {
            animator.SetFloat("Speed",0);
            animator.SetBool("isJump",false);
        }

    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJump",false);
            moving = true;
        }
        else if(collision.gameObject.CompareTag("Ground End")){
            GroundColliderOff[] groundColliders = FindObjectsOfType<GroundColliderOff>();
            foreach (var ground in groundColliders)
            {
                ground.OffCollider();
            }
            moving = true;
            thrust = 10;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.up;
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrust ,ForceMode2D.Impulse);
            StartCoroutine(Lose());
        }
        else if(collision.gameObject.CompareTag("Finish")){
            moving = false;
            GroundColliderOff[] groundColliders = FindObjectsOfType<GroundColliderOff>();
            foreach (var ground in groundColliders)
            {
                ground.gameObject.tag = "Untagged";
            }
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            thrust = 1;
            Debug.Log("Add force");
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.left * thrust ,ForceMode2D.Impulse);
            StartCoroutine(Win());
        }
        else moving = false;
    }

    IEnumerator Win(){
        yield return new WaitForSeconds(3);
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator Lose(){
        yield return new WaitForSeconds(3);
        loseScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
