using UnityEngine;

public class PlayerExample : MonoBehaviour {

    public float moveSpeed;
    Vector3 moveVector;
    //public Joystick joystick;

    void Update () 
	{
       
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVector = (transform.right + transform.forward).normalized;
            transform.Translate(moveVector * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveVector = (-transform.right -transform.forward).normalized;
            transform.Translate(moveVector * moveSpeed * Time.deltaTime);
        }
    }
}