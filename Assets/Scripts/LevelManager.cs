using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    
    public int CurrentLevelID { get; private set; }
    public string CurrentLevelName { get; private set; }
    private NutDatabase database;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDatabase();
            InitializeLevel();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeDatabase()
    {
        if (database == null)
        {
            database = new NutDatabase();
            database.InitializeTableData();
        }
    }

    // Новые методы для работы с орехами
    public void SaveNutCount(float nutCount)
    {
        database.SaveNutCount(CurrentLevelID, nutCount);
      
    }

    public void ClearAll()
    {
        float nutCount = 0;
        for (int i = 0; i <= 4; i++)
        {

            database.SaveNutCount(i, 0);
        }

    }

    public float LoadNutCount(int levelID)
    {
        return database.LoadNutCount(levelID);
    }

    public float GetTotalNutsExceptCurrent()
    {
        float totalNuts = 0;
        for (int i = 1; i <= GetTotalLevels(); i++)
        {
            if (i != CurrentLevelID)
            {
                totalNuts += database.LoadNutCount(i);
            }
        }
        return totalNuts;
    }

    private void InitializeLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SetLevelInfo(sceneName);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetLevelInfo(scene.name);
    }

    private void SetLevelInfo(string sceneName)
    {
        switch (sceneName)
        {
            case "Level1.1":
                CurrentLevelID = 1;
                CurrentLevelName = "Уровень 1";
                break;
            case "Level1.2":
                CurrentLevelID = 2;
                CurrentLevelName = "Уровень 2";
                break;
            case "Level2.1":
                CurrentLevelID = 3;
                CurrentLevelName = "Уровень 3";
                break;
            case "Level2.2":
                CurrentLevelID = 4;
                CurrentLevelName = "Уровень 4";
                break;
            default:
                Debug.LogWarning($"Неизвестный уровень: {sceneName}");
                CurrentLevelID = 0;
                CurrentLevelName = "Неизвестный уровень";
                break;
        }
        
        Debug.Log($"Текущий уровень: {CurrentLevelName} (ID: {CurrentLevelID})");
    }

    public bool IsLevelValid(int levelID)
    {
        return levelID >= 1 && levelID <= 4;
    }

    public int GetTotalLevels()
    {
        return 4;
    }
}
