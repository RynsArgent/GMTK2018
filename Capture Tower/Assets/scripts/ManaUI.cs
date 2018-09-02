using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManaUI : MonoBehaviour {
    public scr_laserpoint manaPoints;
    public Text UI;

    void Update () {
        if (manaPoints == null)
        {
            return;
        }
        UI.text = "Mana: " + manaPoints.Mana;
    }
}
