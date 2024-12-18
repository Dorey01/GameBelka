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
                            INSERT INTO LivelNuts (LevelID, NutCount) VALUES (3, 0);";
                        command.ExecuteNonQuery();
                        Debug.Log("Добавлены начальные данные в таблицу");
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Ошибка при инициализации данных: {e.Message}");
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
            Debug.LogError($"Ошибка при загрузке данных: {e.Message}");
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
            Debug.LogError($"Ошибка при сохранении данных: {e.Message}");
        }
    }
}