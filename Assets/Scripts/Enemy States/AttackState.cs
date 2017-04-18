using UnityEngine;
using System.Collections;

public class AttackState : IEnemyState {

    private float attackCooldown = 2.0f;
    private float attackTimer = 2.0f;
    private EnemyController enemy;

    public void OnEnter(EnemyController enemy) {

        this.enemy = enemy;
    }

    public void OnExecute() {

        Debug.Log("Attack State");

        if (enemy.target != null && !enemy.target.GetComponent<PlayerController>().isDead) {
            if (enemy.inRange) {
                Attack();
            } else{
                enemy.Move();
            }
        } else {
            enemy.target = null;
            enemy.ChangeState(new PatroolState());
        }

        attackTimer += Time.deltaTime;

    }

    public void OnExit() {



    }

    public void OnTriggerEnter(Collider2D other) {

        if (other.tag == "Edge") {
            enemy.Flip();
            enemy.target = null;
        }

    }

    void Attack() {

        enemy.animator.SetFloat("Speed", 0.0f);

        if (attackTimer >= attackCooldown) {

            attackTimer = 0.0f;

            enemy.animator.SetTrigger("Attack");

        }

    }

}
