using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
    public List<GameObject> path = new List<GameObject>();

    [Header("Set Grid Use On/Off")]
    public bool findDistance = false;

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
       if(findDistance)
        {
            SetDistance();
            SetPath();
            findDistance = false;
        }
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

    void SetDistance()
    {
        InitialSetup();
        int x = startX;
        int y = startY;
        int[] testArray = new int[rows * columns];
        //Check as many steps as possible
        for (int step = 1; step < rows * columns; step++)
        {
            foreach(GameObject obj in gridArray)
            {
                // As long as -1 is true, will check - check if object or if NOT object
                if (obj&&obj.GetComponent<GridStat>().visited == step - 1)
                    TestFourDirections(obj.GetComponent<GridStat>().x, obj.GetComponent<GridStat>().y, step);
            }
        }

    }

    void SetPath()
    {
        int step;
        int x = endX;
        int y = endY;
        
        // Running list of how to get from one point to the next
        List<GameObject> templist = new List<GameObject>();
        
        // Remove previous paths
        path.Clear();

        // If -1, program couldn't find it, cover bases here.
        if (gridArray[endX, endY] &&gridArray[endX,endY].GetComponent<GridStat>().visited > 0)
        {
            path.Add(gridArray[x, y]);
            step = gridArray[x, y].GetComponent<GridStat>().visited - 1;
        }

        // If not less than one, can't get to it - so end function.
        else
        {
            print("Can't reach desired location.");
            return;
        }

        // Test all directions to check if it's going the right going.
        for(int i = step; step > -1; step--)
        {
            if(TestDirection(x,y,step,1))   
                templist.Add(gridArray[x, y + 1]);

            if (TestDirection(x, y, step, 2))
                templist.Add(gridArray[x + 1, y]);

            if (TestDirection(x, y, step, 3))
                templist.Add(gridArray[x, y - 1]);

            if (TestDirection(x, y, step, 4))
                templist.Add(gridArray[x - 1, y]);

            GameObject tempObj = FindClosest(gridArray[endX, endY].transform, templist);
            path.Add(tempObj);
            x = tempObj.GetComponent<GridStat>().x;
            y = tempObj.GetComponent<GridStat>().y;
            templist.Clear();
        }
    }

    void InitialSetup()
    {
        foreach (GameObject obj in gridArray)
        {
            obj.GetComponent<GridStat>().visited = -1;
        }

        gridArray[startX, startY].GetComponent<GridStat>().visited = 0;
    }

    bool TestDirection(int x, int y, int step, int direction)
    {
        // int direction tells which case for 1 is up, 2 is right, 3 is down, 4 is left
        switch (direction)
        {
            // 4 is to the left, so adjust to minus & columns ('> -1' previously '< rows')
            // eg. if (y + 1 < rows && gridArray[x, y + 1] && gridArray[x, y + 1].GetComponent<GridStat>().visited == step)
            // Not sure if something is wrong in 3 or 4

            case 4:
                if (x - 1 > -1 && gridArray[x + 1, y] && gridArray[x + 1, y].GetComponent<GridStat>().visited == step)
                    return true;
                else
                    return false;

            case 3:
                if (y - 1 > -1 && gridArray[x, y - 1] && gridArray[x, y - 1].GetComponent<GridStat>().visited == step)
                    return true;
                else
                    return false;

            case 2:
                if (x + 1 < rows && gridArray[x + 1, y] && gridArray[x + 1, y].GetComponent<GridStat>().visited == step)
                    return true;
                else
                    return false;

            case 1:
                if (y + 1 < rows && gridArray[x, y + 1] && gridArray[x, y + 1].GetComponent<GridStat>().visited == step)
                    return true;
                else
                    return false;
        }
        return false;
    }

    void TestFourDirections(int x, int y, int step)
    {
        // Check Up, down, left, right starting from 0 
        if (TestDirection(x, y, -1, 1))
            SetVisited(x, y + 1, step);

        if (TestDirection(x, y, -1, 2))
            SetVisited(x + 1, y, step);

        if (TestDirection(x, y, -1, 3))
            SetVisited(x, y - 1, step);

        if (TestDirection(x, y, -1, 4))
            SetVisited(x - 1, y, step);
    }

    void SetVisited(int x, int y, int step)
    {
        // Sets visited grid section to the 'step'
        if (gridArray[x, y])
            gridArray[x, y].GetComponent<GridStat>().visited = step;
    }

    GameObject FindClosest(Transform targetLocation, List<GameObject> list)
    {
        float currentDistance = scale * rows * columns;
        int indexNumber = 0;
        for(int i = 0; i<list.Count; i++)
        {
            if(Vector3.Distance(targetLocation.position, list[i].transform.position) < currentDistance)
            {
                currentDistance = Vector3.Distance(targetLocation.position, list[i].transform.position);
                indexNumber = i;
            }
        }
        return list[indexNumber];
    }
}
