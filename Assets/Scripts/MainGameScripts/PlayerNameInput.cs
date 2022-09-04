using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class PlayerNameInput : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField nameInputField = null;
    [SerializeField]
    private Button continueButton = null;

    private const string PlayerPrefsNameKey = "PlayerName";

    private void Start() => SetUpInputField();

    private void SetUpInputField()
    {
        continueButton.interactable = false;
        if (!PlayerPrefs.HasKey(PlayerPrefsNameKey)) { return; }

        string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);
        nameInputField.text = defaultName;

        SetPlayerName();
    }

    public void SetPlayerName()
    {
        string name = nameInputField.text;
        continueButton.interactable = !string.IsNullOrWhiteSpace(name);
    }

    public void SavePlayerName()
    {
        string playerName = nameInputField.text;
        PhotonNetwork.NickName = playerName;
        PlayerPrefs.SetString(PlayerPrefsNameKey, playerName);
    }
}
