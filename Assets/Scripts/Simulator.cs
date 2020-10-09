
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simulator : MonoBehaviour
{
    public Button simulateButton;
    public Button resetButton;
    public Text generationText;

    public CellGrid cellGridObject;

    public Cell[,] cellGrid;
    public Vector2Int gridSize;

    public bool isSimulating;

    public void SetHeight(int height)
    {
        gridSize.y = height;    
    }

    public void SetWidth(int width)
    {
        gridSize.x = width;
    }

    public void SetSimSpeed(float speed)
    {
        simSpeed = speed;
    }

    int generation;
    public float simSpeed = 1f;

    private void Start()
    {
        CreateNewGrid();
    }


    public void CreateNewGrid()
    {
        StopCoroutine(RunSimulation());
        isSimulating = false;
        generation = 0;
        cellGrid = cellGridObject.GenerateGrid(gridSize.x, gridSize.y);
        StartCoroutine(RunSimulation());
    }

    /// <summary>
    /// Loop through the grid and update the cell graphics
    /// </summary>
    public void DisplayCellGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                cellGrid[x, y].UpdateAlive();
            }
        }
    }

    public Cell GetCell(int xPos, int yPos)
    {
        return cellGrid[xPos, yPos];
    }

    /// <summary>
    /// Get and return the count of living cells in the grid touching the given cell
    /// </summary>
    /// <param name="xPos"></param>
    /// <param name="yPos"></param>
    /// <returns></returns>
    public int GetNeighborCount(int xPos, int yPos)
    {
        int neighbors = 0;

        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                neighbors += GetCell(xPos + x, yPos + y).isAlive ? 1 : 0;
            }
        }

        // Subtract the current cell if it's alive
        neighbors -= GetCell(xPos, yPos).isAlive ? 1 : 0;
        return neighbors;
    }

    private void Update()
    {
        generationText.text = "Generation: " + generation;
    }

    /// <summary>
    /// Set a randomized grid of living cells
    /// </summary>
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

    /// <summary>
    /// One generation of the Game of Life
    /// Loops through every grid position and determines whether or not that cell is alive or dead
    /// </summary>
    public void SimulateGeneration()
    {
        generation++;

        for (int x = 1; x < gridSize.x - 1; x++)
        {
            for (int y = 1; y < gridSize.y - 1; y++)
            {
                int neighborCount = GetNeighborCount(x, y);
                bool alive;
                if (GetCell(x, y).isAlive)
                {
                    if (neighborCount < 2 || neighborCount > 3)
                    {
                        alive = false;
                    }
                    else
                    {
                        alive = true;
                    }
                }
                else
                {
                    if (neighborCount == 3)
                    {
                        alive = true;
                    }
                    else
                    {
                        alive = false;
                    }
                }
                GetCell(x, y).SetTempAlive(alive);
            }
        }
    }

    /// <summary>
    /// Start or pause the simulation
    /// </summary>
    public void ToggleSimulation()
    {
        isSimulating = !isSimulating;
        if (isSimulating)
        {
            simulateButton.GetComponentInChildren<Text>().text = "Stop Simulation";
        } else
        {
            simulateButton.GetComponentInChildren<Text>().text = "Start Simulation";
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
            yield return new WaitForSeconds(simSpeed);
        }
    }
}
