using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerNameTags : MonoBehaviourPun
{
    [SerializeField]
    private TextMeshProUGUI nameText;

    // Start is called before the first frame update
    void Start()
    {
    //    if (!photonView.IsMine)
    //    {
    //        SetName();
    //    }
    }

    private void SetName()
    {
        nameText.text = photonView.Owner.NickName;
    }
}
