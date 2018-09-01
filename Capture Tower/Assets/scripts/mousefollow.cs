using UnityEngine;

public class mousefollow : MonoBehaviour {
        // Update is called once per frame
        void Update () {
        Vector2 temp = Input.mousePosition;
        transform.position = Camera.main.ScreenToWorldPoint(temp);
    } 
}
