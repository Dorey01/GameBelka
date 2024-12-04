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
        if (transform.position.y > 4f)
        {
                moveingRught = false;
            
        }
        else if(transform.position.y < -4f){
            moveingRught = true;
        }
        if (moveingRught) {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }
    }


}
