﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManaUI : MonoBehaviour {
    public Text UI;

    void Update () {
        UI.text = "Mana: " + GameController.Mana;
    }
}
