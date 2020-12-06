using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static System.Action<string> SaveInitiated;
    public static System.Action SaveBetweenScenes;
    public static System.Action<string> LoadInitiated;
    public static System.Action LoadBetweenScenes;

    public static void OnSaveInitiated()
    {
        SaveInitiated?.Invoke("");
    }

    public static void OnSaveBetweenScenes()
    {
        SaveInitiated?.Invoke("temp/");
    }

    public static void OnLoadInitiated()
    {
        LoadInitiated?.Invoke("");
    }

    public static void OnLoadBetweenScenes()
    {
        LoadInitiated?.Invoke("temp/");
    }
}
