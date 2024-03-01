using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluePrint : MonoBehaviour
{
    [SerializeField] Building_Script Scriptobj;
    public GameObject buildingprefab;
    int i = 0;
    void Awake()
    {
        GetCards();
    }

   
    public void Select()
    {
        if (GameManager.CardChange)
        {
            Debug.Log("test test ");
            GetCards();
            GameManager.CardChange = false;

        }
        else if (GameManager.CardSelected) Debug.Log("glupane imaš kartu");
        else
        {
            buildingprefab.GetComponent<BuidlingDisplay>().bulding = Scriptobj;
            var lastsel = Instantiate(buildingprefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);

            
            GameManager.CardSelected = true;
            gameObject.SetActive(false);
            GameManager.lastselected = lastsel;
        }
        
        
    }

    

    private void OnEnable()
    {
        
        GetCards();
    }

    public void GetCards()
    {       
        switch (GameManager.Level)
        {
            case <= 3:
                i = Random.Range(1, 4);
                break;

            case > 3 and < 7:
                i = Random.Range(1, 7);
                break;
            case >= 7:
                i = Random.Range(1, 9);
                break;


        }
        switch (i)
        {
            case 1:
                Scriptobj = Resources.Load<Building_Script>("Farm");
                break;
            case 2:
                Scriptobj = Resources.Load<Building_Script>("ClayMine");
                break;
            case 3:
                Scriptobj = Resources.Load<Building_Script>("Lumbermill");
                break;
            case 4:
                Scriptobj = Resources.Load<Building_Script>("House");
                break;
            case 5:
                Scriptobj = Resources.Load<Building_Script>("Bank");
                break;
            case 6:
                Scriptobj = Resources.Load<Building_Script>("WarCamp");
                break;
            case 7:
                Scriptobj = Resources.Load<Building_Script>("SawMill");
                break;
            case 8:
                Scriptobj = Resources.Load<Building_Script>("Granary");
                break;

            default:
                Debug.Log("Error2");
                break;
        }

        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Scriptobj.Icon;
    }

}
