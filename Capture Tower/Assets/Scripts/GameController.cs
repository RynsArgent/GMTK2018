using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static int Mana;
    public static int skill = 1;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("1"))
        {
            skill = 1;
        }
        if (Input.GetKeyDown("2"))
        {
            skill = 2;
        }

        if (scr_laserpoint.linerenderer.enabled == false)
        {
            if (Mana < 200)
            {
                Mana += 1;
            }
        }
    }

    
    

}
