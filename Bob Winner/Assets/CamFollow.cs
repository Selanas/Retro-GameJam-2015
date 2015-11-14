using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

	public GameObject target;

	public float horizontalDiff = 10f;
	public float verticalDiff = 10f;

	public float horizontalSpeed = 5f;
	public float verticalSpeed = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		float diffX = Mathf.Abs(target.transform.position.x - transform.position.x);

		//if(diffX > horizontalDiff) {
			float X = transform.position.x;
			X = Mathf.Lerp(X,target.transform.position.x,Time.deltaTime *horizontalSpeed);
			transform.position = new Vector3(X,transform.position.y,transform.position.z);
		//}

        Shaking();

    }

    public static float shake = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    void Shaking() {
        if (shake > 0) {
            transform.localPosition = transform.position + Random.insideUnitSphere * shakeAmount;

            shake -= Time.deltaTime * decreaseFactor;
        }
        else {
            shake = 0f;

        }
    }
}
