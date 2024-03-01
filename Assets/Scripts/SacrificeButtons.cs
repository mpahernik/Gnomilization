using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SacrificeButtons : MonoBehaviour
{
    public TMP_InputField WheatInput;
    public TMP_InputField ClayInput;
    public TMP_InputField WoodInput;
    public TMP_InputField PopInput;
    public GameObject manager;
    public GameObject Changecost;

    int AddedAmount;

    

    public void TurnOff()
    {
        this.gameObject.SetActive(false);
    }

    public void AddWheat()
    {
        if (int.Parse(WheatInput.text) <= GameManager.Wheat && int.Parse(WheatInput.text) <= GameManager.SacrificeAmount)
        {
            AddedAmount = int.Parse(WheatInput.text);
            GameManager.Wheat -= AddedAmount;
            GameManager.SacrificeAmount -= AddedAmount;
            GameManager.Score += AddedAmount;
            manager.GetComponent<GameManager>().SetErrorText("Added: " + AddedAmount + " to sacrifice pool");
            manager.GetComponent<GameManager>().UpdateUI();
        }

    }
    public void AddClay()
    {
        if (int.Parse(ClayInput.text) <= GameManager.Clay && int.Parse(ClayInput.text) <= GameManager.SacrificeAmount)
        {
            AddedAmount = int.Parse(ClayInput.text);
            GameManager.Clay -= AddedAmount;
            GameManager.SacrificeAmount -= AddedAmount;
            GameManager.Score += AddedAmount;
            manager.GetComponent<GameManager>().UpdateUI();
        }
    }
    public void AddWood()
    {
        if (int.Parse(WoodInput.text) <= GameManager.Wood && int.Parse(WoodInput.text) <= GameManager.SacrificeAmount)
        {
            AddedAmount = int.Parse(WoodInput.text);
            GameManager.Wood -= AddedAmount;
            GameManager.SacrificeAmount -= AddedAmount;
            GameManager.Score += AddedAmount;
            manager.GetComponent<GameManager>().UpdateUI();
        }
    }
    public void AddPop()
    {
        if (int.Parse(PopInput.text) <= GameManager.Population && int.Parse(PopInput.text) <= GameManager.SacrificeAmount)
        {
            AddedAmount = int.Parse(PopInput.text);
            GameManager.Population -= AddedAmount;
            GameManager.SacrificeAmount -= AddedAmount;
            GameManager.Score += AddedAmount;
            manager.GetComponent<GameManager>().UpdateUI();
        }
    }

    public void Changecards()
    {
        if (GameManager.CardChangecost < GameManager.Gold)
        {
            GameManager.CardChange = true;
            GameManager.Gold -= GameManager.CardChangecost;
            GameManager.CardChangecost++;
            Changecost.GetComponent<TextMeshProUGUI>().text = GameManager.CardChangecost.ToString();
            manager.GetComponent<GameManager>().goldamt.GetComponent<TextMeshProUGUI>().text = GameManager.Gold.ToString();
            Debug.Log(GameManager.CardChangecost);
        }
        else
        {
            manager.GetComponent<GameManager>().SetErrorText("Not enough gold");
        }

    }

    public void SellCards()
    {
        if (GameManager.CardSelected)
        {
            Debug.Log("alo alo");
            Destroy(GameManager.lastselected);
            GameManager.CardSelected = false;
            GameManager.Gold += 5;
            GameManager.TurnTrigger = true;
        }


    }
}
