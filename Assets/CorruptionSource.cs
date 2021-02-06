using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CorruptionSource : MonoBehaviour
{
    [SerializeField] Sprite battleBackground;
    [SerializeField] Sprite cleansedStoneSprite;
    [SerializeField] GameObject corruptedTile;
    public List<CorruptionCheckpoint> checkpoints;
    [HideInInspector] public bool isCleansed = false;
    [HideInInspector] public string id;
    [HideInInspector] public bool bufferDestroyAnim = false;
    void Awake()
    {
        GameEvents.SaveInitiated += Save;
        GameEvents.LoadInitiated += Load;
    }
    void Start()
    {
        id = GetComponent<UniqueID>().id;
    }
    public Sprite GetBattleBackground()
    {
        return battleBackground;
    }

    public List<CorruptionCheckpoint> GetCheckpoints()
    {
        return checkpoints;
    }

    public int GetNumberOfCheckpoints()
    {
        return checkpoints.Count;
    }

    public void ActivateDestroyAnim()
    {
        GetComponent<Animator>().SetBool("isDestroyed", true);
    }

    public void DestroyAnimComplete()
    {
        foreach (MysteriousObject obj in GetComponentsInChildren<MysteriousObject>())
        {
            obj.gameObject.SetActive(false);
        }
        corruptedTile.SetActive(false);
    }

    private void Save(string temp)
    {
        SaveData.current.corruptionSources.Add(new CorruptionSourceData(gameObject.GetComponent<CorruptionSource>()));
    }

    public void Load(string temp)
    {
        CorruptionSourceData data = SaveData.current.corruptionSources.Find(x => x.id == id);

        if (data != default)
        {
            isCleansed = data.isCleansed;
            bufferDestroyAnim = data.bufferDestroyAnim;

            for (int i = 0; i < data.checkpoints.Count; i++)
            {
                if(data.checkpoints[i])
                {
                    checkpoints[i].cleansed = true;
                }
            }

            if(isCleansed)
            {
                GetComponent<Animator>().enabled = false;
                GetComponentInChildren<CorruptionHelper>().GetComponent<SpriteRenderer>().sprite = cleansedStoneSprite;
                foreach (MysteriousObject obj in GetComponentsInChildren<MysteriousObject>())
                {
                    obj.gameObject.SetActive(false);
                }
                corruptedTile.SetActive(false);
            }
            if(bufferDestroyAnim)
            {
                bufferDestroyAnim = false;
                isCleansed = true;
                ActivateDestroyAnim();
            }
        }
    }

    public void OnDestroy()
    {
        GameEvents.SaveInitiated -= Save;
        GameEvents.LoadInitiated -= Load;
    }
}

[System.Serializable]
public class CorruptionSourceData
{
    public string id;
    public bool isCleansed;
    public List<bool> checkpoints = new List<bool>();
    public bool bufferDestroyAnim;

    public CorruptionSourceData(CorruptionSource corruptionSource)
    {
        id = corruptionSource.id;
        isCleansed = corruptionSource.isCleansed;
        bufferDestroyAnim = corruptionSource.bufferDestroyAnim;
        foreach(CorruptionCheckpoint cp in corruptionSource.checkpoints)
        {
            checkpoints.Add(cp.cleansed);
        }
    }
}

public enum CheckpointOptions { Health, Gaia, Monster }

[System.Serializable]
public class CorruptionCheckpoint
{
    public CheckpointOptions option;
    public bool cleansed;
    [HideInInspector] public int rewardAmount;
    [HideInInspector] public Encounter encounter;
    [HideInInspector] public GameObject Template;
}

#if UNITY_EDITOR
[CustomEditor(typeof(CorruptionSource))]
public class CorruptionSourceEditor : Editor
{
    //public CorruptionCheckpoint script { get { return (target as CorruptionCheckpoint); } }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // for other non-HideInInspector fields

        CorruptionSource script = target as CorruptionSource;
        // draw checkbox for the bool
        for (int i = 0; i < script.checkpoints.Count; i++)
        {
           // script.checkpoints[i].cleansed = EditorGUILayout.Toggle(true);
            if (/*script.checkpoints[i].option == CheckpointOptions.Health || script.checkpoints[i].option == CheckpointOptions.Gaia*/script.checkpoints[i].cleansed) // if bool is true, show other fields
            {
                //script.rewardAmount = EditorGUILayout.ObjectField("I Field", script.rewardAmount, typeof(int), true) as int;
                //script.rewardAmount = EditorGUILayout.ObjectField("Template", script.rewardAmount, typeof(int), true) as int;
                script.checkpoints[i].rewardAmount = (int)EditorGUILayout.IntField(script.checkpoints[i].rewardAmount);
            }
        }
    }
}

#endif