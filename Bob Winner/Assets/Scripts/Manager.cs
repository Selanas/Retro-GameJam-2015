using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

    public static GameObject currMap;

    public static GameObject[] Maps;
    public GameObject[] maps;

	// Use this for initialization
	void Start () {
        Maps = maps;

        foreach(GameObject map in Maps) {
            map.SetActive(false);
        }

        Maps[0].SetActive(true);
        currMap = Maps[0];
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
