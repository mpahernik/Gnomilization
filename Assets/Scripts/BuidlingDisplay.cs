using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuidlingDisplay : MonoBehaviour
{
    public Building_Script bulding;
    string Buildingname;
    int CostWh, CostC, CostWo, CostPop, CostGold;
    Sprite Icon;
    public int IncomeWh, IncomeC, IncomeWo, IncomePop, IncomeSwo, IncomeGold, CostInc;
    int Range;
    int IncrperBld, DecrperBld;
    public bool isplaced ;
    GameObject _tilehit;
    bool _editing;

    
    private void Awake()
    {
        _editing = true;
        isplaced = false;
        Buildingname = bulding.BuildingName;
        CostWh = bulding.CostWh;
        CostC = bulding.CostC;
        CostWo = bulding.CostWo;
        CostPop = bulding.CostPop;
        CostGold = bulding.CostGold;
        Icon = bulding.Icon;
        IncomeWh = bulding.IncomeWh;
        IncomeC = bulding.IncomeC;
        IncomeWo = bulding.IncomeWo;
        IncomePop = bulding.IncomePop;
        IncomeSwo = bulding.IncomeSwo;
        IncomeGold = bulding.IncomeGold;
        Range = bulding.range;
        IncrperBld = bulding.IncrperBld;
        DecrperBld = bulding.DecrperBld;
        CostInc = bulding.CostIcr;
        gameObject.GetComponent<SpriteRenderer>().sprite = Icon;
        


    }

    void Update()
    {
        
       //setting the building
        if (isplaced == false)
        { 
            //clicked on card and now getting ready to position the building
            var MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MousePos.z = 0;           
            transform.position = MousePos;

            //raycast to check if the tile is hit and free and put the building in position
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Tile"))
                    {
                        
                        if (hit.collider.gameObject.GetComponent<TileController>().HasBuilding == false)
                        {
                            if (bulding.Buildingtype == Building_Script.btype.ClayMine)
                            {
                                //Conditioons for clay mine
                                if (hit.collider.gameObject.GetComponent<TileController>().TileType == TileController.tiletype.clay)
                                {
                                    isplaced = true;
                                    transform.position = hit.collider.transform.position;
                                    hit.collider.gameObject.GetComponent<TileController>().HasBuilding = true;
                                    this.transform.parent = hit.transform;


                                    _tilehit = hit.collider.gameObject;
                                    GameManager.CardSelected = false;
                                }
                                //Conditions for lumberjack
                                
                                else
                                {
                                    Debug.Log("can only be placed on clay tiles");
                                }
                            }
                            else if (bulding.Buildingtype == Building_Script.btype.LumberMill){
                                if (hit.collider.gameObject.GetComponent<TileController>().TileType == TileController.tiletype.forest)
                                {
                                    isplaced = true;
                                    transform.position = hit.collider.transform.position;
                                    hit.collider.gameObject.GetComponent<TileController>().HasBuilding = true;
                                    this.transform.parent = hit.transform;


                                    _tilehit = hit.collider.gameObject;
                                    GameManager.CardSelected = false;
                                }
                                else
                                {
                                    Debug.Log("can only be played on forest tiles");
                                }
                            }
                            else {
                                //Place the tile
                                isplaced = true;
                                transform.position = hit.collider.transform.position;
                                hit.collider.gameObject.GetComponent<TileController>().HasBuilding = true;
                                this.transform.parent = hit.transform;


                                _tilehit = hit.collider.gameObject;
                                GameManager.CardSelected = false;
                            }
                        }
                        else
                        {
                            Debug.Log("The tile has a building already");
                        }
                    }
                }
                else
                {
                    Debug.Log("Ray didn't hit anything.");
                }

            }
        }


        //building is set add incomes
        if (isplaced && _editing)
        {
            Recalculate();
            AddIncome();
            _editing = false;
            GameManager.TurnTrigger = true;
        }
    }

    
    void Recalculate()
    {
        List<GameObject> copylist = new List<GameObject>();
        if(Range == 1) copylist = new List<GameObject>(_tilehit.GetComponent<TileController>().TilesInRangeOne);
        
        if (Range == 2) copylist = new List<GameObject>(_tilehit.GetComponent<TileController>().TilesInRangeTwo); 
        
        //check buildings in range and increase income
        foreach (GameObject tille in copylist){
            
            if (tille.transform.childCount > 0 && tille.transform.GetChild(0).transform.tag == "Altar") Debug.Log("altar");
            
            else if(tille.transform.childCount > 0 && tille.transform.GetChild(0).transform.GetComponent<BuidlingDisplay>().bulding.IncomeType == this.bulding.IncomeType)
           // else if(tille.transform.childCount > 0 && tille.transform.GetChild(0).transform.GetComponent<BuidlingDisplay>().Buildingname == this.Buildingname)
            {
                switch (bulding.Buildingtype)
                {
                    case (Building_Script.btype.Farm):
                        IncomeWh += IncrperBld;
                        break;
                    case (Building_Script.btype.ClayMine):
                        IncomeC += IncrperBld;
                        break;
                    case (Building_Script.btype.LumberMill):
                        IncomeWo += IncrperBld;
                        break;
                    default:
                        Debug.Log("error5");
                        break;

                }
                
            //increase the income on the other buildings with same type and add it to total income    
            tille.transform.GetChild(0).GetComponent<BuidlingDisplay>().IncomeWh += tille.transform.GetChild(0).GetComponent<BuidlingDisplay>().IncrperBld;
            tille.transform.GetChild(0).GetComponent<BuidlingDisplay>().AdditionalIncome();
            }
            //if the building has some reductions with other types check and reduce income
                
            
        }
             
    }
    public void AddIncome()
    {
        
        //increase static resource increases variables by income from scriptable object
        GameManager.WheatIncome += IncomeWh;
        GameManager.ClayIncome += IncomeC;
        GameManager.WoodIncome += IncomeWo;
        GameManager.PopulationInc += IncomePop;
        GameManager.SwordsInc += IncomeSwo;
        GameManager.Goldinc += IncomeGold;
       
    }
    public void AdditionalIncome()
    {
        switch (bulding.Buildingtype)
        {
            case (Building_Script.btype.Farm ):
            case (Building_Script.btype.Granary):
                GameManager.WheatIncome += IncrperBld; ;
                break;
            case (Building_Script.btype.ClayMine):
                GameManager.ClayIncome += IncrperBld;
                break;
            case (Building_Script.btype.LumberMill):
            case (Building_Script.btype.Sawmill):
                GameManager.WoodIncome += IncrperBld;
                break;

            case (Building_Script.btype.House):
                GameManager.PopulationInc += IncrperBld;
                break;
            case (Building_Script.btype.Warcamp):
                GameManager.SwordsInc += IncrperBld;
                break;
            case (Building_Script.btype.Bank):
                GameManager.Goldinc += IncrperBld;
                break;
            default:
                Debug.Log("error");
                break;

        }
       
    }

    void PayCost()
    {
        GameManager.Wheat -= CostWh;
        GameManager.Clay -= CostC;
        GameManager.Wood -= CostWo;
        GameManager.Population -= CostPop;
        GameManager.Gold -= CostGold;       
    }
    
}
