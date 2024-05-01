using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    [Header("Grid Sizing")]
    public int rows = 10;
    public int columns = 10;
    public int scale = 1;

    [Header("Asset Object to Tile")]
    public GameObject gridPrefab;

    [Header("Grid Placement in World")]
    public Vector3 leftBottomLocation = new Vector3(0, 0, 0);
    public GameObject[,] gridArray;
    public int startX = 0;
    public int startY = 0;
    public int endX = 2;
    public int endY = 2;

    [Header("Randomise?")]
    public bool YesButNotWorkingYet;

    void Awake()
    {
        // To create the array in the size of the columns & rows declared in inspector
        gridArray = new GameObject[columns, rows];

        // Automate grid creation on awake - can assign to multiple diff empties to set the grid pattern & potentially randomise in future.
        if(gridPrefab)
        {
            GenerateGrid(); 
        }
        else print("Missing gridprefab, please assign.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateGrid()
    {
        for(int i = 0; i < columns; i++)
        {
            for(int j = 0; j < rows; j++)
            {
                // Instatiate grid rows & columms; y scaling not needed.
                GameObject obj = Instantiate(gridPrefab, new Vector3(leftBottomLocation.x + scale * i, leftBottomLocation.y, leftBottomLocation.z + scale * j), Quaternion.identity);
                obj.transform.SetParent(gameObject.transform);

                // Giving each grid coordinate a different value
                obj.GetComponent<GridStat>().x = i;
                obj.GetComponent<GridStat>().y = j;
                print("helloooooooo?");

                // v When instantiated, add to grid:
                gridArray[i, j] = obj;

            }
        }
    }
}
