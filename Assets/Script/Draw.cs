using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Draw : MonoBehaviour
{
    public float minimumDistance;
    private Vector3 lastPosition;
    float leastDistance = 100;
    int index = 0;

    public GameObject tank;
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.instance.currentIndexOfBomb == 0)
        {
            ContactPoint2D contact = collision.contacts[0];
            Vector3 pos = contact.point;
            Debug.Log("Hit Point " + pos);
            Destroy(collision.collider.gameObject);
            GenerateSpline(new Vector3(pos.x,pos.y-0.5f,pos.z));
        }
    }

    void GenerateSpline(Vector3 pos)
    {
         if (gameObject.GetComponent<SpriteShapeController>() != null)
        {
            var f = Mathf.Abs((pos - lastPosition).magnitude);
            if ( f > minimumDistance)
            {
                var spriteShapeController = gameObject.GetComponent<SpriteShapeController>();
                var spline = spriteShapeController.spline;
                
                for (int i = 0; i < spline.GetPointCount(); i++)
                {
                    Vector2 splinePoint = spline.GetPosition(i);
                   // Debug.Log("Vector Distance" + Vector2.Distance(splinePoint, m));
                    if (leastDistance > Vector2.Distance(splinePoint, pos))
                    {
                        leastDistance = Vector2.Distance(splinePoint, pos);
                        if (splinePoint.x > pos.x)
                            index = i - 1;
                        else
                        index = i;
                        //Debug.Log("MinDistance" + leastDistance +" , index "+i +" ,"+" Point "+spline.GetPosition(i));
                    }
                }
                spline.InsertPointAt(index+1, new Vector3(pos.x, pos.y, pos.z));
                var newPointIndex = index+1; //spline.GetPointCount() - 1;
                
                spline.SetTangentMode(newPointIndex, ShapeTangentMode.Continuous);
                spline.SetLeftTangent(newPointIndex, new Vector3(-1f,0,0));
                spline.SetRightTangent(newPointIndex, new Vector3(1f, 0, 0));
                spline.SetHeight(newPointIndex, 1f);
                spline.SetBevelCutoff(newPointIndex, 180f);
                lastPosition = new Vector3(pos.x, pos.y - 8, pos.z);
                leastDistance = 100f;
                //spline.SetBevelCutoff(newPointIndex, 82.0f);

                spriteShapeController.BakeCollider();
                //if(tank!=null)
                //tank.GetComponent<TanksMovement>().GenerateWayPoints();
            }
        }
    }
   
}
