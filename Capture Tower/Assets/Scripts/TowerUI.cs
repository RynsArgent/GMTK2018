using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour {

    public Text UI;
    void Update ()
    {
       {
            UI.text = "Tower: " + GameController.TowerHP;
        }
    }
}
