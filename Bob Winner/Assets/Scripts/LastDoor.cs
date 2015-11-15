using UnityEngine;
using System.Collections;

public class LastDoor : MonoBehaviour {

    public float EndTime = 2;

    public GameObject sprite;
    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = sprite.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Ending () {
        yield return new WaitForSeconds(EndTime);
        Application.LoadLevel(2);
    }

    void OnTriggerEnter2D (Collider2D other) {
        if(other.gameObject.CompareTag("Hero") && Manager.AllRelics) {
            anim.SetTrigger("Open");
            Manager.End = true;
            StartCoroutine(Ending());
        }
    }
}
