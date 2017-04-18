using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : CharacterController {

    [SerializeField]
    private Stat patronStat;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private Transform bulletPos;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private BarController bar;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private Text scoreText;

    private bool isImmortal;
    private float immortalTime = 1.0f;
    private bool isGrounded;
    private bool isKnocked;
    private bool isFalling {
        get {
            return rigidbody2d.velocity.y < 0;
        }
    }

    //input
    private bool jump;
    private bool attack;
    private bool slide;
    private bool shoot;

    private static PlayerController instance;

    public static PlayerController Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<PlayerController>();
            }
            return instance;
        }

        set {
            instance = value;
        }
    }

    public override void Start(){

        base.Start();
        isGrounded = false;
        patronStat.Init();

    }

    void Update() {

        if (isDead) {
            Respawn();
        }

        if (!isDamage && !isDead) {
            HandleInput(); 
        }

    }	

	void FixedUpdate () {

        isGrounded = CheckGrounded();

        animator.SetInteger("Health", healthStat.CurrentValue);

        float move = 0.0f;

        if (!isDamage && !isDead) {
            move = Input.GetAxis("Horizontal"); 
        }

        HandleMovement(move);

        Flip(move);

        HandleAtacks();

        ClearInput();

        if (transform.position.y <= -5) {
            FallDeath();
        }

    }




/************************************************************FUNCTIONAL********************************************************************/

    void HandleInput() {

        if (Input.GetKeyDown(KeyCode.UpArrow) && !isFalling) {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.RightControl)) {
            attack = true;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            shoot = true;
        }

        if (Input.GetKeyDown(KeyCode.RightShift)) {
            slide = true;
        }

    }



    void HandleMovement(float move) {

        if (IsPlayerNotInAction() && !isKnocked) {

            rigidbody2d.velocity = new Vector2(move * MoveSpeed, rigidbody2d.velocity.y);

        }
        animator.SetFloat("Speed", Mathf.Abs(move));

        if (slide && Mathf.Abs(move) > 0.5f && IsPlayerNotInAction()) {
            animator.SetTrigger("Slide");
        }

        // for correct jumping on platform
        animator.SetFloat("VSpeed", rigidbody2d.velocity.y);
        if (isFalling) {
            animator.SetBool("Land", true);
            gameObject.layer = 11;
        }

        if (isGrounded) {

            animator.SetBool("Land", false);

            if (jump) {
                rigidbody2d.AddForce(new Vector2(0.0f, jumpForce));
                animator.SetTrigger("Jump");
            }
        }

    }



    void HandleAtacks() {

        if (attack && isGrounded) {
            animator.SetTrigger("Attack");
            rigidbody2d.velocity = Vector2.zero;
        }

        if (shoot && isGrounded && patronStat.CurrentValue > 0) {
            animator.SetTrigger("Shoot");
            rigidbody2d.velocity = Vector2.zero;
        }

    }



    public void Flip(float move) {

        if (((move > 0 && !isFacingRight) || (move < 0 && isFacingRight)) && IsPlayerNotInAction()) {
            Flip();
        }

    }

    private bool CheckGrounded() {

        if (rigidbody2d.velocity.y <= 0.0f) {

            foreach (Transform point in groundPoints) {

                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++) {

                    if (colliders[i].gameObject != gameObject) {
                        return true;
                    }

                }

            }

        }

        return false;

    }

    private IEnumerator Blinking() {
        while (isImmortal) {
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }



    public void Shoot() {

        if (isFacingRight) {
            (Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity)).GetComponent<Missle>().Init(Vector2.right);
        } else {
            (Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity)).GetComponent<Missle>().Init(Vector2.left);
        }

        patronStat.CurrentValue--;

    }

    public override IEnumerator TakeDamage() {

        if (!isImmortal) {

            healthStat.CurrentValue--;

            if (!isDead) {

                animator.SetTrigger("Damage");
                isImmortal = true;
                StartCoroutine(Blinking());

                yield return new WaitForSeconds(immortalTime);

                isImmortal = false;

            } else {
                StartCoroutine(Death());
            } 

        }

    }

    private IEnumerator Death() {
        //fade out the screen
        GameObject.Find("Fade").GetComponent<Fade>().FadeScreen(1);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(2);
    }

    protected override void Revive() {

        //fade in the screen
        GameObject.Find("Fade").GetComponent<Fade>().FadeScreen(-1);
        transform.position = new Vector3(1.56f, 5.98f, 0.0f);
        // facing character right
        Flip(1.0f);

    }

    private void FallDeath() {

        transform.position = new Vector3(1.56f, 2.3f, 0.0f);
        healthStat.CurrentValue = 0;
        StartCoroutine(Death());
        
    }

    public void AddScore(int scoreValue) {

        int currentValue = Int32.Parse(scoreText.text);

        currentValue += scoreValue;

        if (currentValue > 9999) {
            currentValue = 9999;
        }

        scoreText.text = currentValue.ToString();
        SceneProperties.score = currentValue;

    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Heart") {
            Destroy(collision.gameObject);
            healthStat.CurrentValue++;
        } else if (collision.gameObject.tag == "Patron") {
            Destroy(collision.gameObject);
            patronStat.CurrentValue++;
        }

        if (collision.gameObject.tag != "Moving Platform") {
            gameObject.layer = 0;
        }

        if (!collision.gameObject.CompareTag("Spikes")) {
            isKnocked = false;
        }

    }

    public override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);

        if (other.gameObject.tag == "Heart") {
            Destroy(other.gameObject);
            healthStat.CurrentValue++;
        } else if (other.gameObject.tag == "Patron") {
            Destroy(other.gameObject);
            patronStat.CurrentValue++;
        }

    }

    public void OnTriggerStay2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Spikes")) {

            if (!isImmortal && !isDead) {

                rigidbody2d.velocity = Vector2.zero;
                StartCoroutine(TakeDamage());
                isKnocked = true;
                rigidbody2d.AddForce(new Vector2((isFacingRight ? -6 : 6), 8), ForceMode2D.Impulse);
               
            }
        }

    }




    void ClearInput() {

        jump = false;
        attack = false;
        slide = false;
        shoot = false;

    }



    // is player in idle or run or jump state
    bool IsPlayerNotInAction() {

        return !animator.GetCurrentAnimatorStateInfo(0).IsName("Slide") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot");

    }

    
}
