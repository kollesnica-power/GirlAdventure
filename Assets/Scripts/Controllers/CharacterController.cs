using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class CharacterController: MonoBehaviour {

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    public EdgeCollider2D attackCollider;
    [SerializeField]
    public List<string> damageSources;
    [SerializeField]
    protected Stat healthStat;

    protected Rigidbody2D rigidbody2d;
    protected bool isFacingRight;
    public Animator animator { get; private set; }
    public bool isDead {
        get {
            return healthStat.CurrentValue <= 0;
        }
        set {}
    }
    public bool isDamage { get; set; }

    public float MoveSpeed {
        get {
            return moveSpeed;
        }

        set {
            moveSpeed = value;
        }
    }

    // deathtimer
    private float deathTimer = 0.0f;
    private float deathCooldown = 5.0f;

    // Use this for initialization
    public virtual void Start () {

        isFacingRight = true;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthStat.Init();

    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void Flip() {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Attack() {
        attackCollider.enabled = true;

        // make collider move to prevent damage when the player stand still
        attackCollider.transform.Translate(new Vector3(0.01f, 0, 0));
        attackCollider.transform.Translate(new Vector3(-0.01f, 0, 0));
    }

    protected void Respawn() {

        deathTimer += Time.deltaTime;

        if (deathTimer >= deathCooldown) {

            deathTimer = 0.0f;

            healthStat.CurrentValue = 3;

            animator.Play("Idle");

        }

        if (!isDead) {
            Revive();
        }

    }

    protected abstract void Revive();

    public virtual void OnTriggerEnter2D(Collider2D other) {

        if (damageSources.Contains(other.tag)) {
            Debug.Log(this.GetType().Name + " was hited by " + other.tag);
            if (!isDead) {
                StartCoroutine(TakeDamage()); 
            }
        }

    }

    public abstract IEnumerator TakeDamage();

}
