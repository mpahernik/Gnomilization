using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header ("Resources and Incomes")]
    public static int Wheat=10;
    public static int WheatIncome=0;
    public static int Clay=10;
    public static int ClayIncome=0;
    public static int Wood=10;
    public static int WoodIncome=0;
    public static int Population = 1;
    public static int PopulationInc = 0;
    public static int Swords = 0;
    public static int SwordsInc = 0;
    public static int Gold = 5;
    public static int Goldinc = 0;
    public static int Level = 1;
    
    [Header ("UI objects")]    
    public GameObject wheatamt;
    public GameObject wheatinc;
    public GameObject clayamt;
    public GameObject clayinc;
    public GameObject woodamt;
    public GameObject woodinc;
    public GameObject popatm;
    public GameObject popinc;
    public GameObject swordamt;
    public GameObject swordinc;
    public GameObject goldamt;
    public GameObject goldinc;
    public GameObject turncounter;
    public GameObject sacrificeamount;
    public GameObject CardsBar;
    public GameObject score;
    public GameObject ErrorText;
    public GameObject SacPanel;
    [Header("Misclenious")]
    public static bool TurnTrigger;
    public static bool CardSelected = false;
    public static bool CardChange = false;  
    public static int claycounter=0;
    public static int forestcounter=0;
    public static int TurnCounter = 0;
    public static int SacrificeAmount = 10;
    public static int CardChangecost = 1;
    public static int Score = 0;
    bool TurnCntCheck = false;
    public static GameObject lastselected;
    public static GameObject cardselected;
    // Start is called before the first frame update

    private void Awake()
    {
        ErrorText.SetActive(false);
        TurnTrigger = false;
    }
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(TurnTrigger);
        if (TurnTrigger)
        {
            if (!TurnCntCheck)
            {
                TurnCounter += 1;
                TurnCntCheck = true;
            }
            if (TurnCounter % 5 == 0 && TurnCounter != 0)
            {
                if (SacrificeAmount != 0)
                {
                    SacPanel.SetActive(true);
                    //problem nisi sac obavio otvaramo panel i NE trigeramo turn
                }
                else
                {//sve je ok napravi ko da je normalni turn
                    
                    Wheat += WheatIncome;
                    Clay += ClayIncome;
                    Wood += WoodIncome;
                    Population += PopulationInc;
                    Gold += Goldinc;
                    Swords += SwordsInc;
                    Level += 1;
                    TurnTrigger = false;
                    SacrificeAmount = 10 * Level * Level;
                    //enable new cards
                    foreach (Transform cardslot in CardsBar.transform)
                    {
                        cardslot.gameObject.SetActive(true);
                    }
                    TurnCntCheck = false;

                }



                //veci dio za sacrifice, ako nije 0 onda otvaraj panel, ako zbroj wheat, clay wood pop manji od traženog sac game over
               

            }


            else
            {
                
                Wheat += WheatIncome;
                Clay += ClayIncome;
                Wood += WoodIncome;
                Population += PopulationInc;
                Gold += Goldinc;
                Swords += SwordsInc;
                
                TurnTrigger = false;
                TurnCntCheck = false;

            }
            UpdateUI();
        }

                      

        
    }

    public void UpdateUI()
    {
        wheatamt.gameObject.GetComponent<TextMeshProUGUI>().text = Wheat.ToString();
        wheatinc.gameObject.GetComponent<TextMeshProUGUI>().text ="+" + WheatIncome.ToString();
        clayamt.gameObject.GetComponent<TextMeshProUGUI>().text = Clay.ToString();
        clayinc.gameObject.GetComponent<TextMeshProUGUI>().text = "+" + ClayIncome.ToString();
        woodamt.gameObject.GetComponent<TextMeshProUGUI>().text = Wood.ToString();
        woodinc.gameObject.GetComponent<TextMeshProUGUI>().text = "+" + WoodIncome.ToString();
        popatm.gameObject.GetComponent<TextMeshProUGUI>().text = Population.ToString();
        popinc.gameObject.GetComponent<TextMeshProUGUI>().text = "+" + PopulationInc.ToString();
        swordamt.gameObject.GetComponent<TextMeshProUGUI>().text = Swords.ToString();
        swordinc.gameObject.GetComponent<TextMeshProUGUI>().text = "+" + SwordsInc.ToString();
        goldamt.gameObject.GetComponent<TextMeshProUGUI>().text = Gold.ToString();
        goldinc.gameObject.GetComponent<TextMeshProUGUI>().text = "+" + Goldinc.ToString();
        turncounter.gameObject.GetComponent<TextMeshProUGUI>().text = TurnCounter.ToString();
        sacrificeamount.gameObject.GetComponent<TextMeshProUGUI>().text = SacrificeAmount.ToString();
        score.gameObject.GetComponent<TextMeshProUGUI>().text = Score.ToString();
    }

    public void GameOver(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void SetErrorText(string s)
    {
        ErrorText.SetActive(true);
        ErrorText.GetComponent<TextMeshProUGUI>().text = s;
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(2.5f);
        ErrorText.SetActive(false);
        
    }
}
