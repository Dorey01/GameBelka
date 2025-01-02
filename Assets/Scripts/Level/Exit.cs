using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private static float SaveNut;
    public static void Save(float Nut)
    {
        SaveNut = Nut;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelManager.Instance.SaveNutCount(SaveNut);

        // Получаем текущее имя сцены
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "Level1.1":
                SceneManager.LoadScene("Level1.2");
                break;  // Добавлен break

            case "Level1.2":
                SceneManager.LoadScene("Level2.1");
                break;  // Добавлен break

            case "Level3":
                // Здесь нужно указать, какую сцену загружать после Level3
                SceneManager.LoadScene("MainMenu"); // или другую сцену
                break;  // Добавлен break

            default:
                Debug.LogWarning($"Неизвестный уровень: {sceneName}");
                break;
        }
    }
}