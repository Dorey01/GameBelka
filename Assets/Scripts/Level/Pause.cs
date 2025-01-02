using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseS : MonoBehaviour
{
    public GameObject panel;

    public void Start()
    {
        Time.timeScale = 1f;
    }
    public void Puse()
    {
        if (panel.active == true)
        {
            panel.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            panel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void StartPuse()
    {

            panel.SetActive(false);
            Time.timeScale = 1f;

    }

    public void Menu()
    {
        
        SceneManager.LoadScene("Menu");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panel.active == true)
            {
                panel.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                panel.SetActive(true);
                Time.timeScale = 0f;
            }
     
        }
    }
}
