using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public int nbMap = 0;
    public float targetPos = -1.5f;

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
            other.transform.position = new Vector2(transform.position.x, targetPos);
        }
    }
}
