using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : PowerUp
{
    public FloatValue playerHealth;
    public float amountToIncrease;
    public FloatValue heartContainers;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger) {
            playerHealth.runTimeValue += amountToIncrease;
            if(playerHealth.runTimeValue > heartContainers.runTimeValue * 2f) {
                playerHealth.runTimeValue = heartContainers.runTimeValue * 2f;
            }
            powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
