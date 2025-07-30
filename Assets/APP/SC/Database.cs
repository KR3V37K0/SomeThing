using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Mono.Data.Sqlite;
using UnityEngine;

public static class DatabaseCommands
{
    private static readonly string ConnectionString = SetDataBaseClass.SetDataBase("Database_Localization.db");

    public static async Task<string> GetLocalization(string keyword)
    {
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
                            SELECT {Localization.languages[Localization.current_language_code]} 
                            FROM Localization 
                            WHERE keyword = '{keyword}'";

                        using (var reader = dbcmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return reader.GetString(0);
                            }
                        }
                    }
                }

                return "~ERROR~ keyword: " + keyword;
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[DB] GetLocalization() failed: {e.Message}");
            return "~ERROR~ keyword: " + keyword;
        }
    }
    public static async Task<string> GetRandomTipsKeyword()
    {
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
                            SELECT * FROM Localization
                            WHERE keyword LIKE 'Loading.Tip%'
                            ORDER BY RANDOM()
                            LIMIT 1;";

                        using (var reader = dbcmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return reader.GetString(0);
                            }
                        }
                    }
                }
                return "";
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"[DB] Loading Tips Error {e.Message}");
            return "";
        }
    }

}
