using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable()]
public class SaveData: ISerializable
{
    public float coins=0;
    public LevelsInfo[] Lvl = new LevelsInfo[15];
    public int actualLevel=0;
    public SaveData () {
    }

    public SaveData (SerializationInfo info, StreamingContext ctxt)
    {
        coins = (float)info.GetValue("coins", typeof(float));
      // Lvl = (LevelsInfo[]) info.GetValue("Lvl", typeof(LevelsInfo[]));
      Lvl = _Level.Lvl;
      actualLevel = _Level.actualLevel;
    }

    public void GetObjectData (SerializationInfo info, StreamingContext ctxt)
    {
        info.AddValue("coins", coins);
        info.AddValue("Lvl", Lvl);
        info.AddValue("actualLevel", actualLevel);
    }
}



public class SaveLoad {

    public static string currentFilePath = "SaveData.cjc";
    public static SaveData data = new SaveData ();
    public static void Save ()
    {
        Save (currentFilePath);
    }

    public static void Save (string filePath)
    {
        Debug.Log("Saved");
        data = new SaveData ();
        Stream stream = File.Open(filePath, FileMode.Create);
        BinaryFormatter bformatter = new BinaryFormatter();
        bformatter.Binder = new VersionDeserializationBinder(); 
        bformatter.Serialize(stream, data);
        stream.Close();
    }
	
    public static void Load ()  {
        Load(currentFilePath);  
    }

    public static void Load (string filePath) 
    {
        Debug.Log("Loaded");
        data = new SaveData ();
        Stream stream = File.Open(filePath, FileMode.Open);
        BinaryFormatter bformatter = new BinaryFormatter();
        bformatter.Binder = new VersionDeserializationBinder(); 
        data = (SaveData)bformatter.Deserialize(stream);
        stream.Close();
        

    }
}

public sealed class VersionDeserializationBinder : SerializationBinder
{
    public override Type BindToType(string assemblyName, string typeName)
    {
        if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
        {
            Type typeToDeserialize = null;
            assemblyName = Assembly.GetExecutingAssembly().FullName;
            typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));
            return typeToDeserialize;
        }

        return null;
    }
}