using UnityEngine;
using System.Collections;

public class BeeSprite : MonoBehaviour {

    public GameObject HurtSFX;

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

        if (Manager.End)
            return;

        transform.Translate(Vector2.left * Speed * Time.deltaTime);
	}

    void OnTriggerEnter2D (Collider2D other) {

        if (Manager.End)
            return;

        if (other.gameObject.CompareTag("Kick")) {
            Instantiate(HurtSFX, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Punch")) {
            Instantiate(HurtSFX, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Bullet")) {
            Instantiate(HurtSFX, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Whip")) {
            Instantiate(HurtSFX, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

}
