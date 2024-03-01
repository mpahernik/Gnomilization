using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Altar : MonoBehaviour
{
    public GameObject Panel;

    public void Awake()
    {
        Panel = GameObject.FindGameObjectWithTag("Panel");
        Panel.SetActive(false);
    }

    private void OnMouseDown()
    {
        Panel.SetActive(true);
    }

    
}
