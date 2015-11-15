using UnityEngine;
using System.Collections;

public class BeeSprite : MonoBehaviour {

    private Vector3 initPos;
    public float Speed = 3;

    void Awake () {
        initPos = transform.position;
    }
	// Use this for initialization
	void OnEnable() {
        transform.position = initPos;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.left * Speed * Time.deltaTime);
	}

    void OnTriggerEnter2D (Collider2D other) {
        if(other.gameObject.CompareTag("Kick")) {
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Punch")) {
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Bullet")) {
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Whip")) {
            gameObject.SetActive(false);
        }
    }

}
