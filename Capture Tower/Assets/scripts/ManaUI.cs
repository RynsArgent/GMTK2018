using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManaUI : MonoBehaviour {
    Text Mana;
    // Update is called once per frame
    void Update () {
        Mana.text = "Mana: " + scr_laserpoint.Mana;
	}
}
