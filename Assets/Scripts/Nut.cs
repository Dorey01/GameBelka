using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NutS : MonoBehaviour
{
    private float Nut = 0;
    public Text NutText;
    public AudioSource NutSound;





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Nut")
        {
            Nut++;
            NutText.text = Nut.ToString();
            Destroy(collision.gameObject);

        }
    }


}
