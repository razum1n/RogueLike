using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private SaveData saveData;
    private JsonSaver jsonSaver;
    public float Score 
    { 
        get { return saveData.score; }
        set { saveData.score = value; }
    }

    public string Rank
    {
        get { return saveData.rank; }
        set { saveData.rank = value; }
    }

    private void Awake()
    {
        saveData = new SaveData();
        jsonSaver = new JsonSaver();
    }

    public void Save()
    {
        jsonSaver.Save(saveData);
    }

    public void Load()
    {
        jsonSaver.Load(saveData);
    }

}
