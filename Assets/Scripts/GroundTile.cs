using UnityEngine.Tilemaps;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum GroundType { GreenforestGrass, GreenforestWater, GreenforestSwamp}

public class GroundTile : Tile
{
    public Sprite tileSprite;
    public GroundType groundType;
    public int layer;

    public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
    {
        base.GetTileData(location, tileMap, ref tileData);
        {
            tileData.sprite = tileSprite;
            tileData.colliderType = Tile.ColliderType.Sprite;
        }
    }

    /*public GroundType GetGroundType()
    {
        return groundType;
    }*/

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Ground Tile")]
    public static void CreateGroundTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Ground Tile", "New Ground Tile", "asset", "Save Ground Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<GroundTile>(), path);
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(GroundTile))]
public class GroundTileEditor : Editor
{
    public GroundTile tile { get { return (target as GroundTile); } }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        tile.tileSprite = (Sprite)EditorGUILayout.ObjectField("Sprite", tile.tileSprite, typeof(Sprite), false);
        tile.groundType = (GroundType)EditorGUILayout.EnumPopup("Ground Type", tile.groundType);
        tile.layer = EditorGUILayout.LayerField("Layer", tile.layer);
        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(tile);
    }
}
#endif
