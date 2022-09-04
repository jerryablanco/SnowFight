using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DoorType
{
    Key,
    Enemey,
    Button
}

public class Door : Interactable
{
    [Header("Door Variables")]
    public DoorType thisDoorType;
    public bool open = false;

    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (playerInRange && thisDoorType == DoorType.Key) {
                //does player have key//
                if(playerInventory.numberOfKeys > 0) {
                    playerInventory.numberOfKeys--;
                    Open();
                }

            }
        }
    }

    public void Open()
    {
        //turn off door sprite render
        doorSprite.enabled = false;
        open = true;
        physicsCollider.enabled = false;
        //turn off door box collider
    }

    public void Close()
    {

    }
}
