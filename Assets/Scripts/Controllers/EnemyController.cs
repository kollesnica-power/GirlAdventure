using UnityEngine;
using System.Collections;

public class EnemyController : CharacterController {

    [SerializeField]
    private float attackRange;
    [SerializeField]
    private GameObject heartPrefab;
    [SerializeField]
    private GameObject patronPrefab;

    private IEnemyState currentState;
    public GameObject target { get; set; }
    public bool inRange {
        get {
            if (target != null ) {
                return Vector2.Distance(transform.position, target.transform.position) <= attackRange;
            }
            return false;
        }
    }

    // Use this for initialization
    public override void Start () {

        base.Start();

        isFacingRight = (transform.localScale.x == 1.0f);

        ChangeState(new IdleState());

	}
	
	// Update is called once per frame
	void Update () {

        if (!isDead) {

            currentState.OnExecute();

            LookAtTarget();

        } /*else {
            Respawn();
        }*/

	}

    void LookAtTarget() {

        if (target != null) {

            float xDist = transform.position.x - target.transform.position.x;
            
            if ((xDist > 0.5f && isFacingRight) || (xDist < -0.5f && !isFacingRight)) { // make character goes maximum on 0.5 from target
                Flip();
            } 

        }

    }

    public void ChangeState(IEnemyState newState) {

        if (currentState != null) {
            currentState.OnExit();
        }

        currentState = newState;
        currentState.OnEnter(this);

    }

    public void Move() {

        animator.SetFloat("Speed", 1.0f);

        transform.Translate(GetDirection() * MoveSpeed * Time.deltaTime, Space.World);

    }

    Vector2 GetDirection() {

        return isFacingRight ? Vector2.right : Vector2.left;
    }

    public override void OnTriggerEnter2D(Collider2D other) {

        base.OnTriggerEnter2D(other);

        if (other.tag == "Bullet") {
            Destroy(other.gameObject);
        }

        currentState.OnTriggerEnter(other);

    }

    public override IEnumerator TakeDamage() {

        healthStat.CurrentValue--;

        if (!isDead) {
            animator.SetTrigger("Damage");
        } else {
            animator.SetTrigger("Death");
            SpawnBonus();
            PlayerController.Instance.AddScore(100);
            yield return null;
        }

    }

    private void SpawnBonus() {

        GameObject bonus = Instantiate((Random.Range(0, 2) == 1 ? heartPrefab : patronPrefab), 
                                        transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(bonus.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        bonus.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 300.0f));

    }

    protected override void Revive() {

        ChangeState(new IdleState());

    }

}
