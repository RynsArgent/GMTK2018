using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour {

    public Text UI;
    TowerHealth towerhp;
    private void Start()
    {
        GameObject towerObj = GameObject.Find("Tower");
        towerhp = towerObj.GetComponent<TowerHealth>();
    }
    
   
    void Update ()
    {
       {
            UI.text = "Tower: " + towerhp.hp;
        }
    }
}
