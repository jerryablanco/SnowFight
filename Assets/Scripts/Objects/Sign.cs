using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange) {
            if (dialogBox.activeInHierarchy) {
                dialogBox.SetActive(false);
            }
            else {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger) {
            playerInRange = true;
            Debug.Log("InContext");
            context.Raise();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger) {
            playerInRange = false;
            dialogBox.SetActive(false);
            context.Raise();
        }

    }
}
