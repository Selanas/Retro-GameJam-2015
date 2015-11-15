using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Screen.SetResolution(128, 128, true);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
            Application.LoadLevel(1);
	}
}
