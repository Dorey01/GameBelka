using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseS : MonoBehaviour
{
    public GameObject panel;
    private CameraFollow cameraFollow;
    public void Start()
    {
        Time.timeScale = 1f;
        cameraFollow = Camera.main.GetComponent<CameraFollow>();


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
        cameraFollow.ResetCam();
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
