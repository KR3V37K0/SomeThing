using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

public static class DeliveryManager
{
    static string dbPath = Path.Combine(Application.streamingAssetsPath, "Database_Delivery.db");
    static string ConnectionString = $"URI=file:{dbPath}";
    static List<string> readers = new List<string>();
    static bool is_locked = false;

    public static async Task<bool> AddQuest(QuestDeliverySCO _quest, string _table)
    {
        if (is_locked || readers.Count>0) await Task.Delay(100);
        is_locked = true;
        try
        {
            return await Task.Run(async () =>
            {
                using (var dbconn = new SqliteConnection(ConnectionString))
                {
                    dbconn.Open();

                    using (var dbcmd = dbconn.CreateCommand())
                    {
                        dbcmd.CommandText = $@"INSERT OR IGNORE INTO Delivery(SaveFile, Tab, Quest) VALUES({SaveLoad.current_save.preview.slot}, '{_table}', '{JsonUtility.ToJson(_quest)}')";
                        
                        int rowsAffected = dbcmd.ExecuteNonQuery();
                        is_locked = false;
                        return rowsAffected > 0;
                    }
                }
            });
        }
        catch (Exception e)
        {
            is_locked = false;
            Debug.LogError($"[DB] Adding Quest Error {e.Message}");
            return false;
        }
    }
    public static async Task<List<QuestDeliveryJson>> GetQuestTo(string _table)
    {
        if (is_locked) await Task.Delay(100);
        string id="GetQuestTo "+_table;
        readers.Add(id);

        List<QuestDeliveryJson> quests = new List<QuestDeliveryJson>();

        try
        {
            return await Task.Run(() =>
            {
                using (var dbconn = new SqliteConnection(ConnectionString))
                {
                    dbconn.Open();

                    using (var dbcmd = dbconn.CreateCommand())
                    {
                        dbcmd.CommandText = $@"
                            SELECT Quest 
                            FROM Delivery
                            WHERE SaveFile = {SaveLoad.current_save.preview.slot} AND Tab = '{_table}'";

                        using (var reader = dbcmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                quests.Add( JsonUtility.FromJson<QuestDeliveryJson>( reader.GetString(0) ));
                            }
                            return quests;
                        }
                    }
                }
            });
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            readers.Remove(id);
            return quests;  
        }
     
    }

}
