using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour {

    public Text UI;
    
    void Update()
    {
        {
            if (GameController.skill == 1)
            {
                UI.text = "Magic: Laser";
            }
            else
            {
                UI.text = "Magic: Conversion";
            }
        }
    }
}

