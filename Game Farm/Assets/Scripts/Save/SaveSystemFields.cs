using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystemFields : MonoBehaviour
{
    public static void SaveField(Field field)
    {
        List<FieldData> listOfFields = new List<FieldData>();
        var Farm = GameObject.FindGameObjectsWithTag("Farm");

        for (int i = 0; i < Farm[0].transform.childCount; i++)
            if (Farm[0].transform.GetChild(i).tag == "Plot") {    
                //добавл€ем в список все Plot
                listOfFields.Add(new FieldData(Farm[0].transform.GetChild(i).GetComponent<Field>()));
            }

        BinaryFormatter formatter = new BinaryFormatter();
        string pathFieldInfo = Application.persistentDataPath + "/fields.anime"; //путь, куда файл сохран€етс€
        FileStream stream = new FileStream(pathFieldInfo, FileMode.Create);

        formatter.Serialize(stream, listOfFields);
        stream.Close();
    }

    public static void LoadField()
    {
        string pathFieldInfo = Application.persistentDataPath + "/fields.anime";
        if (File.Exists(pathFieldInfo))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(pathFieldInfo, FileMode.Open);

            List<FieldData> data = formatter.Deserialize(stream) as List<FieldData>;
            stream.Close();

            var Farm = GameObject.FindGameObjectWithTag("Farm");
            int j = 0;
            for (int i = 0; i < Farm.transform.childCount; i++) {
                if (Farm.transform.GetChild(i).tag == "Plot")
                {
                    Farm.transform.GetChild(i).GetComponent<Field>().LoadField(data[j]);
                    j++;
                }
            }
            while (j != data.Count)
            {
                var pl = Instantiate(Resources.Load("Plot", typeof(GameObject)), new Vector3(data[j].px, data[j].py, data[j].pz), Quaternion.identity) as GameObject;
                pl.GetComponent<Field>().LoadField(data[j],true);
                pl.transform.parent = Farm.transform;
                j++;
            }
        }
    }

    public static void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/fields.anime"))
        {
            File.Delete(Application.persistentDataPath + "/fields.anime");
            Debug.Log("Fields data reset complete!");
        }
        else Debug.Log("No Felds save data to delete.");
    }
}
