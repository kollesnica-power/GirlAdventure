using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBehaviour : StateMachineBehaviour {

    private BoxCollider2D slideCollider;
    private BoxCollider2D collider;

    private Vector2 slideOffset = new Vector2(-0.1730056f, -0.862344f);
    private Vector2 slideSize = new Vector2(0.5257963f, 0.1773f);
    private Vector2 offset;
    private Vector2 size;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (collider == null) {
            collider = PlayerController.Instance.GetComponent<BoxCollider2D>();
            offset = collider.offset;
            size = collider.size;
        }

        if (slideCollider == null) {
            BoxCollider2D[] allChildren = PlayerController.Instance.GetComponentsInChildren<BoxCollider2D>();

            foreach (BoxCollider2D child in allChildren) {
                if (child.name == "SlideCollider") {
                    slideCollider = child;
                }
            }

        }

        slideCollider.enabled = true;
        collider.offset = slideOffset;
        collider.size = slideSize;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        slideCollider.enabled = false;
        collider.offset = offset;
        collider.size = size;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
