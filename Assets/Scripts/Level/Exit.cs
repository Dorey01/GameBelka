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
                break;  // �������� break

            case "Level1.2":
                SceneManager.LoadScene("Level2.1");
                break;  // �������� break

            case "Level2.1":
                // ����� ����� �������, ����� ����� ��������� ����� Level3
                SceneManager.LoadScene("Level2.2"); // ��� ������ �����
                break;  // �������� break
            case "Level2.2":
                // ����� ����� �������, ����� ����� ��������� ����� Level3
                SceneManager.LoadScene("TheEndHistory"); // ��� ������ �����
                break;  // �������� break

            default:
                Debug.LogWarning($"����������� �������: {sceneName}");
                break;
        }

    }

   
}