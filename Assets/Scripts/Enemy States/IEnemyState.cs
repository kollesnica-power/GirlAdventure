using UnityEngine;
using System.Collections;

public interface IEnemyState {

    void OnEnter(EnemyController enemy);

    void OnExecute();

    void OnExit();

    void OnTriggerEnter(Collider2D other);

}
