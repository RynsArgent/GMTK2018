/*using UnityEngine;

public class scr_laserpoint : MonoBehaviour {
    private LineRenderer linerenderer;
    public Transform laserPoint;
	// Use this for initialization
	void Start () {
        linerenderer = GetComponent<LineRenderer>();
        linerenderer.enabled = false;
        linerenderer.useWorldSpace = true;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Input.mousePosition);
        Debug.DrawLine(transform.position, hit.point);
        laserPoint.position = hit.point;
        linerenderer.SetPosition(0, transform.position);
        linerenderer.SetPosition(1, laserPoint.position);
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
