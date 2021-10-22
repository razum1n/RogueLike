using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class SaveData
{
    public float score;
    public float bestTime;

    public string hasValue;

    public SaveData()
    {
        score = 0f;
        bestTime = 1000.0f;
        hasValue = String.Empty;
    }
}
