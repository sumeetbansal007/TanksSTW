using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float maxSpeed = 1.0f;
    public float speed = 2;
   // public float jumpPower = 150f;

 //   public Transform groundCheck;
    public bool grounded;

    private Rigidbody2D rb2D;
  //  private Animator anim;

	void Start ()
	{
	    rb2D = GetComponent<Rigidbody2D>();
//	    anim = GetComponent<Animator>();
	}
	
	void Update ()
    {

      //  grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

//        anim.SetBool("Grounded", grounded);
//        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

//        if (Input.GetButtonDown("Jump"))
//        {
//            rb2D.AddForce(Vector2.up * jumpPower);
//        }
	}

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        
        rb2D.AddForce( Vector2.right * speed * h);

        if (rb2D.velocity.x > maxSpeed)
        {
            rb2D.velocity = new Vector2(maxSpeed, rb2D.velocity.y );
        }

        if (rb2D.velocity.x < - maxSpeed)
        {
            rb2D.velocity = new Vector2(-maxSpeed, rb2D.velocity.y);
        }
    }
}
