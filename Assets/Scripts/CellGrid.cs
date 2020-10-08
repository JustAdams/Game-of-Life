using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGrid : MonoBehaviour
{
    public Transform cellPrefab;

    public Cell[,] cellGrid;
    GameObject grid;
    public Vector2 gridSize;


    int generation;

    private void Start()
    {
        generation = 0;
    }


    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-gridSize.x / 2 + 0.5f + x, 0f, -gridSize.y / 2 + 0.5f + y);
    }

    public void GenerateGrid()
    {
        string holderName = "Grid Holder";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }
        Transform gridHolder = new GameObject(holderName).transform;
        gridHolder.parent = transform;



        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 cellPosition = CoordToPosition(x, y);
                Transform newCell = Instantiate(cellPrefab, cellPosition, Quaternion.Euler(Vector3.right * 90)) as Transform;
                newCell.parent = gridHolder;
            }
        }
    }



    /*****************************************************************/


    public Cell GetCell(int xPos, int yPos)
    {
        return null;
    }

    public int GetNeighborCount(int xPos, int yPos)
    {
        int neighbors = 0;

        for (int x = -1; x < 1; x++)
        {
            for (int y = -1; y < 1; y++)
            {
                neighbors += GetCell(x, y).isAlive ? 1 : 0;
            }
        }

        // Subtract the current cell if it's alive
        return neighbors -= GetCell(xPos, yPos).isAlive ? 1 : 0;
    }




    public void RandomizeGrid()
    {

    }


    public void SimulateGeneration()
    {
        generation++;

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                int neighborCount = GetNeighborCount(x, y);
                bool alive = false;
                if (GetCell(x, y).isAlive)
                {
                    if (neighborCount <= 1 || neighborCount >= 4)
                    {
                        alive = false;
                    } else
                    {
                        alive = true;
                    }
                } else
                {
                    if (neighborCount == 3)
                    {
                        alive = true;
                    } else
                    {
                        alive = false;
                    }
                }
                GetCell(x, y).SetAlive(alive);
            }
        }
    }
}
