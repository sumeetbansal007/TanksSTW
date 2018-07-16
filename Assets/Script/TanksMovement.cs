using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Networking;
public class TanksMovement : MonoBehaviour
{

    private GameObject terrain;
    public float moveSpeed;
    public float rotationSpeed;
    public GameObject body;
   // private List<Vector3> wayPoints = new List<Vector3>();
    public int targetWayPoint;
    private SpriteShapeController spriteShapeController;
    private Spline spline;
    public bool isMyTurn;
    
    private void Awake()
    {
        terrain = GameObject.Find("Terrain");
        if (terrain != null)
            spriteShapeController = terrain.GetComponent<SpriteShapeController>();
        spline = spriteShapeController.spline;

        //gameObject.transform.position = spline.GetPosition(1);
    }
    

	void Update () {

        if (gameObject.name == "PlayerTank")
        {

            if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveForward();
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveBackward();
            }
            else
            {
                body.GetComponent<Rigidbody2D>().simulated = true;
                gameObject.GetComponent<AudioSource>().Stop();
            }
        }
        else if (gameObject.name == "EnemyTank")
        {
            if (Input.GetKey(KeyCode.D))
            {
                MoveForward();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                MoveBackward();
            }
            else
            {
                gameObject.GetComponent<AudioSource>().Stop();
                body.GetComponent<Rigidbody2D>().simulated = true;

            }
        }
        
           
    }
    void MoveForward()
    {
        if (!isMyTurn)
            return;
        if (targetWayPoint < spline.GetPointCount() - 2)
        {
            gameObject.GetComponent<AudioSource>().Play();
            body.GetComponent<Rigidbody2D>().simulated = false;
            transform.position = Vector3.MoveTowards(transform.position, spline.GetPosition(targetWayPoint), Time.deltaTime * moveSpeed);

            var rotation = Quaternion.LookRotation(transform.position - spline.GetPosition(targetWayPoint));
            rotation.x = 0;
            rotation.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

            if (Vector3.Distance(transform.position, spline.GetPosition(targetWayPoint)) <= 0f)
                targetWayPoint++;
        }
    }
    void MoveBackward()
    {
        if (!isMyTurn)
            return;
        if (targetWayPoint > 1)
        {
            body.GetComponent<Rigidbody2D>().simulated = false;
            transform.position = Vector3.MoveTowards(transform.position, spline.GetPosition(targetWayPoint - 1), Time.deltaTime * moveSpeed);

            var rotation = Quaternion.LookRotation(transform.position - spline.GetPosition(targetWayPoint - 1));
            rotation.x = 0;
            rotation.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

            if (Vector3.Distance(transform.position, spline.GetPosition(targetWayPoint - 1)) <= 0f)
                targetWayPoint--;
        }
    }
   
}
