using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private Animation anim;
    private static float SaveNut;
    public static void Save(float Nut)
    {
        SaveNut = Nut;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelManager.Instance.SaveNutCount(SaveNut);

        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "Level1.1":
                SceneManager.LoadScene("Level1.2");
                break;  // Добавлен break

            case "Level1.2":
                SceneManager.LoadScene("Level2.1");
                break;  // Добавлен break

            case "Level2.1":
                // Здесь нужно указать, какую сцену загружать после Level3
                SceneManager.LoadScene("Level2.2"); // или другую сцену
                break;  // Добавлен break
            case "Level2.2":
                // Здесь нужно указать, какую сцену загружать после Level3
                SceneManager.LoadScene("TheEndHistory"); // или другую сцену
                break;  // Добавлен break

            default:
                Debug.LogWarning($"Неизвестный уровень: {sceneName}");
                break;
        }

    }

   
}