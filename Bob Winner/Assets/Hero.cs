using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hero : MonoBehaviour {

    public GameObject sprite;

    [Header("HUD")]
    public Text lifeText01;
    public Text lifeText02;

    [Header("Life")]
    public int lifeMax = 100;
    private int life = 100;
    private bool damaged = false;

    [Header("Attack")]
    public GameObject Attack;
    public float attackTime = 0.3f;

    [Header("Movement")]
    public float maxSpeed = 10f;
    private Vector2 move = Vector2.zero;

    [Header("Rotation")]
    bool facingRight = true;

    private Rigidbody2D body2D;
    private Animator anim;

    // Use this for initialization
    void Start() {
        // Screen.SetResolution(128, 128, true);
        anim = sprite.GetComponent<Animator>();
        body2D = GetComponent<Rigidbody2D>();
        Attack.SetActive(false);
        life = lifeMax;
        damaged = false;
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

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    void Update() {

        anim.SetFloat("Velocity", body2D.velocity.magnitude);

        lifeText01.text = "Vie: " + life + "%";
        lifeText02.text = "Vie: " + life + "%";

        if (Input.GetButtonDown("Attack") && !Attack.activeSelf) {
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

    IEnumerator TakeDamage () {
        damaged = true;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = true;
        damaged = false;
    }

    void OnTriggerEnter2D (Collider2D other) {
        if(other.gameObject.CompareTag("EnemyAttack") && !damaged) {
            StartCoroutine(TakeDamage());
            Destroy(other.gameObject);
            life -= other.gameObject.GetComponent<Damage>().dmg;
        }

        if (other.gameObject.CompareTag("Trap") && !damaged) {
            StartCoroutine(TakeDamage());
            life -= other.gameObject.GetComponent<Damage>().dmg;
        }

        if (other.gameObject.CompareTag("Enemy") && !damaged) {
            StartCoroutine(TakeDamage());
            life -= other.gameObject.GetComponent<Damage>().dmg;
        }
    }
}
