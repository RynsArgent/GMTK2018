using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealth : Unit
{
    public GameObject gameOverScreen;

    // Update is called once per frame
    protected override void Update()
    {
    }

    public override void OnTriggerUnitBounds(GameObject other)
    {
        Destroy(other);
        Damage(1);
    }

    public override void OnUnitDied()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }
}