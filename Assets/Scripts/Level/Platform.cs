using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{


    public float speed = 3f;
    public float dirYUp = 1f;
    public float dirYDown = 1f;
    private float startY;
    bool moveingRught = true;
    private void Start()
    {
        startY = transform.position.y;
    }
    private void Update()
    {
        if (transform.position.y > (startY + dirYUp))
        {
                moveingRught = false;
            
        }
        else if(transform.position.y < (startY - dirYDown)){
            moveingRught = true;
        }
        if (moveingRught) {
            transform.position = new Vector3(transform.position.x , transform.position.y + speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }
    }


}
