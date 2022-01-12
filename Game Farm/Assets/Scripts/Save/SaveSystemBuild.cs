using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystemBuild : MonoBehaviour
{
    public static void SaveBuilds(string tag)
    {
        List<BuildData> listOfbuilding = new List<BuildData>();
        GameObject[] buildings = GameObject.FindGameObjectsWithTag(tag);

        for (int i = 1; i < buildings.Length; i++)
        {
            listOfbuilding.Add(new BuildData(buildings[i].transform.GetComponent<ProductionBuilding>()));
        }

        BinaryFormatter formatter = new BinaryFormatter();
        string pathBuilding = Application.persistentDataPath + $"/{tag}.anime"; //путь, куда файл сохраняется
        FileStream stream = new FileStream(pathBuilding, FileMode.Create);

        print(buildings);
        formatter.Serialize(stream, listOfbuilding);
        stream.Close();
    }

    public static void LoadBuilding(string tag)
    {
        string pathBuilding = Application.persistentDataPath + $"/{tag}.anime";
        if (File.Exists(pathBuilding))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(pathBuilding, FileMode.Open);

            List<BuildData> dataBuilds = formatter.Deserialize(stream) as List<BuildData>;
            stream.Close();


            GameObject builds = GameObject.FindGameObjectWithTag(tag);

            int j = 0;

            while (j != dataBuilds.Count)
            {
                var pl = Instantiate(builds, new Vector3(dataBuilds[j].px, dataBuilds[j].py, dataBuilds[j].pz), Quaternion.identity) as GameObject;
                pl.GetComponent<Building>().MainRenderer.enabled = true;
                pl.GetComponent<Building>().GetComponent<Collider2D>().enabled = true;
                pl.GetComponent<Building>().MainRenderer.material.color = Color.white;
                pl.GetComponent<ProductionBuilding>().LoadBuild(dataBuilds[j]);

                j++;
            }
        }
    }

    public static void ResetData(string tag)
    {
        if (File.Exists(Application.persistentDataPath + $"/{tag}.anime"))
        {
            File.Delete(Application.persistentDataPath + $"/{tag}.anime");
            Debug.Log("Data reset complete!");
        }
        else Debug.Log("No save data to delete.");
    }
}
