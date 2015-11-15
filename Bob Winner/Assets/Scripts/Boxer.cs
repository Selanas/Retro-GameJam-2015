using UnityEngine;
using System.Collections;

public class Boxer : MonoBehaviour {

    public GameObject HurtSFX;

    public float attackDist = 0.5f;

    private GameObject target;
    private Vector3 initPos;

    public GameObject sprite;

    [Header("Life")]
    public int lifeMax = 100;
    private int life = 100;
    private bool damaged = false;

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

    [Header("Movement")]
    public float maxSpeed = 10f;
    private Vector2 move = Vector2.zero;

    [Header("Rotation")]
    bool facingRight = true;

    private Rigidbody2D body2D;
    private Animator anim;


    void Awake() {

        target = GameObject.FindGameObjectWithTag("Hero");

        initPos = transform.position;
        anim = sprite.GetComponent<Animator>();
        body2D = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void OnEnable() {
        transform.position = initPos;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        sprite.transform.position = transform.position + new Vector3(0, 0.985f,0);
        attacking = false;
        defending = false;

        Kick.SetActive(false);
        Punch.SetActive(false);


        life = lifeMax;
        damaged = false;
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (Manager.End) {
            anim.SetBool("Moving", false);
            body2D.velocity = Vector2.zero;
            return;
        }

        if (life <= 0) {
            sprite.transform.position = transform.position + Vector3.up * 0.25f ;
            body2D.velocity = Vector2.zero;
            return;
        }

        if (move.x < 0 && !facingRight)
            Flip();
        else if (move.x > 0 && facingRight)
            Flip();

        if (attacking) {
            body2D.velocity = Vector2.zero;
            return;
        }

        if (damaged)
            return;

        Debug.Log(move);

        if(Mathf.Abs(target.transform.position.x - transform.position.x) > attackDist && Mathf.Abs(target.transform.position.y - transform.position.y) > 0.05f) {

            Debug.Log("Distance: " + (target.transform.position.x - transform.position.x) + " , " + (target.transform.position.y - transform.position.y));

            move = target.transform.position - transform.position;
            move.Normalize();
            move.x *= maxSpeed;
            move.y *= maxSpeed * 0.8f;

            body2D.velocity = Vector2.Lerp(body2D.velocity, move, Time.deltaTime * 50);
        }
        if (Mathf.Abs(target.transform.position.x - transform.position.x) <= attackDist && Mathf.Abs(target.transform.position.y - transform.position.y) <= 0.1f) {
            body2D.velocity = Vector2.zero;

            int rand = Random.Range(0, 3);

            if (rand == 0) {
                StartCoroutine(Punching());
            }
            if (rand == 1) {
                StartCoroutine(Kicking());
            }
            if(rand == 2) {
                StartCoroutine(Defending());
            }
            
        }
    }

    void Update() {

        if (Manager.End)
            return;

        anim.SetBool("Dead", life <= 0);

        if (life <= 0)
            return;

        anim.SetBool("Moving", body2D.velocity != Vector2.zero);

        
    }

    IEnumerator Kicking() {
        attacking = true;
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("Kick");

        
        yield return new WaitForSeconds(0.1f);
        Kick.SetActive(true);
        yield return new WaitForSeconds(kickTime);
        Kick.SetActive(false);
        yield return new WaitForSeconds(1f);
        attacking = false;
    }

    IEnumerator Punching() {
        attacking = true;
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("Punch");

        
        yield return new WaitForSeconds(0.1f);
        Punch.SetActive(true);
        yield return new WaitForSeconds(punchTime);
        Punch.SetActive(false);
        yield return new WaitForSeconds(1f);
        attacking = false;
    }

    IEnumerator Defending() {
        attacking = true;
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("Defend");

        
        yield return new WaitForSeconds(0.1f);
        defending = true;
        yield return new WaitForSeconds(defendTime);
        defending = false;
        yield return new WaitForSeconds(1f);
        attacking = false;

    }

    void Flip() {
        facingRight = !facingRight;
        //Vector3 theScale = transform.localScale;
        transform.eulerAngles += Vector3.up * 180;
        //theScale.x *= -1;
        //transform.localScale = theScale;
    }

    IEnumerator TakeDamagePunch() {
        damaged = true;

        Vector2 repousse = new Vector2(Mathf.Sign(target.transform.position.x - transform.position.x) * maxSpeed * -1, 0);
        body2D.velocity = repousse;

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

        damaged = false;
    }

    IEnumerator TakeDamage() {
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

        damaged = false;
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (Manager.End)
            return;

        if (life <= 0)
            return;

        if (other.gameObject.CompareTag("Kick") && !damaged) {
            if (defending) {
                life -= 5;
                return;
            }
            Instantiate(HurtSFX, transform.position, Quaternion.identity);
            StartCoroutine(TakeDamage());
            life -= 20;
        }

        if (other.gameObject.CompareTag("Punch") && !damaged) {
            if (defending) {
                life -=10;
                return;
            }
            Instantiate(HurtSFX, transform.position, Quaternion.identity);
            StartCoroutine(TakeDamagePunch());
            life -= 30;
        }

        if (other.gameObject.CompareTag("Whip") && !damaged) {
            if (defending) {
                life -= 5;
                return;
            }
            Instantiate(HurtSFX, transform.position, Quaternion.identity);
            StartCoroutine(TakeDamage());
            life -= 10;
        }

        if (other.gameObject.CompareTag("Bullet") && !damaged) {
            if (defending) {
                life -= 30;
                return;
            }
            StartCoroutine(TakeDamage());
            life -= 50;
        }
    }

}
