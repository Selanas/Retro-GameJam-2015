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
    

    public int maxAttack = 3;
    private int selectedAttack = 0;
    private bool attacking = false;
    

    [Header("Defense")]
    private bool defending = false;
    public float defendTime = 0.3f;

    [Header("Kick")]
    public GameObject Kick;
    public float kickTime = 0.3f;

    [Header("Punch")]
    public bool hasPunch = false;
    public GameObject Punch;
    public float punchTime = 0.3f;

    [Header("Gun")]
    public bool hasGun = false;
    public GameObject Gun;
    public float gunTime = 0.3f;

    [Header("Whip")]
    public bool hasWhip = false;
    public GameObject Whip;
    public float whipTime = 0.3f;


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

        selectedAttack = 0;
        attacking = false;
        defending = false;

        Kick.SetActive(false);
        Punch.SetActive(false);
        Whip.SetActive(false);

        life = lifeMax;
        damaged = false;
    }

    // Update is called once per frame
    void FixedUpdate() {

        

        if (move.x > 0 && !facingRight)
            Flip();
        else if (move.x < 0 && facingRight)
            Flip();

        if (attacking) {
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

        bool movement = Input.GetButton("Horizontal") || Input.GetButton("Vertical");
        anim.SetBool("Moving", movement);

        lifeText01.text = "Vie: " + life + "%";
        lifeText02.text = "Vie: " + life + "%";

        if (Input.GetButtonDown("Attack") && !attacking) {
            if(selectedAttack == 0)
                StartCoroutine(Kicking());
            if (selectedAttack == 1)
                StartCoroutine(Punching());
            if (selectedAttack == 2)
                StartCoroutine(Shoot());
            if (selectedAttack == 3)
                StartCoroutine(Whipping());
        }

        if (Input.GetButtonDown("Defend") && !attacking) {
            StartCoroutine(Defending());
        }

        if (Input.GetButtonDown("Switch") && !attacking) {
            selectedAttack ++;

            if (selectedAttack == 1 && !hasPunch)
                selectedAttack = 2;
            if (selectedAttack == 2 && !hasGun)
                selectedAttack = 3;
            if (selectedAttack == 3 && !hasWhip)
                selectedAttack = 0;

            if (selectedAttack > maxAttack)
                selectedAttack = 0;

            Debug.Log(selectedAttack);
        }
    }

    IEnumerator Kicking() {

        anim.SetTrigger("Kick");

        attacking = true;
        yield return new WaitForSeconds(0.1f);
        Kick.SetActive(true);
        yield return new WaitForSeconds(kickTime);
        Kick.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        attacking = false;
    }

    IEnumerator Punching() {

        anim.SetTrigger("Punch");

        attacking = true;
        yield return new WaitForSeconds(0.1f);
        Punch.SetActive(true);
        yield return new WaitForSeconds(punchTime);
        Punch.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        attacking = false;
    }

    IEnumerator Shoot() {

        anim.SetTrigger("Shoot");

        attacking = true;
        yield return new WaitForSeconds(0.1f);
        Instantiate(Gun,transform.TransformPoint(Vector2.right * 0.1f), transform.rotation);
        yield return new WaitForSeconds(gunTime);
        attacking = false;
    }

    IEnumerator Whipping () {
        attacking = true;
        yield return new WaitForSeconds(0.1f);
        Whip.SetActive(true);
        yield return new WaitForSeconds(whipTime);
        Whip.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        attacking = false;
    }

    IEnumerator Defending() {

        anim.SetTrigger("Defend");

        attacking = true;
        yield return new WaitForSeconds(0.1f);
        defending = true;
        yield return new WaitForSeconds(defendTime);
        defending = false;
        yield return new WaitForSeconds(0.2f);
        attacking = false;
        
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
        anim.SetTrigger("Heart");
        SpriteRenderer spriteRend = sprite.GetComponent<SpriteRenderer>();

        spriteRend.enabled = false;
        yield return new WaitForSeconds(0.1f);
        spriteRend.enabled = true;
        yield return new WaitForSeconds(0.1f);
        spriteRend.enabled = false;
        yield return new WaitForSeconds(0.1f);
        spriteRend.enabled = true;
        yield return new WaitForSeconds(0.1f);
        spriteRend.enabled = false;
        yield return new WaitForSeconds(0.1f);
        spriteRend.enabled = true;
        yield return new WaitForSeconds(0.1f);
        spriteRend.enabled = false;
        yield return new WaitForSeconds(0.1f);
        spriteRend.enabled = true;
        yield return new WaitForSeconds(0.1f);
        spriteRend.enabled = false;
        yield return new WaitForSeconds(0.1f);
        spriteRend.enabled = true;
        yield return new WaitForSeconds(0.1f);
        spriteRend.enabled = false;
        yield return new WaitForSeconds(0.1f);
        spriteRend.enabled = true;
        damaged = false;
    }

    void OnTriggerEnter2D (Collider2D other) {
        if(other.gameObject.CompareTag("EnemyAttack") && !damaged) {

            if (defending) {
                return;
            }

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
