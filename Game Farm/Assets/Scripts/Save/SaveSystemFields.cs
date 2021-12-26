using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystemFields
{
    public static void SaveField(Field field)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string pathFieldInfo = Application.persistentDataPath + "/fields.anime"; //путь, куда файл сохраняется
        FileStream stream = new FileStream(pathFieldInfo, FileMode.Create);

        FieldData data = new FieldData(field);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static FieldData LoadField()
    {
        string pathFieldInfo = Application.persistentDataPath + "/fields.anime";
        if (File.Exists(pathFieldInfo))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(pathFieldInfo, FileMode.Open);

            FieldData data = formatter.Deserialize(stream) as FieldData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file is not found in " + pathFieldInfo);
            return null;
        }
    }
    public static void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/fields.anime"))
        {
            File.Delete(Application.persistentDataPath + "/fields.anime");
            Debug.Log("Data reset complete!");
        }
        else Debug.Log("No save data to delete.");
    }
}
