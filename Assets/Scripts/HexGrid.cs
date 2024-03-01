using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public int gridSizeX = 5; // Number of hexagons in the X-axis
    public int gridSizeY = 5; // Number of hexagons in the Y-axis
    public float hexRadius = 1f; // Radius of the hexagon
    public GameObject Altar;

    public List<GameObject> ListofTiles = new List<GameObject>();

    public GameObject hexPrefab; // Prefab for the hexagon

    void Awake()
    {
        GenerateHexGrid();
    }

    private void Start()
    {
        GameObject chosentile = ListofTiles[Random.Range(0,ListofTiles.Count)];
        chosentile.GetComponent<TileController>().HasBuilding = true;
        Instantiate(Altar, chosentile.transform);
    }


    void GenerateHexGrid()
    {
        float hexWidth = Mathf.Sqrt(3) * hexRadius;
        float hexHeight = 1.5f * hexRadius;
        //create grid with hexes
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                float offsetX = x * hexWidth * 0.75f; // Offset for every other column
                float offsetY = y * hexHeight;

                if (x % 2 == 1)
                {
                    offsetY += hexHeight * 0.5f;
                }

                Vector3 hexPosition = new Vector3(offsetX, offsetY, 0);
                hexPrefab.GetComponent<TileController>().positionx = x;
                hexPrefab.GetComponent<TileController>().positiony = y;
                GameObject tilecreated = (GameObject)GameObject.Instantiate(hexPrefab, hexPosition, Quaternion.identity, transform);
                ListofTiles.Add(tilecreated);
            }
        }


       
    }
}
