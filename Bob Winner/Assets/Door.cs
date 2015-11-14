using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public int nbMap = 0;
    public float targetPos = -1.5f;

    public bool Horizontal = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D (Collider2D other) {
        if(other.gameObject.CompareTag("Hero")) {
            Manager.currMap.SetActive(false);
            Manager.currMap = Manager.Maps[nbMap];
            Manager.currMap.SetActive(true);

            if(Horizontal)
                other.transform.position = new Vector2(targetPos, transform.position.y);
            else
                other.transform.position = new Vector2(transform.position.x, targetPos);
        }
    }
}
