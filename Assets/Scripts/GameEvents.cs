using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static System.Action<string> SaveInitiated;
    public static System.Action SaveBetweenScenes;
    public static System.Action<string> LoadInitiated;
    public static System.Action LoadBetweenScenes;

    public static System.Action<string> SaveInitiatedData;
    public static System.Action<string> LoadInitiatedData;

    public static bool OnSaveInitiated()
    {
        FindObjectOfType<SaveDataManager>().Reset();
        SaveInitiated?.Invoke("");
        SaveInitiatedData?.Invoke("");
        return true;
    }

    public static bool OnSaveBetweenScenes()
    {
        FindObjectOfType<SaveDataManager>().Reset();
        SaveInitiated?.Invoke("temp/");
        SaveInitiatedData?.Invoke("temp/");
        return true;
    }

    public static bool OnLoadInitiated()
    {
        
        LoadInitiatedData?.Invoke("");
        LoadInitiated?.Invoke("");
        return true;
    }

    public static bool OnLoadBetweenScenes()
    {
        
        LoadInitiatedData?.Invoke("temp/");
        LoadInitiated?.Invoke("temp/");
        return true;
    }
}
