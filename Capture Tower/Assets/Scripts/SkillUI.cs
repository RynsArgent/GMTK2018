using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour {

    public Text UI;
    void Update()
    {
        {
            UI.text = "Skill: " + GameController.skill;
        }
    }
}

