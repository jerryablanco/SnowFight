using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum EnemyState
{
    IDLE,
    WALKING,
    ATTACKING,
    STAGGER
}

public class Enemy : MonoBehaviour
{
    [Header("State Machine")]
    public EnemyState currentState;
    public Inventory playerInventory;

    [Header("Enemy Stats")]
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public Vector2 homePosition;
    //public SpawnPoints spawnPoints;

    [Header("Death Effects")]
    public GameObject deathEffect;
    private float deathEffectDelay = 1f;
    private float respawnTimer = 2f;
    public Signal powerUpSignal;


    private void Awake()
    {
        health = maxHealth.initialValue;
    }

    public void Knock(Rigidbody2D myRigidBody, float knockTime, float damage)
    {
        if (this.gameObject.activeSelf) {
            StartCoroutine(KnockCo(myRigidBody, knockTime));
            TakeDamage(damage);
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && this.gameObject.activeSelf) {
            DeathEffect();
            playerInventory.coins += 1;
            powerUpSignal.Raise();
            this.gameObject.SetActive(false);
        }
    }

    private void DeathEffect()
    {
        if(deathEffect != null ) {
            GameObject effect = PhotonNetwork.Instantiate(deathEffect.name, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
        }
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidBody, float knockTime)
    {
        if (myRigidBody != null) {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = EnemyState.IDLE;
            myRigidBody.velocity = Vector2.zero;
        }
    }

}
