using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : Projectile
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        //Maybe
        Invoke("Destroy", lifetime);
    }

    private void OnDestroy()
    {
        //Put back into pool.
        gameObject.SetActive(false);
        //Debug.Log("Snowball Got Destroyed");
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void Setup(Vector2 velocity, Vector3 direction)
    {
        myRigidBody.velocity = velocity.normalized * moveSpeed;
        transform.rotation = Quaternion.Euler(direction);
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("Player")){
            Destroy(this.gameObject);
        }
    }
}
