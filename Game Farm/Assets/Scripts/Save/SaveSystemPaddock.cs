using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystemPaddock : MonoBehaviour
{
    public static void SavePaddock(Paddock paddock)
    {
        List<PaddockData> listOfCows = new List<PaddockData>();
        List<PaddockData> listOfChickens = new List<PaddockData>();
        GameObject[] cows = GameObject.FindGameObjectsWithTag("Cow");
        GameObject[] chickens = GameObject.FindGameObjectsWithTag("Chicken");

        for (int i = 1; i < cows.Length; i++)
        {
            print("------------ cows l: " + cows.Length);
            print("------------ cows[i]: " + cows[i]);
            print("------------ cows(): " + cows[i].transform.GetComponent<Paddock>());
            listOfCows.Add(new PaddockData(cows[i].transform.GetComponent<Paddock>()));
        }
        for (int i = 1; i < chickens.Length; i++)
        {
            print("------------ chickens l: " + chickens.Length);
            print("------------ chickens[i]: " + chickens[i]);
            print("------------ cchickens(): " + chickens[i].transform.GetComponent<Paddock>());
            listOfChickens.Add(new PaddockData(chickens[i].transform.GetComponent<Paddock>()));
        }

        BinaryFormatter formatter = new BinaryFormatter();
        string pathPaddockCows = Application.persistentDataPath + "/paddockCow.anime"; //путь, куда файл сохраняется
        string pathPaddockChickens = Application.persistentDataPath + "/paddockChic.anime"; //путь, куда файл сохраняется
        FileStream stream1 = new FileStream(pathPaddockCows, FileMode.Create);
        FileStream stream2 = new FileStream(pathPaddockChickens, FileMode.Create);

        formatter.Serialize(stream1, listOfCows);
        formatter.Serialize(stream2, listOfChickens);
        stream1.Close(); stream2.Close();
    }

    public static void LoadPaddock()
    {
        string pathPaddockCows = Application.persistentDataPath + "/paddockCow.anime";
        if (File.Exists(pathPaddockCows))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(pathPaddockCows, FileMode.Open);

            List<PaddockData> dataCows = formatter.Deserialize(stream) as List<PaddockData>;
            stream.Close();

            
            GameObject cows = GameObject.FindGameObjectWithTag("Cow");

            int j = 0;
           
            while (j != dataCows.Count)
            {
                var pl = Instantiate(cows, new Vector3(dataCows[j].px, dataCows[j].py, dataCows[j].pz), Quaternion.identity) as GameObject;
                pl.GetComponent<Building>().MainRenderer.enabled = true;
                pl.GetComponent<Building>().GetComponent<Collider2D>().enabled = true;
                pl.GetComponent<Building>().MainRenderer.material.color = Color.white;
                pl.GetComponent<Paddock>().LoadPaddock(dataCows[j]); 

                j++;
            }
        }

        string pathPaddockChickens = Application.persistentDataPath + "/paddockChic.anime";
        if (File.Exists(pathPaddockChickens))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(pathPaddockChickens, FileMode.Open);

            List<PaddockData> dataChickens = formatter.Deserialize(stream) as List<PaddockData>;
            stream.Close();

            GameObject chicken = GameObject.FindGameObjectWithTag("Chicken");

            int j = 0;

            while (j != dataChickens.Count)
            {
                var pl = Instantiate(chicken, new Vector3(dataChickens[j].px, dataChickens[j].py, dataChickens[j].pz), Quaternion.identity) as GameObject;
                pl.GetComponent<Building>().MainRenderer.enabled = true;
                pl.GetComponent<Building>().GetComponent<Collider2D>().enabled = true;
                pl.GetComponent<Building>().MainRenderer.material.color = Color.white;
                pl.GetComponent<Paddock>().LoadPaddock(dataChickens[j]);

                j++;
            }
        }
    }
    public static void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/paddockChic.anime"))
        {
            File.Delete(Application.persistentDataPath + "/paddockChic.anime");
            Debug.Log("Data reset complete!");
        }
        if (File.Exists(Application.persistentDataPath + "/paddockCow.anime"))
        {
            File.Delete(Application.persistentDataPath + "/paddockCow.anime");
            Debug.Log("Data reset complete!");
        }
        else Debug.Log("No save data to delete.");
    }
}
