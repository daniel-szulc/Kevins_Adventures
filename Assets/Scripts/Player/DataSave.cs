using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public static class DataSave

{

    public static void SaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        String path = Application.persistentDataPath + "/gamedata.kevin";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        PlayerData data = new PlayerData();
        
        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static PlayerData LoadData()
    {
        String path = Application.persistentDataPath + "/gamedata.kevin";
        if (File.Exists(path))
        {
            
            BinaryFormatter formatter = new BinaryFormatter();
            
            FileStream stream = new FileStream(path, FileMode.Open);
            
            PlayerData data = (PlayerData)formatter.Deserialize(stream) as PlayerData;
           
            stream.Close();
           
            return data;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }

    public static void DeleteData()
    {
        String path = Application.persistentDataPath + "/gamedata.kevin";
        try
        {
            File.Delete(path);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
