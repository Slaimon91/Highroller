using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CorruptionSource : MonoBehaviour
{
    [SerializeField] Sprite battleBackground;
    [SerializeField] List<CorruptionCheckpoint> checkpoints;

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
}

public enum CheckpointOptions { Health, Gaia, Monster }

[System.Serializable]
public class CorruptionCheckpoint
{
    public CheckpointOptions option;
    [HideInInspector] public bool cleansed;
    public int rewardAmount;
    public Encounter encounter;
}

/*#if UNITY_EDITOR
[CustomEditor(typeof(CorruptionCheckpoint))]
public class RandomScript_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // for other non-HideInInspector fields

        RandomScript script = (RandomScript)target;

        // draw checkbox for the bool
        script.StartTemp = EditorGUILayout.Toggle("Start Temp", script.StartTemp);
        if (script.StartTemp) // if bool is true, show other fields
        {
            script.iField = EditorGUILayout.ObjectField("I Field", script.iField, typeof(InputField), true) as InputField;
            script.Template = EditorGUILayout.ObjectField("Template", script.Template, typeof(GameObject), true) as GameObject;
        }
    }
}
#endif*/