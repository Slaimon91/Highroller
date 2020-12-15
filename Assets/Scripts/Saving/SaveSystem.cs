using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Collections;

public static class SaveSystem
{
    private static List<string> sceneNames = new List<string>()
    {
        "OW_FOD",
        "OW_SG",
        "OW_WE",
        "OW_RC",
        "OW_PP",
        "OW_PW"
    };
    public static void Save<T>(T objectToSave, string key)
    {
        string path = Application.persistentDataPath + "/saves/";
        Directory.CreateDirectory(path);
        BinaryFormatter formatter = GetBinaryFormatter();
        using(FileStream fileStream = new FileStream(path + key + ".txt", FileMode.Create))
        {
            formatter.Serialize(fileStream, objectToSave);
        }
    }

    public static void OverrideWithTemp(string key)
    {
        string path = Application.persistentDataPath + "/saves/" + key + "/";

        foreach (string dir in sceneNames)
        {
            if(File.Exists(path + "temp/" + dir + "/SaveData.txt"))
            {
                if(File.Exists(path + dir + "/SaveData.txt"))
                {
                    File.Delete(path + dir + "/SaveData.txt");
                }

                File.Copy(path + "temp/" + dir + "/SaveData.txt", path + dir + "/SaveData.txt");
            }
        }
    }

    public static T Load<T>(string key)
    {
        string path = Application.persistentDataPath + "/saves/";
        if (File.Exists(path + key + ".txt"))
        {
            BinaryFormatter formatter = GetBinaryFormatter();
            T returnValue = default(T);
            using (FileStream fileStream = new FileStream(path + key + ".txt", FileMode.Open))
            {
                returnValue = (T)formatter.Deserialize(fileStream);
            }

            return returnValue;
        }
        else
        {
            return default(T);
        }
    }

    public static bool SaveExists(string key)
    {
        string path = Application.persistentDataPath + "/saves/" + key + ".txt";
        return File.Exists(path);
    }

    public static void DeleteSavefile(string key)
    {
        string path = Application.persistentDataPath + "/saves/" + key;
        DirectoryInfo directory = new DirectoryInfo(path);
        if(Directory.Exists(path))
            directory.Delete(true);
        Directory.CreateDirectory(path);
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        SurrogateSelector selector = new SurrogateSelector();

        Vector2SerializationSurrogate vector2Surrogate = new Vector2SerializationSurrogate();
        Vector3SerializationSurrogate vector3Surrogate = new Vector3SerializationSurrogate();
        QuaternionSerializationSurrogate quaternionSerializationSurrogate = new QuaternionSerializationSurrogate();

        selector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), vector2Surrogate);
        selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);
        selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSerializationSurrogate);

        formatter.SurrogateSelector = selector;

        return formatter;
    }

    public static void ResetSavefile(int savefileNr)
    {
        string path = Application.persistentDataPath + "/saves/" + savefileNr;
        DirectoryInfo directory = new DirectoryInfo(path);
        Debug.Log(path + " reset file exists!");
        if (Directory.Exists(path))
            directory.Delete(true);
        Directory.CreateDirectory(path);
        path += "/";
        BuildDirectories(path);

        Directory.CreateDirectory(path + "temp");
        path += "temp/";
        BuildDirectories(path);
    }

    public static void ResetTemp(int savefileNr)
    {
        string path = Application.persistentDataPath + "/saves/" + savefileNr + "/temp";
        DirectoryInfo directory = new DirectoryInfo(path);
        Debug.Log(path + " reset temp!");
        if (Directory.Exists(path))
            directory.Delete(true);
        Directory.CreateDirectory(path);
        path += "/";
        BuildDirectories(path);

    }

    private static void BuildDirectories(string path)
    {
        foreach(string dir in sceneNames)
        {
            Directory.CreateDirectory(path + dir);
        }
    }
    /*
    public static void SavePlayer(PlayerController playerController)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(playerController);
        Debug.Log(path);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.save";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            try
            {
                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();

                return data;
            }
            catch
            {
                Debug.LogError("Failed to load file in " + path);
                stream.Close();
                return null;
            }
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    */
}
