using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystemWareHouse
{
    public static void SaveWareHouse(WareHouse warehouse)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string pathWareHouseInfo = Application.persistentDataPath + "/warehouse.anime"; //путь, куда файл сохраняется
        FileStream stream = new FileStream(pathWareHouseInfo, FileMode.Create);

        WareHouseData data = new WareHouseData(warehouse);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static WareHouseData LoadWareHouse()
    {
        string pathWareHouseInfo = Application.persistentDataPath + "/warehouse.anime";
        if (File.Exists(pathWareHouseInfo))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(pathWareHouseInfo, FileMode.Open);

            WareHouseData data = formatter.Deserialize(stream) as WareHouseData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file is not found in " + pathWareHouseInfo);
            return null;
        }
    }
    public static void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/warehouse.anime"))
        {
            File.Delete(Application.persistentDataPath + "/warehouse.anime");
            Debug.Log("Data reset complete!");
        }
        else Debug.Log("No save data to delete.");
    }
}
