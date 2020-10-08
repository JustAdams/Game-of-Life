using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CellGrid))]
public class CellGridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CellGrid grid = target as CellGrid;
        if (GUILayout.Button("Create Grid"))
        {
            grid.GenerateGrid();
        }
        if (GUILayout.Button("Simulate Generation"))
        {
            grid.SimulateGeneration();
        }
    }
}
