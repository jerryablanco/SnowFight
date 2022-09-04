using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviourPun
{
    public enum PlayerState
    {
        WALKING,
        ATTACKING,
        INTERACTING,
        STAGGER
    }

    public PlayerState currentState;
    public float moveSpeed;
    public float jumpHeight; //Don't need following tutorial
    public bool onGround = false;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public VectorValue startingPosition;
    public Inventory playerInventory;
    public SpriteRenderer recievedItemSprite;
    public Signal playerHit;
    public GameObject projectile;
    public GameObject endMenu;
    public float fireSpeed = .3f;
    [SerializeField]
    private Transform snowBallSpawnPoint;
    private Rigidbody2D playerRigidbody;
    private Vector3 change;
    private Animator animator;

    //public AudioSource audioData; //Took out audio for now

    private readonly string _horizontal = "Horizontal";
    private readonly string _vertical = "Vertical";

    // Start is called before the first frame update
    void Start()
    {
        //endMenu.SetActive(false);
        currentHealth.runTimeValue = currentHealth.initialValue;
        playerInventory.coins = 0;
        currentState = PlayerState.WALKING;
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {   
        HandleMovement();
        //Still want to animate all the chars.
    }

    private void HandleMovement()
    {
        //If Powerup Lock interacting (prob have to remove)
        //if (currentState == PlayerState.INTERACTING)
        //{
        //    return;
        //}

        change = Vector3.zero;
        //Get x & y //GetAxisRaw goes to max value without interpolation.... 
        change.x = Input.GetAxisRaw(_horizontal);
        change.y = Input.GetAxisRaw(_vertical);
        //only move us if it is us.
        if (photonView.IsMine) {
            if (Input.GetButtonDown("attack") && (currentState != PlayerState.ATTACKING && currentState != PlayerState.STAGGER)) {
                StartCoroutine(AttackCoRoutine());
            }
            else if (currentState == PlayerState.WALKING) {
                MoveCharacter();
            }

        }
        
        if (currentState == PlayerState.WALKING)
        {
            UpdateAnimation();
        }
    }

    private void MoveCharacter()
    {
        if (change != Vector3.zero) {
            change.Normalize();
            playerRigidbody.MovePosition(transform.position + change * moveSpeed * Time.deltaTime);
        }
    }

    //private void UpdateAnimationAndMove()
    //{
    //    if (change != Vector3.zero) {
    //        MoveCharacter();
    //        animator.SetFloat("moveX", change.x);
    //        animator.SetFloat("moveY", change.y);
    //        animator.SetBool("moving", true);
    //    }
    //    else {
    //        animator.SetBool("moving", false);
    //    }
    //}

    private void UpdateAnimation()
    {
        if (change != Vector3.zero) {
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else {
            animator.SetBool("moving", false);
        }
    }


    private IEnumerator AttackCoRoutine()
    {
        currentState = PlayerState.ATTACKING;
        yield return null;
        MakeSnowball();
        yield return new WaitForSeconds(fireSpeed);
        if (currentState != PlayerState.INTERACTING) {
            currentState = PlayerState.WALKING;
        }
    }

    //ToDo - refactor to use Snowball pool like enemies do.
    private void MakeSnowball()
    {
        Vector2 playdirection = new Vector2(animator.GetFloat("moveX"),  animator.GetFloat("moveY"));
        Snowball snowball = PhotonNetwork.Instantiate(projectile.name, (transform.position + new Vector3(playdirection.x, playdirection.y)), Quaternion.identity).GetComponent<Snowball>();
        snowball.Setup(playdirection, ChooseSnowballDirection());
    }
   
    private Vector3 ChooseSnowballDirection()
    {
        float direction = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, direction).normalized;
    }

    private IEnumerator SecondAttackCoRoutine()
    {
        currentState = PlayerState.ATTACKING;
        animator.SetBool("attacking", true);
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.INTERACTING) {
            currentState = PlayerState.WALKING;
        }
    }

    public void RaiseItem()
    {
        if (playerInventory.currentItem != null) {
            if (currentState != PlayerState.INTERACTING) {
                animator.SetBool("getItem", true);
                currentState = PlayerState.INTERACTING;
                recievedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else {
                animator.SetBool("getItem", false);
                currentState = PlayerState.WALKING;
                recievedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }


    public void Knock(float knockTime, float damage)
    {
        if (photonView.IsMine)
        {
            currentHealth.runTimeValue -= damage;
            playerHealthSignal.Raise();
            if (currentHealth.runTimeValue > 0)
            {
                StartCoroutine(KnockCo(knockTime, damage));
            }
            else
            {
                //TODO:  Player Dies Should raise a signal and something else show the mainmenu buttons, 
                //Not working here.
                //Deathsound
                //audioData.Play();
                //endMenu.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator KnockCo(float knockTime, float damage)
    {
        playerHit.Raise();
        if (playerRigidbody != null) {
            yield return new WaitForSeconds(knockTime);
            playerRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.WALKING;
            playerRigidbody.velocity = Vector2.zero;
        }
    }
}
