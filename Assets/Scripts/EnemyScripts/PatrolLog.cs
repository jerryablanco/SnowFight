using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log
{
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;

    private void FixedUpdate()
    {
        CheckDistance();
    }

    protected override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                && Vector3.Distance(target.position, transform.position) > attackRadius
                && (currentState == EnemyState.WALKING || currentState == EnemyState.IDLE)) {
            Vector3 movePosition = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            ChangeAnimator(movePosition - transform.position);
            myRigidbody.MovePosition(movePosition);
            animator.SetBool("wakeUp", true);
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius) {

            if (Vector3.Distance(transform.position, currentGoal.position) > roundingDistance) {
                Vector3 movePosition = Vector3.MoveTowards(transform.position, currentGoal.position, moveSpeed * Time.deltaTime);

                ChangeAnimator(movePosition - transform.position);
                myRigidbody.MovePosition(movePosition);
            }
            else {
                ChangeGoal();
            }
        }
    }

    private void ChangeGoal()
    {
        if(currentPoint == path.Length - 1) {
            currentPoint = 0;
            currentGoal = path[0];
        }
        else {
            currentPoint++;
            currentGoal = path[currentPoint];
        }
    }
}
