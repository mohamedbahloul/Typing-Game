using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;



public static class SaveSystem 
{
    private static string path = Application.persistentDataPath + "/eng.txt";
    private static string path1 = Application.persistentDataPath + "/eng.nooba";
    
    public static bool testExist() {
        if (File.Exists(path))
        {
            return true;
        }
        else
            return false;
    }
    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(path1, FileMode.OpenOrCreate);
        string serializableSaveData = ReadString();
        bf.Serialize(file, serializableSaveData);
        file.Close();
        //Debug.Log (SaveData.currentLevel);
    }
    public static void SaveDic()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path1, FileMode.Create);
        //PlayerData data = new PlayerData();
        formatter.Serialize(stream, ReadString());
        stream.Close();
    }
    public static string ReadString()
    {
        

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        return reader.ReadToEnd();
        reader.Close();
    }
    public static string loadDic()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            string data = formatter.Deserialize(stream) as string;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
        
}
