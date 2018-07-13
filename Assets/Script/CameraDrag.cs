using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

        if (transform.position.x <= 9f && transform.position.x >= -9f)
        {
            transform.Translate(move, Space.World);
        }
        else
        {
            if (transform.position.x > 9f)
                transform.position = new Vector3(9, transform.position.y, transform.position.z);
            if (transform.position.x < -9f)
                transform.position = new Vector3(-9f, transform.position.y, transform.position.z);

        }

    }


}