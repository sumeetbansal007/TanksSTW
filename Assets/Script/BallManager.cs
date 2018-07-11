using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallManager : MonoBehaviour
{
	public GameObject bombPrefab;
	GameObject bombObj;
	public Transform BombInitialPos;
    public bool isPressed, isBallThrown;
    private float power = 2.3f;
    public int numOfTrajectoryPoints = 20;
    public List<GameObject> trajectoryPoints;
    public bool DestroyPointsFlag = false;
    GameObject dot;
    public GameObject TrajectoryPointPrefeb;
    Ray ray; //A ray is an infinite line starting at origin and going in some direction.
    RaycastHit hit;
    public bool setPoints = false;
    public bool callonce = false;
    
    

    void Awake()
    {
   //     originalConstraints = GetComponent<Rigidbody2D>().constraints;
    }

    //---------------------------------------	
    void Start()
    {
		
        trajectoryPoints = new List<GameObject>();
        isPressed = isBallThrown = false;
    }
   
    void Update()
    {
        gameplay();

    }
    void gameplay()
    {
        //to get information of objects hitting to ray genrated from mouse cursor
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && !isPressed)
        {
            isPressed = true;
			bombObj = Instantiate (bombPrefab, BombInitialPos.position, Quaternion.identity);
			bombObj.GetComponent<SpriteRenderer> ().enabled = false;
			bombObj.GetComponent<Rigidbody2D> ().gravityScale = 0;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            for (int i = 0; i < numOfTrajectoryPoints; i++)
            {
                Destroy(trajectoryPoints[i]);
            }
           
            isPressed = false;

            if (!isBallThrown)
            {
                throwBall();         
            }
        }

        if (isPressed  && !isBallThrown)
        {
            if (!callonce)
            {
                //	print("called once");
                for (int i = 0; i < numOfTrajectoryPoints + 1; i++)
                {
                    dot = Instantiate(TrajectoryPointPrefeb) as GameObject;
                    dot.GetComponent<SpriteRenderer>().enabled = false;
                    trajectoryPoints.Insert(i, dot);

                }
                callonce = true;
            }
        }

        if (isPressed == true)
        {
            Vector2 vel = GetForceFrom(new Vector2(bombObj.transform.position.x, bombObj.transform.position.y), Camera.main.ScreenToWorldPoint(Input.mousePosition));
            setTrajectoryPoints(transform.position, vel / bombObj.GetComponent<Rigidbody2D>().mass);
			bombObj.gameObject.transform.rotation = Quaternion.Slerp(bombObj.transform.rotation, Quaternion.Euler(0, 0, 0), 1 * Time.deltaTime);
        }
    }

    
   
    private void throwBall()
    {
		bombObj.GetComponent<SpriteRenderer> ().enabled = true;
		bombObj.GetComponent<Rigidbody2D>().gravityScale = 1;
	//	bombObj.gameObject.GetComponent<Rigidbody2D>().constraints = originalConstraints;
		//bombObj.GetComponent<Rigidbody2D>().AddForce(GetForceFrom(bombObj.transform.position * 50 , Camera.main.ScreenToWorldPoint(Input.mousePosition)));
		bombObj.GetComponent<Rigidbody2D>().AddForce(GetForceFrom(bombObj.transform.position * 100 , Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        isBallThrown = true;
        Invoke("DestroyBomb", 2f);
    }

    void DestroyBomb()
    {
        Destroy(bombObj);
        isBallThrown = false;
        isPressed = false;
        callonce = false;
    }

    private Vector2 GetForceFrom(Vector2 fromPos, Vector2 toPos)
    {
        return (new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y)) * power;
    }

    public float angle;
    public float velocity;
    //---------------------------------------	
    // It displays projectile trajectory path
    //---------------------------------------	
    void setTrajectoryPoints(Vector2 pStartPosition, Vector2 pVelocity)
    {
        velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));

        angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
        float fTime = 0;
        fTime += 0.08f;

        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {

            float dx = (velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad));
            float dy = (velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f));

            Vector2 pos = new Vector2(pStartPosition.x + dx, pStartPosition.y + dy);

            if (trajectoryPoints.Count > 0 && trajectoryPoints[i].gameObject != null)
            {
                trajectoryPoints[i].transform.position = pos;
                trajectoryPoints[i].GetComponent<SpriteRenderer>().enabled = true;
                fTime += 0.08f; // previous value was 0.08f
            }

        }
       DestroyPointsFlag = false;
    }

 

}