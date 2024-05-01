using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    public int rows = 10;
    public int columns = 10;
    public int scale = 1;
    public GameObject gridPrefab;
    public Vector3 leftBottomLocation = new Vector3(0, 0, 0);

    void Awake()
    {
        // I don't understand why this if/else doesn't want braces
        if(gridPrefab)
            GenerateGrid(); 
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

            }
        }
    }
}
