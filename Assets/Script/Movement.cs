using System;
using System.Threading;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public bool moving;
    public float thrust = 1f;
    public enum MovementMode
    {
        Up,   //=0
        Right,//=1
        Left,  //=2
        Stand, //=3
        Lose
    };

    public MovementMode movemode;
    public GameObject winScreen, loseScreen;
    void FixedUpdate()
    {
        if (moving&& movemode != MovementMode.Lose)
        {
            Move();
        }
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Up Left");
            movemode = MovementMode.Up;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1); 
            thrust = 3;
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * thrust, ForceMode2D.Impulse);
            moving = false;
            thrust = 1;
        }

        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Up Right");
            movemode = MovementMode.Up;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.one;
            thrust = 3;
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.one * thrust, ForceMode2D.Impulse);
            moving = false;
            thrust = 1;
        }

        else if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("Up");
            movemode = MovementMode.Up;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.up;
            thrust = 3;
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrust, ForceMode2D.Impulse);
            moving = false;
            thrust = 1;
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Left");
            movemode = MovementMode.Left;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.left;
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.left * thrust ,ForceMode2D.Impulse);
            moving = false;
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Right");
            movemode = MovementMode.Right;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.right;
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrust, ForceMode2D.Impulse);
            moving = false;
        }

        else{
            Debug.Log("Stand");
            movemode = MovementMode.Stand;
            return;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            moving = true;
        }
        else if(collision.gameObject.CompareTag("Ground End")){
            GroundColliderOff[] groundColliders = FindObjectsOfType<GroundColliderOff>();
            foreach (var ground in groundColliders)
            {
                ground.OffCollider();
            }
            moving = true;
            movemode = MovementMode.Lose;
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
