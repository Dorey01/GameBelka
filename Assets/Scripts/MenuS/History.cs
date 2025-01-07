using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class History : MonoBehaviour
{
    public GameObject Image1;
    public GameObject Image2;
    int i = 0;
    public void Scip()
    {
        
        Image1.SetActive(false);
        Image2.SetActive(true);
        if( i!= 0)
        {
            SceneManager.LoadScene("Level1.1");
        }
        else
        {
            i = 1;
        }
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

}
