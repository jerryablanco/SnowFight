using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject findOpponentPanel = null;
    [SerializeField]
    private GameObject waitingStatusPanel = null;
    [SerializeField]
    private TextMeshProUGUI waitingStatusText = null;

    private bool isConnecting = false;
    private const string GameVersion = "0.1";
    private const int MaxPlayersPerRoom = 2;
    private const string _roomName = "testRoom";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void FindOpponent()
    {
        isConnecting = true;

        findOpponentPanel.SetActive(false);
        waitingStatusPanel.SetActive(true);

        waitingStatusText.text = "Searching...";

        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("is connected to photon.");
            PhotonNetwork.JoinRoom(_roomName);
        }
        else
        {
            PhotonNetwork.GameVersion = GameVersion;
            Debug.Log("trying to connected to photon.");
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("should have connected.");
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected to master");

        if (isConnecting)
        {
            Debug.Log("joining test room");
            PhotonNetwork.JoinRoom(_roomName);
            Debug.Log("joined test  room");
        }
        else
        {
            Debug.Log("isn't trying to connect for some reason.");
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (waitingStatusPanel != null)
        {
            waitingStatusPanel.SetActive(false);
        }
        if (findOpponentPanel != null)
        {
            findOpponentPanel.SetActive(true);
        }
        Debug.Log($"Disconnected due to : {cause} ");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"No clients are waiting for an opponent, creating a new room : code{returnCode} msg{message}");
        PhotonNetwork.CreateRoom(_roomName, new RoomOptions { MaxPlayers = MaxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Client successfully joined room:{PhotonNetwork.CurrentRoom.Name}");
        
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerCount != MaxPlayersPerRoom)
        {
            waitingStatusText.text = $"Waiting For Opponent(s) {playerCount}/{MaxPlayersPerRoom}";
            Debug.Log($"Currently {playerCount} in room.");
        }
        else
        {
            Debug.Log("match reach to begin");
            waitingStatusText.text = "Opponent Found";
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Player entered room {newPlayer.NickName}");
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;

            Debug.Log("match reach to begin");
            waitingStatusText.text = "Opponent Found";
            PhotonNetwork.LoadLevel("Playground");
        }
        else
        {
            Debug.Log($"Currently have {PhotonNetwork.CurrentRoom.PlayerCount} players");
        }
    }

    public void PlayGame()
        {
        //Debug.Log("I clicked the restart");
        SceneManager.LoadScene("Playground");
        //Application.LoadLevel("Playground");
    }

    public void QuitGame()
    {
        //Debug.Log("I clicked the end game");
        Application.Quit();
    }
}
