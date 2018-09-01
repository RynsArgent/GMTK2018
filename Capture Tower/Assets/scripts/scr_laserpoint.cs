using UnityEngine;

public class scr_laserpoint : MonoBehaviour {
    private LineRenderer linerenderer;
    public Transform Tower;
	// Use this for initialization
	void Start () {
        linerenderer = GetComponent<LineRenderer>();
        linerenderer.enabled = false;
        linerenderer.useWorldSpace = true;
        transform.position = Tower.position;
	}
	
	// Update is called once per frame
	void Update () {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
        //laserpoint.position = hit.point;
        linerenderer.SetPosition(0, transform.position);
        linerenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (Input.GetKey("mouse 0"))
        {
            linerenderer.enabled = true;
        }
        else
        {
            linerenderer.enabled = false;
        }


    }
}