using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SnowballPool : MonoBehaviour
{
    public static SnowballPool snowballPoolInstance;

    [SerializeField]
    private GameObject pooledSnowball;
    private bool notEnoughSnowballsInPool = true;

    private List<GameObject> snowballs;

    private void Awake()
    {
        snowballPoolInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        snowballs = new List<GameObject>();
    }

    public GameObject GetSnowball()
    {
        if (snowballs.Count > 0) {
            for (int i = 0; i < snowballs.Count; i++) {
                if (!snowballs[i].activeInHierarchy) {
                    return snowballs[i];
                }
            }
            notEnoughSnowballsInPool = true;
        }

        if (notEnoughSnowballsInPool) {
            GameObject snowball = PhotonNetwork.Instantiate(pooledSnowball.name, Vector3.zero, Quaternion.identity);
            snowball.SetActive(false);
            snowballs.Add(snowball);
            return snowball;
        }

        return null;
    }
}
