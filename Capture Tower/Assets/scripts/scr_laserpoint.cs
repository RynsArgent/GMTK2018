using UnityEngine;

public class scr_laserpoint : MonoBehaviour {
    public static LineRenderer linerenderer;
    public Transform Tower;
    public Vector3 offset;
    private int laserTimer = 0;
    
    private Collider2D[] mouseResults = new Collider2D[64];
    private int targetLayerMask;
    
    // Use this for initialization
    void Start() {
        targetLayerMask = LayerMask.GetMask("UnitBounds");
        linerenderer = GetComponent<LineRenderer>();
        linerenderer.enabled = false;
        linerenderer.useWorldSpace = true;
        transform.position = Tower.position + offset;
        
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 worldMousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        linerenderer.SetPosition(0, (Vector2)transform.position);
        linerenderer.SetPosition(1, worldMousePos);

        if (Input.GetKey("mouse 0"))
        {
            if (GameController.Mana > 0)
            {
                linerenderer.enabled = true;
                if (laserTimer < 10)
                {
                    laserTimer++;
                }
                else
                {
                    // Deal damage to first enemy found in within point.
                    int resultCount = Physics2D.OverlapPointNonAlloc(worldMousePos, mouseResults, targetLayerMask);
                    for (int i = 0; i < resultCount; i++)
                    {
                        Collider2D result = mouseResults[i];
                        Enemy enemy = result.GetComponentInParent<Enemy>();
                        if (enemy != null)
                        {
                            enemy.Damage(1);
                            break;
                        }
                    }

                    GameController.Mana -= 2;
                    laserTimer = 0;
                }
            }
        }
        else
        {
            linerenderer.enabled = false;
        }

        if (Input.GetKeyDown("mouse 1"))
        {
            if (GameController.Mana > 0)
            {
                // Deal damage to first enemy found in within point.
                int resultCount = Physics2D.OverlapPointNonAlloc(worldMousePos, mouseResults, targetLayerMask);
                for (int i = 0; i < resultCount; i++)
                {
                    Collider2D result = mouseResults[i];
                    Enemy enemy = result.GetComponentInParent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.OnClickUnitBounds();
                        break;
                    }
                }

            }
        }


        if (GameController.Mana <= 0)
        {
            linerenderer.enabled = false;
        }

        
    }
}