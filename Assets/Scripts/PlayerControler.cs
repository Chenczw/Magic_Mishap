using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    //"Public" variables: SerializeField - 目的为了别人看不到但是自己可以在Unity里面改
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpForce = 500.0f;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask whatIsGround;

    //private variable
    private Rigidbody2D rBody;
    private Animator anim;
    private bool isGrounded = false;
    private bool isFacingRight = true;
    private bool hasBeenHit = false;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    //Physics
    private void FixedUpdate()
    {
        float horiz = Input.GetAxis("Horizontal");
        isGrounded = GroundCheck();

        //jump code. 
        /*一般在 &&情况下无所谓顺序，但是 isGrounded 是一个 bool 类型的数据。放前面对于运算更简便
         所以在这里顺序很重要*/
        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            rBody.AddForce(new Vector2(0.0f, jumpForce));
            isGrounded = false;
        }

        rBody.velocity = new Vector2(horiz * speed, rBody.velocity.y);

        //Check is the sprite needs to be flipped
        if((isFacingRight && rBody.velocity.x < 0) || (!isFacingRight && rBody.velocity.x>0))
        {
            Flip();
        }
        
        //Commuicate with the animator
        anim.SetFloat("xVelocity", Mathf.Abs(rBody.velocity.x));
        anim.SetFloat("yVelocity", rBody.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround);
    }

    //转身
    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;

        isFacingRight = !isFacingRight;
    }

    // Detect when another object collides with the player
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Delete the "other" object if it as an object
        if (other.gameObject.CompareTag("Object"))
        {
            ScoreScript.scoreValue += ScoreScript.gemScoreIncrement;
            Destroy(other.gameObject);
        }

        // Trigger "die" animation if the player has hit the enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            hasBeenHit = true;
            // Commuicate with the animator
            anim.SetBool("hasBeenHit", hasBeenHit);
        }
    }
}