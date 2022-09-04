using Photon.Pun;
using UnityEngine;
using Cinemachine;
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab = null;
    [SerializeField]
    private CinemachineVirtualCamera playerCamera = null;
    [SerializeField]
    private SpawnPoints spawnPoints = null;
    [SerializeField]
    private FloatValue currentPlayerNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        
       var player = PhotonNetwork.Instantiate(playerPrefab.name, GetPlayerSpawnPoint(), Quaternion.identity);
        
        playerCamera.Follow = player.transform;
        playerCamera.LookAt = player.transform;
    }

    [PunRPC]
    //A little hacky but just wanted to get somethign down. Probably better to attach a spawn point to a player on entering lobby.
    private Vector3 GetPlayerSpawnPoint()
    {

        //This didn't work, trying just random for now.
        //currentPlayerNumber.runTimeValue++;
        //if (currentPlayerNumber.runTimeValue > 3)
        //{
        //    currentPlayerNumber.runTimeValue = currentPlayerNumber.initialValue;
        //}
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, 3);
        var spawnlocation = spawnPoints.locations[randomNumber];//(int)currentPlayerNumber.runTimeValue //didnt work

        if (spawnlocation != null)
        {
            Debug.Log($"Found spawn location for player:{currentPlayerNumber.runTimeValue} - location { spawnlocation}");
            return new Vector3 { x = spawnlocation.x, y = spawnlocation.y, z = 0 };
        }
        //else
        //{
        //    Debug.Log($"didn't find spawn location for player:{currentPlayerNumber.runTimeValue}");
        //}

        return Vector3.zero;
    }
}
