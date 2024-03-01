using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuilding", menuName ="Building")]
public class Building_Script : ScriptableObject
{
    public string BuildingName;
    public int CostWh, CostC, CostWo,CostPop, CostGold;
    public int CostIcr;
    public Sprite Icon;
    public int IncomeWh, IncomeC, IncomeWo, IncomePop, IncomeSwo, IncomeGold;
    public int range;
    public int IncrperBld, DecrperBld;
    public enum btype {Farm, ClayMine, LumberMill, Granary, Sawmill, House, Warcamp, Bank};
    public btype Buildingtype;

    public enum inctype {IncWheat, IncClay, IncWood,IncPop,IncSword,IncGold };
    public inctype IncomeType;
}
