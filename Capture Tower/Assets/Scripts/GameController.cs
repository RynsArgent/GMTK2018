using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static int Mana = 40;
    public static int skill = 1;
    public int ManaDelay = 7;
    public int ManaRegen = 1;
    private int ManaTimer = 0;
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

        if (scr_laserpoint.linerenderer != null && scr_laserpoint.linerenderer.enabled)
        {
            if (Mana < 500)
            {
                if (ManaTimer < ManaDelay)
                {
                    ManaTimer++;
                }
                else
                {
                    Mana += ManaRegen;
                    ManaTimer = 0;
                }
            }
        }
    }

    
    

}
