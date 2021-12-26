using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string pathPlayerInfo = Application.persistentDataPath + "/player.anime"; //путь, куда файл сохраняется
        FileStream stream = new FileStream(pathPlayerInfo, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string pathPlayerInfo = Application.persistentDataPath + "/player.anime";
        if (File.Exists(pathPlayerInfo))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(pathPlayerInfo, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file is not found in " + pathPlayerInfo);
            return null;
        }
    }
    public static void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/player.anime"))
        {
            File.Delete(Application.persistentDataPath + "/player.anime");
            Debug.Log("Data reset complete!");
        }
        else Debug.Log("No save data to delete.");
    }
}
