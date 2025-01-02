using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public Canvas menu;
    public Canvas opshen;
    public Canvas level;
    public void PlayGame_Level1()
    {
        SceneManager.LoadScene("Level1.1");
    }
    public void PlayGame_Level2()
    {
        SceneManager.LoadScene("Level1.2");
    }
    public void PlayGame_Level3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void Levls()
    {
        menu.enabled = false;
        level.enabled = true;
    }


    public void Management()
    {
        menu.enabled = false;
        opshen.enabled = true;
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            menu.enabled = true;
            level.enabled = false;
            opshen.enabled = false;
        }
    }

}
