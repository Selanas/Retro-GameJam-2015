using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

    public static bool End = false;
    public static bool AllRelics = false;
    public static GameObject currMap;

    public static GameObject[] Maps;
    public GameObject[] maps;

	// Use this for initialization
	void Start () {

        End = false;
        AllRelics = false;

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
