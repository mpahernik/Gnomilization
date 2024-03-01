using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public int positionx;
    public int positiony;
    public int Forestchance = 23, Claychance = 8;
    public bool HasBuilding = false;
    public enum tiletype { normal, clay, forest};

    public tiletype TileType;
    
    public List<GameObject> TilesInRangeTwo = new List<GameObject>();
    public List<GameObject> TilesInRangeOne = new List<GameObject>();

    int targetx, targety;

    private void Start()
    {
        int i = Random.Range(0, 100);
        //Tiles in range
        var list = transform.parent.gameObject.GetComponent<HexGrid>().ListofTiles;
        foreach(GameObject tile in list)
        {
            targetx = tile.GetComponent<TileController>().positionx;
            targety = tile.GetComponent<TileController>().positiony;
            if (targetx - positionx == 0 && targety - positiony == 0)
            {
              //  Debug.Log("Same tile");
            }
            //the tile is not the same, check if the tile is in range
            else if (Mathf.Abs(targetx - positionx) + Mathf.Abs(targety - positiony) <=3 )
            {
                //case of same row or collum as central tile, only 
                if ((targetx - positionx == 0 || targety - positiony == 0))
                {
                    if(Mathf.Abs(targetx - positionx) + Mathf.Abs(targety - positiony) <= 2)
                    {
                     
                        TilesInRangeTwo.Add(tile);
                    }
                }
                //if tile is not in the same row or collum as the central tile and added abs value is less or equal to 3
                else
                {
                    //check for the displacemnt that adds extra 2 tiles
                    if (positionx % 2 == 0)
                    {
                        if (Mathf.Abs(positionx - targetx) == 1 && targety - positiony == 2)
                        {
                         //   Debug.Log("necemo ove parne");
                        }
                        else
                        {
                            TilesInRangeTwo.Add(tile);
                        }
                    }
                    else if (positionx % 2 != 0)
                    {
                        if (Mathf.Abs(positionx - targetx) == 1 && targety - positiony == -2)
                        {
                          //  Debug.Log("necemo ove neparne");
                        }
                        else
                        {
                            TilesInRangeTwo.Add(tile);
                        }
                    }
                    else
                    {
                        TilesInRangeTwo.Add(tile);
                    }
                }
            }           
        }
        
        //make a list for tiles in range one
        foreach (GameObject reducedtiles in TilesInRangeTwo)
        {
            
            targetx = reducedtiles.GetComponent<TileController>().positionx;
            targety = reducedtiles.GetComponent<TileController>().positiony;
            if (Mathf.Abs(targetx - positionx) + Mathf.Abs(targety - positiony) <= 2)
            {
                //case of same row or collum as central tile, only 
                if ((targetx - positionx == 0 || targety - positiony == 0))
                {
                    if (Mathf.Abs(targetx - positionx) + Mathf.Abs(targety - positiony) <= 1)
                    {

                        TilesInRangeOne.Add(reducedtiles);
                    }
                }
                //if tile is not in the same row or collum as the central tile and added abs value is less or equal to 3
                else
                {
                    //check for the displacemnt that adds extra 2 tiles
                    if (positionx % 2 == 0)
                    {
                        if (Mathf.Abs(positionx - targetx) == 1 && targety - positiony == 1)
                        {
                            //   Debug.Log("necemo ove parne");
                        }
                        else
                        {
                            TilesInRangeOne.Add(reducedtiles);
                        }
                    }
                    else if (positionx % 2 != 0)
                    {
                        if (Mathf.Abs(positionx - targetx) == 1 && targety - positiony == -1)
                        {
                            //  Debug.Log("necemo ove neparne");
                        }
                        else
                        {
                            TilesInRangeOne.Add(reducedtiles);
                        }
                    }
                    else
                    {
                        TilesInRangeOne.Add(reducedtiles);
                    }
                }
            }
        }

        //weights for tile spawns
        foreach (GameObject tile in TilesInRangeOne)
        {
            if(tile.GetComponent<TileController>().TileType == tiletype.clay)
            {
               
                i -= 20;
                break;
            }
            if( tile.GetComponent<TileController>().TileType == tiletype.forest)
            {
                i += 20;
                break;
            }
        }
        //type of tile
        
        if (i <= Claychance - GameManager.claycounter)
        {
            TileType = tiletype.clay;
            this.GetComponent<SpriteRenderer>().color = Color.yellow;
            GameManager.claycounter++;
        }
        else if (i > 100 - Forestchance + GameManager.forestcounter)
        {
            TileType = tiletype.forest;
            if (TileType == tiletype.forest) this.GetComponent<SpriteRenderer>().color = Color.green;
            GameManager.forestcounter++;
        }
        else
        {
            TileType = tiletype.normal;
        }
    }
    private void OnMouseEnter()
    {
        
        transform.localScale = transform.localScale * 1.1f;
    }
    private void OnMouseExit()
    {
        transform.localScale = transform.localScale / 1.1f;
    }

    //Used for debugging
    /*
    private void OnMouseDown()
    {
        foreach(GameObject tile in TilesInRangeOne)
        {
            tile.GetComponent<SpriteRenderer>().color = Color.red;
        }
    } */ 
}
