using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CannonScript : MonoBehaviour 
{
	public GameObject trajectoryPointPrefeb;
	public GameObject bombPrefab;
    public Transform initPos;
	
	private GameObject bombObj;
	private bool isPressed, isBallThrown;
	private float power = 25;
	private int numOfTrajectoryPoints = 10;
	private List<GameObject> trajectoryPoints;
	//---------------------------------------	
	void Start ()
	{
		trajectoryPoints = new List<GameObject>();
		isPressed = isBallThrown = false;
		for(int i=0;i<numOfTrajectoryPoints;i++)
		{
			GameObject dot= (GameObject) Instantiate(trajectoryPointPrefeb);
			dot.GetComponent<Renderer>().enabled = false;
			trajectoryPoints.Insert(i,dot);
		}
	}
	//---------------------------------------	
	void Update () 
	{

        if (isBallThrown)
        {
           return;
        }
		if(Input.GetMouseButtonDown(0))
		{
			isPressed = true;
				GenerateBomb();
		}
		else if(Input.GetMouseButtonUp(0))
		{
			isPressed = false;
			if(!isBallThrown)
			{
                HideTrajectoryPoints();
                ShootBomb();
			}
		}
		if(isPressed)
		{
			Vector3 vel = GetForceFrom(bombObj.transform.position,Camera.main.ScreenToWorldPoint(Input.mousePosition));
            float angle = 0;
            if(gameObject.name == "PlayerTurret")
            {
                angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0,0,angle);
            }
            else
            {
                angle = Mathf.Atan2(vel.y, -vel.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0,0,-angle);           
            }
               
            SetTrajectoryPoints(initPos.position, vel);
		}
	}
	//---------------------------------------	
	// When ball is thrown, it will create new ball
	//---------------------------------------	
	private void GenerateBomb()
	{
		bombObj = (GameObject) Instantiate(bombPrefab);
		Vector3 pos = transform.position;
        bombObj.transform.SetParent(null);
		pos.z=1;
		bombObj.transform.position = initPos.position;
		bombObj.SetActive(false);
	}
	//---------------------------------------	
	private void ShootBomb()
	{
		bombObj.SetActive(true);	
		bombObj.GetComponent<Rigidbody2D>().gravityScale = 1;
        bombObj.GetComponent<Rigidbody2D>().AddForce(GetForceFrom(bombObj.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)), ForceMode2D.Impulse);
		isBallThrown = true;
        Invoke("DestroyBomb", 2f);
    }

    void DestroyBomb()
    {
        Destroy(bombObj);
        isBallThrown = false;
        GenerateBomb();
        isPressed = true;
    }
    //---------------------------------------	
    private Vector2 GetForceFrom(Vector3 fromPos, Vector3 toPos)
	{
		return (new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y))*power;//*ball.rigidbody.mass;
	}
	//---------------------------------------	
	// It displays projectile trajectory path
	//---------------------------------------	
	void SetTrajectoryPoints(Vector3 pStartPosition , Vector3 pVelocity )
	{
		float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));

        float angle = 0;
            angle =  Mathf.Rad2Deg*(Mathf.Atan2(pVelocity.y , pVelocity.x));
        
		float fTime = 0;
		
		fTime += 0.1f;
		for (int i = 0 ; i < numOfTrajectoryPoints ; i++)
		{
			float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
			float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
			Vector3 pos = new Vector3(pStartPosition.x + dx , pStartPosition.y + dy ,2);
			trajectoryPoints[i].transform.position = pos;
			trajectoryPoints[i].GetComponent<Renderer>().enabled = true;  
            if(gameObject.name == "PlayerTurret")
            {
                trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude) * fTime, pVelocity.x) * Mathf.Rad2Deg);
            }
            else
            {
                trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude) * fTime, pVelocity.x) * Mathf.Rad2Deg);
            }
           
            fTime += 0.1f;
		}
	}

    void HideTrajectoryPoints()
    {
        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {            
            trajectoryPoints[i].GetComponent<Renderer>().enabled = false;           
        }
    }
}