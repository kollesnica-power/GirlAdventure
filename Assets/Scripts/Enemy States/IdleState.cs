using UnityEngine;
using System.Collections;

public class IdleState : IEnemyState {

    private EnemyController enemy;
    private float idleTimer;
    private float idleDuration;

    public void OnEnter(EnemyController enemy) {

        this.enemy = enemy;
        idleDuration = Random.Range(0.0f, 5.0f);

    }

    public void OnExecute() {

        Debug.Log("Idle State");

        if (enemy.target != null) {
            enemy.ChangeState(new PatroolState());
        }

        Idle();

    }

    public void OnExit() {



    }

    public void OnTriggerEnter(Collider2D other) {

        if (enemy.damageSources.Contains(other.tag)) {
            enemy.target = PlayerController.Instance.gameObject;
            enemy.ChangeState(new AttackState());
        }

        if (other.tag == "Edge") {
            enemy.Flip();
            enemy.target = null;
        }

    }

    void Idle() {

        enemy.animator.SetFloat("Speed", 0.0f);

        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration) {
            enemy.ChangeState(new PatroolState());
        }

    }

}
