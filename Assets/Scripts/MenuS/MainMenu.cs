using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public Canvas menu;
    public Canvas level;
    public void PlayGame_Level1()
    {
        SceneManager.LoadScene("Lavel1");
    }
    public void PlayGame_Level2()
    {
        SceneManager.LoadScene("Lavel2");
    }
    public void PlayGame_Level3()
    {
        SceneManager.LoadScene("Lavel3");
    }
    public void Levls()
    {
        menu.enabled = false;
        level.enabled = true;
    }
    
}
