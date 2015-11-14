using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    [Header("Attack")]
    public GameObject Attack;
    public float attackTime = 0.3f;

    [Header("Movement")]
    public float maxSpeed = 10f;
    private Vector2 move = Vector2.zero;

    [Header("Rotation")]
    bool facingRight = true;

    private Rigidbody2D body2D;

    // Use this for initialization
    void Start() {
        body2D = GetComponent<Rigidbody2D>();
        Attack.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (move.x > 0 && !facingRight)
            Flip();
        else if (move.x < 0 && facingRight)
            Flip();

        if (Attack.activeSelf) {
            body2D.velocity = Vector2.zero;
            return;
        }

        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        move.Normalize();
        move.x *= maxSpeed;
        move.y *= maxSpeed*0.8f;
        body2D.velocity = Vector2.Lerp(body2D.velocity, move, Time.deltaTime * 50);
    }

    void Update() {
        if(Input.GetButtonDown("Attack") && !Attack.activeSelf) {
            StartCoroutine(Attacking());
        }
    }

    IEnumerator Attacking () {
        Attack.SetActive(true);
        yield return new WaitForSeconds(attackTime);
        Attack.SetActive(false);
    }

    void Flip() {
        facingRight = !facingRight;
        //Vector3 theScale = transform.localScale;
        transform.eulerAngles += Vector3.up * 180;
        //theScale.x *= -1;
        //transform.localScale = theScale;
    }
}
