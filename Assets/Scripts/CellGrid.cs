using UnityEngine;

public class CellGrid : MonoBehaviour
{
    public Cell cellPrefab;

    Vector2Int gridSize;


    /// <summary>
    /// Return  object position given grid coordinates
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-gridSize.x / 2 + 0.5f + x, 0f, -gridSize.y / 2 + 0.5f + y);
    }

    /// <summary>
    /// Generates a grid of dead cells
    /// </summary>
    /// <param name="width">The grids width</param>
    /// <param name="height">The grids height</param>
    /// <returns></returns>
    public Cell[,] GenerateGrid(int width, int height)
    {
        gridSize = new Vector2Int(width, height);
        string holderName = "Grid Holder";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }
        Transform gridHolder = new GameObject(holderName).transform;
        gridHolder.parent = transform;

        Cell[,] cellGrid = new Cell[gridSize.x, gridSize.y];


        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 cellPosition = CoordToPosition(x, y);
                Cell newCell = Instantiate(cellPrefab, cellPosition, Quaternion.Euler(Vector3.right * 90f), gridHolder);
                newCell.SetAlive(false);
                cellGrid[x, y] = newCell;
            }
        }

        Camera.main.GetComponent<CameraController>().UpdateCameraPosition(gridSize);

        return cellGrid;
    }


}
