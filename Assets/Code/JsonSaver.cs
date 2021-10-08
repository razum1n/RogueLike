using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;

public class JsonSaver
{
    private static readonly string fileName = "saveData1.sav";

    public static string GetSaveFileName()
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    public void Save(SaveData data)
    {
        data.hasValue = String.Empty;
        string json = JsonUtility.ToJson(data);

        data.hasValue = GetSHA256(json);
        json = JsonUtility.ToJson(data);

        string saveFileName = GetSaveFileName();

        FileStream fileStream = new FileStream(saveFileName, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    public bool Load(SaveData data)
    {
        string loadFileName = GetSaveFileName();

        if(File.Exists(loadFileName))
        {
            using (StreamReader reader = new StreamReader(loadFileName))
            {
                string json = reader.ReadToEnd();

                if(CheckData(json))
                {
                    JsonUtility.FromJsonOverwrite(json, data);
                }
                else
                {
                    Debug.LogWarning("JSONSAVER Load: Invalid Hash" );
                }

                
            }
            return true;
        }

        return false;
    }

    private bool CheckData(string json)
    {
        SaveData tempSave = new SaveData();
        JsonUtility.FromJsonOverwrite(json, tempSave);

        string oldHash = tempSave.hasValue;
        tempSave.hasValue = String.Empty;

        string tempJson = JsonUtility.ToJson(tempSave);
        string newHash = GetSHA256(tempJson);

        return (oldHash == newHash);
    }

    public void Delete()
    {
        File.Delete(GetSaveFileName());
    }

    public string GetHexFromHash(byte[] hash)
    {
        string hexString = String.Empty;

        foreach (byte b in hash) {
            hexString += b.ToString("x2");
        }
        return hexString;
    }

    private string GetSHA256(string text)
    {
        byte[] textToBytes = Encoding.UTF8.GetBytes(text);
        SHA256Managed mySHA256 = new SHA256Managed();

        byte[] hashValue = mySHA256.ComputeHash(textToBytes);

        return GetHexFromHash(hashValue);
    }
}
