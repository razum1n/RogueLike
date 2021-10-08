using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class SaveData
{
    public float score;
    public string rank;

    public string hasValue;

    public SaveData()
    {
        score = 0f;
        rank = "";
        hasValue = String.Empty;
    }
}
