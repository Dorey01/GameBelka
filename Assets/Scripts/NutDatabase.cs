using UnityEngine;
using Mono.Data.Sqlite;
using System;

public class NutDatabase
{
    private readonly string dbPath;

    public NutDatabase()
    {
        string dbName = "simplefolks.sqlite";
#if UNITY_EDITOR
        dbPath = "URI=file:" + Application.dataPath + "/StreamingAssets/" + dbName;
#else
        dbPath = "URI=file:" + Application.streamingAssetsPath + "/" + dbName;
#endif
    }

 // ... existing code ...

    public void InitializeTableData()
{
    try
    {
        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM LivelNuts";
                long count = (long)command.ExecuteScalar();

                if (count == 0)
                {
                    command.CommandText = @"
                        INSERT INTO LivelNuts (LevelID, NutCount) VALUES (1, 0);
                        INSERT INTO LivelNuts (LevelID, NutCount) VALUES (2, 0);
                        INSERT INTO LivelNuts (LevelID, NutCount) VALUES (3, 0);
                        INSERT INTO LivelNuts (LevelID, NutCount) VALUES (4, 0);";  // Добавлена строка для 4-го уровня
                    command.ExecuteNonQuery();
                    Debug.Log("Выполнена начальная запись в таблицу");
                }
            }
        }
    }
    catch (Exception e)
    {
        Debug.LogError($"Ошибка при инициализации данных: {e.Message}");
    }
}

    // Добавляем новый метод для обнуления орехов
    public void ResetNutCount(int levelID)
    {
        try
        {
            using (var connection = new SqliteConnection(dbPath))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE LivelNuts SET NutCount = 0 WHERE LevelID = @levelID";
                    command.Parameters.AddWithValue("@levelID", levelID);
                    command.ExecuteNonQuery();
                    Debug.Log($"Орехи для уровня {levelID} обнулены");
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Ошибка при обнулении орехов: {e.Message}");
        }
    }

    public float LoadNutCount(int levelID)
    {
        try
        {
            using (var connection = new SqliteConnection(dbPath))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT NutCount FROM LivelNuts WHERE LevelID = @levelID";
                    command.Parameters.AddWithValue("@levelID", levelID);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return Convert.ToInt32(reader["NutCount"]);
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"������ ��� �������� ������: {e.Message}");
        }
        return 0;
    }

    public void SaveNutCount(int levelID, float nutCount)
    {
        try
        {
            using (var connection = new SqliteConnection(dbPath))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        UPDATE LivelNuts 
                        SET NutCount = @nutCount 
                        WHERE LevelID = @levelID";

                    command.Parameters.AddWithValue("@levelID", levelID);
                    command.Parameters.AddWithValue("@nutCount", nutCount);

                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"������ ��� ���������� ������: {e.Message}");
        }
    }
}