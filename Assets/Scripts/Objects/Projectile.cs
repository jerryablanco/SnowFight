using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public Vector2 directionToMove;
    [Header("Lifetime")]
    public float lifetime = 2f;
    private float lifetimeSeconds;
    public Rigidbody2D myRigidBody;

    private void Awake()
    {
        lifetimeSeconds = lifetime; 
    }
    // Start is called before the first frame update
    void Start()
    {
        lifetimeSeconds = lifetime;
        myRigidBody = GetComponent<Rigidbody2D>();
        //Debug.Log("projectile lifetime = " + lifetime.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("projectile lifetime in update methode before math = " + lifetime.ToString());
        lifetimeSeconds =  lifetimeSeconds - Time.deltaTime;
        //Debug.Log("time.deltatime  = " + Time.deltaTime.ToString() + " || and the lifetimeseconds AFTER Math" + lifetimeSeconds.ToString()      );
        if (lifetimeSeconds <= 0) {
            //Debug.Log("I am about to destroy this thing cause it's more than 10 seconds old.");
            Destroy(this.gameObject);
            //this.gameObject.SetActive(false);  //TODO: Get from object pool,. set inactive.
        }
    }

    public void Launch(Vector2 initalVelocity)
    {
        myRigidBody.velocity = initalVelocity * moveSpeed;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(this.gameObject);
        //this.gameObject.SetActive(false); //TODO: Get from object pool,. set inactive.
    }
}
