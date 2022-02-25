using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Hastable = ExitGames.Client.Photon.Hashtable;

public class WaitingRoomManager : MonoBehaviourPunCallbacks
{
    bool readyPressed = false;

    public Text roomId;
    public Text Notif;

    public Text rate;

    public Text readyButton;
    
    // Start is called before the first frame update
    void Start()
    {
        roomId.text = "Room ID: " + PhotonNetwork.CurrentRoom.Name;
        
        readyButton.text = "I'M READY!!!";
        readyButton.fontSize = 30;

        Notif.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        SetPlayerRateText();
    }

    public void StartButton()
    {
        if (!readyPressed)
        {
            int readyPlayer = (int)PhotonNetwork.CurrentRoom.CustomProperties["readyPlayer"];
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hastable() { { "readyPlayer", readyPlayer + 1 } });

            readyPressed = true;

            readyButton.text = "WAITING FOR\nOTHER PLAYERS...";
            readyButton.fontSize = 20;

            Notif.text = "How brave you are!";
        }
 
    }

    public void BackButton()
    {
        //PhotonNetwork.LeaveRoom();
        //PhotonNetwork.LoadLevel("Lobby");

        if ((int) PhotonNetwork.CurrentRoom.CustomProperties["readyPlayer"] > 0)
        {
            Notif.text = "Someone is ready,\nyou want to run away like a coward?";
            return;
        }
        
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel("LoadingServer");
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        if ((int)PhotonNetwork.CurrentRoom.CustomProperties["readyPlayer"]
            == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            PhotonNetwork.LoadLevel("Multiplayer");

            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }

    void SetPlayerRateText()
    {
        rate.text = PhotonNetwork.CurrentRoom.CustomProperties["readyPlayer"]
            + "/" + PhotonNetwork.CurrentRoom.PlayerCount;
    }
}
