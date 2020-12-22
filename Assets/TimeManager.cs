using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float totalGameTime;
    private float timeSinceAppLaunch;
    private float gameTimeAtAppLaunch;
    private float timeOffset = 0;

    public static TimeManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        gameObject.transform.parent = null;
        //DontDestroyOnLoad(gameObject);
        DontDestroyOnLoadManager.DontDestroyOnLoad(gameObject);
        GameEvents.SaveInitiated += Save;
        GameEvents.LoadInitiated += Load;
    }
    private void Update()
    {
        timeSinceAppLaunch = Time.timeSinceLevelLoad;
        totalGameTime = timeSinceAppLaunch + gameTimeAtAppLaunch + timeOffset;
    }

    public string GetTimeHoursSeconds()
    {
        var totalSeconds = totalGameTime;
        var ss = Convert.ToInt32(totalSeconds % 60).ToString("00");
        var mm = (Math.Floor(totalSeconds / 60) % 60).ToString("00");
        var hh = Math.Floor(totalSeconds / 60 / 60).ToString("00");

        return hh + "H " + mm + "M";
    }

    public float GetTimeSeconds()
    {
        return totalGameTime;
    }

    public void AddOneMinute()
    {
        timeOffset += 60;
    }
    private void Save(string temp)
    {
        SaveData.current.timeManager = new TimeManagerData(gameObject.GetComponent<TimeManager>());
    }

    public void Load(string temp)
    {
        if(temp == "")
        {
            TimeManagerData data = SaveData.current.timeManager;

            if (data != default)
            {
                totalGameTime = data.totalGameTime;
            }

            timeSinceAppLaunch = 0;
            gameTimeAtAppLaunch = totalGameTime;
        }
    }

    public void OnDestroy()
    {
        GameEvents.SaveInitiated -= Save;
        GameEvents.LoadInitiated -= Load;
    }
}
[System.Serializable]
public class TimeManagerData
{
    public float totalGameTime;

    public TimeManagerData(TimeManager timeManager)
    {
        totalGameTime = timeManager.totalGameTime;
    }
}

