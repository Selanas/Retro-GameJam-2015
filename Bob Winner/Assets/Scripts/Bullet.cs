using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float Speed = 10;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
	}

    void OnTriggerEnter2D (Collider2D other) {
        if(other.gameObject.CompareTag("Decor")) {
            Destroy(gameObject);
        }
    }
}
