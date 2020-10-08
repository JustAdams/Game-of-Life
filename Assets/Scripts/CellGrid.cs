using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGrid : MonoBehaviour
{
    public Cell cellPrefab;

    public Cell[,] cellGrid;
    GameObject grid;
    public Vector2 gridSize;


    int generation;

    private void Start()
    {
        generation = 0;
        GenerateGrid();
        StartCoroutine(RunSimulation());
    }


    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-gridSize.x / 2 + 0.5f + x, 0f, -gridSize.y / 2 + 0.5f + y);
    }

    public void DisplayCellGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                cellGrid[x, y].UpdateCellDisplay();
            }
        }
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

        cellGrid = new Cell[(int)gridSize.x, (int)gridSize.y];


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

       // RandomizeGrid();
    }



    /*****************************************************************/

    /** Simulator **/

    public bool isSimulating;

    public Cell GetCell(int xPos, int yPos)
    {
        return cellGrid[xPos, yPos];
    }

    public int GetNeighborCount(int xPos, int yPos)
    {
        int neighbors = 0;

        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                try
                {
                    neighbors += GetCell(xPos + x, yPos + y).isAlive ? 1 : 0;
                }
                catch { }
            }
        }

        // Subtract the current cell if it's alive
        neighbors -= GetCell(xPos, yPos).isAlive ? 1 : 0;
        return neighbors;
    }

    public void RandomizeGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                int ran = Random.Range(0, 5);
                GetCell(x, y).SetAlive(ran % 5 == 0);
            }
        }
    }


    public void SimulateGeneration()
    {
        generation++;

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                int neighborCount = GetNeighborCount(x, y);
                bool alive;
                if (GetCell(x, y).isAlive)
                {
                    if (neighborCount < 2 || neighborCount > 3)
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

    public IEnumerator RunSimulation()
    {
        while (true)
        {
            if (isSimulating)
            {
               SimulateGeneration();
               DisplayCellGrid();
            }
            yield return null;
        }
    }

}
