using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static System.Action<string> SaveInitiated;
    public static System.Action SaveBetweenScenes;
    public static System.Action<string> LoadInitiated;
    public static System.Action LoadBetweenScenes;

    public static bool OnSaveInitiated()
    {
        SaveInitiated?.Invoke("");
        return true;
    }

    public static bool OnSaveBetweenScenes()
    {
        SaveInitiated?.Invoke("temp/");
        return true;
    }

    public static bool OnLoadInitiated()
    {
        LoadInitiated?.Invoke("");
        return true;
    }

    public static bool OnLoadBetweenScenes()
    {
        LoadInitiated?.Invoke("temp/");
        return true;
    }
}
