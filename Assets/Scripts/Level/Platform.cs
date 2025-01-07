using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public bool up = false;
    public float speed = 3f;
    public float dirYUp = 1f;
    public float dirYDown = 1f;
    public float dirXR = 1f;
    public float dirXL = 1f;
    private float startY;
    private float startX;
    public bool moveingRught = true;
    private void Start()
    {
        startY = transform.position.y;
        startX = transform.position.x;
    }
    private void Update()
    {
        if (!up)
        {
            if (transform.position.y > (startY + dirYUp))
            {
                moveingRught = false;

            }
            else if (transform.position.y < (startY - dirYDown))
            {
                moveingRught = true;
            }
            if (moveingRught)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime);
            }
        }
        else
        {
            if (transform.position.x > (startX + dirXR))
            {
                moveingRught = false;

            }
            else if (transform.position.x < (startX - dirXL))
            {
                moveingRught = true;
            }
            if (moveingRught)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y );
            }
            else
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y);
            }
        }
    }


}
