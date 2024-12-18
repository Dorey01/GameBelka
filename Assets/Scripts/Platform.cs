using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    float dirY;
    float speed = 3f;

    bool moveingRught = true;
    private void Update()
    {
        if (transform.position.x > 4f)
        {
                moveingRught = false;
            
        }
        else if(transform.position.x < -4f){
            moveingRught = true;
        }
        if (moveingRught) {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y );
        }
        else
        {
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }


}
