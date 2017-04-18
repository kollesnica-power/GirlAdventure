using UnityEngine;
using System.Collections;

public class PatroolState: IEnemyState {

    private EnemyController enemy;
    private float patroolTimer;
    private float patroolDuration;

    public void OnEnter(EnemyController enemy) {

        this.enemy = enemy;
        patroolDuration = Random.Range(5.0f, 10.0f);

    }

    public void OnExecute() {

        Debug.Log("Patrool State");

        if (enemy.target != null) {
            enemy.ChangeState(new AttackState());
        }

        Patrool();

    }

    public void OnExit() {



    }

    public void OnTriggerEnter(Collider2D other) {

        if (other.tag == "Edge") {
            enemy.Flip();
            enemy.target = null;
        } else if (enemy.damageSources.Contains(other.tag)) {
            enemy.target = PlayerController.Instance.gameObject;
            enemy.ChangeState(new AttackState());
        }

    }

    void Patrool() {

        enemy.Move();

        patroolTimer += Time.deltaTime;

        if (patroolTimer >= patroolDuration) {
            enemy.ChangeState(new IdleState());
        }

    }

}
