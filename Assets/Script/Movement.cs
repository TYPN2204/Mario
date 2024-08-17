using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool moving;
    public float thrust = 1f;
    public enum MovementMode
    {
        Up,   //=0
        Right,//=1
        Left  //=2
    };

    public MovementMode movemode;
    void FixedUpdate()
    {
        if (moving)
        {
            Move();
        }
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movemode = MovementMode.Up;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.up;
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrust, ForceMode2D.Impulse);
            moving = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movemode = MovementMode.Left;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.left;
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.left * thrust ,ForceMode2D.Impulse);
            moving = false;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            movemode = MovementMode.Right;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.right;
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrust, ForceMode2D.Impulse);
            moving = false;
        }

        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            movemode = MovementMode.Up;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1); 
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * thrust, ForceMode2D.Impulse);
            moving = false;
        }

        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            movemode = MovementMode.Up;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.one;
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.one * thrust, ForceMode2D.Impulse);
            moving = false;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            moving = true;
        }
    }
}
