using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Enemy[] enemies;
    public Pot[] pots;// will get rid of.
    public float spawnTimer = 2f;
    public SpawnPoints spawnPoints;

    //Went with a different approach and didn't use this
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger) 
        {
            //activate
            ChangeAllEnemyActivationsInRoom(true);
            for (int i = 0; i < enemies.Length; i++) {
                enemies[i].homePosition = GetNewSpawnPoint();
                ChangeActivation(enemies[i], true);
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger) {
            //deactivate
            ChangeAllEnemyActivationsInRoom(false);
        }
    }

    void ChangeActivation(Component component, bool activation)
    {
        component.gameObject.SetActive(activation);
    }

    private void ChangeAllEnemyActivationsInRoom(bool active)
    {
        for (int i = 0; i < enemies.Length; i++) {
            if (active) {
                enemies[i].homePosition = GetNewSpawnPoint();
            }
            ChangeActivation(enemies[i], true);
        }
    }

    private Vector2 GetNewSpawnPoint()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, 9);
        return spawnPoints.locations[randomNumber];
    }
}
