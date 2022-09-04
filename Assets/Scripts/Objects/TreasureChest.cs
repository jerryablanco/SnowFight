using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TreasureChest : Interactable
{

    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public Signal raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange) {
            if (!isOpen){
                OpenChest();
            }
            else {
                EmptyChest();
                //Chest is open
                //dialogBox.SetActive(true);
                //dialogText.text = dialog;
                dialogBox.SetActive(false);
            }
        }
    }

    //private IEnumerator OpenCoRoutine()
    //{
        
    //    yield return new WaitForSeconds(.6f);
    //    animator.SetBool("openChest", false);
    //}

    public void OpenChest()
    {
        dialogBox.SetActive(true);
        //StartCoroutine(OpenCoRoutine());
        dialogText.text = contents.itemDescription;
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        animator.SetBool("openChest", true);
        raiseItem.Raise();
        context.Raise();
        isOpen = true;
        
    }

    public void EmptyChest()
    {
        dialogBox.SetActive(false);
        raiseItem.Raise();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen) {
            playerInRange = true;
            context.Raise();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen) {
            playerInRange = false;
            context.Raise();
        }
    }
}
