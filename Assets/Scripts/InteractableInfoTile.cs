using UnityEngine.Tilemaps;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Info Tile", menuName = "Highroller/Info Tile")]
public class InteractableInfoTile : Tile
{
    public Sprite tileSprite;
    public int layer;
    [TextArea(3, 20)]
    public string genericText;

    public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
    {
        base.GetTileData(location, tileMap, ref tileData);
        {
            tileData.sprite = tileSprite;
            tileData.colliderType = Tile.ColliderType.Sprite;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(InteractableInfoTile))]
public class InteractableInfoTileEditor : Editor
{
    public InteractableInfoTile tile { get { return (target as InteractableInfoTile); } }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        tile.tileSprite = (Sprite)EditorGUILayout.ObjectField("Sprite", tile.tileSprite, typeof(Sprite), false);
        tile.genericText = EditorGUILayout.TextArea(tile.genericText);
        tile.layer = EditorGUILayout.LayerField("Layer", tile.layer);
        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(tile);
    }
}
#endif
