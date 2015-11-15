using UnityEngine;
using System.Collections;

public class Bee : MonoBehaviour {

    public GameObject mainBee;

    public float VSpeed = 1.5f;

    public float Timer = 0.5f;
    private float time = 0;

	// Use this for initialization
	void OnEnable() {
		VSpeed = Mathf.Abs(VSpeed);

        transform.position = mainBee.transform.position + new Vector3(0, 1,0);
        time = 0;
	}
	
	// Update is called once per frame
	void Update () {


        if(time >= Timer) {
            VSpeed = -VSpeed;
            time = 0;
        }

        time += Time.deltaTime;

        transform.Translate(Vector2.up * VSpeed * Time.deltaTime);
	}

}
