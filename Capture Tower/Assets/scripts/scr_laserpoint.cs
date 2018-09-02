using UnityEngine;

public class scr_laserpoint : MonoBehaviour {
    public static LineRenderer linerenderer;
    public Transform Tower;
    public Vector3 offset;
    
    // Use this for initialization
    void Start() {
        linerenderer = GetComponent<LineRenderer>();
        linerenderer.enabled = false;
        linerenderer.useWorldSpace = true;
        transform.position = Tower.position + offset;
        
    }
	
	// Update is called once per frame
	void Update () {
        linerenderer.SetPosition(0, (Vector2)transform.position);
        linerenderer.SetPosition(1, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));

    if (Input.GetKey("mouse 0"))
        {
        if (GameController.Mana > 0 && GameController.skill == 1)
            {
              linerenderer.enabled = true;
              GameController.Mana -= 2;
            }
        }
    else
        {
        linerenderer.enabled = false;
        }

    if (GameController.Mana <= 0)
        {
        linerenderer.enabled = false;
        }

        
    }
}