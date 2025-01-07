using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Меню")]
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas optionsCanvas;
    [SerializeField] private Canvas levelsCanvas;

    #region Управление меню
    public void ShowMainMenu()
    {
        menuCanvas.enabled = true;
        optionsCanvas.enabled = false;
        levelsCanvas.enabled = false;
    }

    public void ShowLevels()
    {
        menuCanvas.enabled = false;
        optionsCanvas.enabled = false;
        levelsCanvas.enabled = true;
    }

    public void ShowOptions()
    {

        menuCanvas.enabled = false;
        optionsCanvas.enabled = true;
        levelsCanvas.enabled = false;
    }
    #endregion

    #region Загрузка уровней
    public void NewGame()
    {
        SceneManager.LoadScene("History2");
        LevelManager.Instance.ClearAll();
    }
    public void LoadLevel1() 
    { 

        SceneManager.LoadScene("Level1.1");
        LevelManager.Instance.ClearAll();
    }

    public void LoadLevel2()
    {

        SceneManager.LoadScene("Level1.2");
        LevelManager.Instance.ClearAll();
    }

    public void LoadLevel3()
    {

        SceneManager.LoadScene("Level2.1");
        LevelManager.Instance.ClearAll();
    }
    public void LoadLevel4()
    {

        SceneManager.LoadScene("Level2.2");
        LevelManager.Instance.ClearAll();
    }


    #endregion

    public void QuitGame()
    {
        Application.Quit();

    }

    private void Update()
    {
      
        // Обработка кнопки Escape
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ShowMainMenu();
        }
    }
}