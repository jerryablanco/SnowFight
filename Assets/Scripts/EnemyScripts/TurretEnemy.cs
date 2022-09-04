using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TurretEnemy : Log
{
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;

    public void Update()
    {
        fireDelaySeconds -= Time.deltaTime;

        if(fireDelaySeconds <= 0) {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }


    protected override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                && Vector3.Distance(target.position, transform.position) > attackRadius
                && (currentState == EnemyState.WALKING || currentState == EnemyState.IDLE)) {
            Vector3 movePosition = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            
            //TODO: Get projectile from the object pool.
            if (canFire) {
                Vector3 distanceBetween = target.transform.position - transform.position;
                canFire = false;
                GameObject currentProjectile = PhotonNetwork.Instantiate(projectile.name, transform.position, Quaternion.identity);
                currentProjectile.GetComponent<Projectile>().Launch(distanceBetween);
            }

            ChangeState(EnemyState.WALKING);
            animator.SetBool("wakeUp", true);
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius) {
            animator.SetBool("wakeUp", false);
        }
    }

}
