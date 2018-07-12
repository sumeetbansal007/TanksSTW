using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TanksMovement : MonoBehaviour {

    private GameObject terrain;
    public float moveSpeed;
    public float rotationSpeed;

    private List<Vector3> wayPoints = new List<Vector3>();
    private int targetWayPoint = 1 ;
    private SpriteShapeController spriteShapeController;
    private Spline spline;

    // Use this for initialization
    void Start () {
        terrain = GameObject.Find("Terrain");
        if (terrain != null)
        spriteShapeController = terrain.GetComponent<SpriteShapeController>();
        spline = spriteShapeController.spline;

        GenerateWayPoints();
	}

    public void GenerateWayPoints()
    {
        wayPoints.Clear();
        for (int i = 0; i < spline.GetPointCount(); i++)
        {
            wayPoints.Add(new Vector3(spline.GetPosition(i).x, spline.GetPosition(i).y , spline.GetPosition(i).z));
        }
    }
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (targetWayPoint < spline.GetPointCount() - 2)
            {
                transform.position = Vector3.MoveTowards(transform.position, wayPoints[targetWayPoint], Time.deltaTime * moveSpeed);

                var rotation = Quaternion.LookRotation(transform.position - wayPoints[targetWayPoint]);
                rotation.x = 0;
                rotation.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

                if (Vector3.Distance(transform.position, wayPoints[targetWayPoint]) <= 0f)
                    targetWayPoint++;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (targetWayPoint > 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, wayPoints[targetWayPoint - 2], Time.deltaTime * moveSpeed);

                var rotation = Quaternion.LookRotation(transform.position - wayPoints[targetWayPoint - 2]);
                rotation.x = 0;
                rotation.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

                if (Vector3.Distance(transform.position, wayPoints[targetWayPoint - 2]) <= 0f)
                    targetWayPoint--;
            }
        }

    }
}
