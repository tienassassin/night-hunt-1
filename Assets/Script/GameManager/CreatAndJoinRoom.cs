using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Hastable = ExitGames.Client.Photon.Hashtable;

public class CreatAndJoinRoom : MonoBehaviourPunCallbacks
{
    public InputField username;

    public InputField create;
    public InputField join;

    public Text statusText;
    
    // Start is called before the first frame update
    void Start()
    {
        username.text = PlayerPrefs.GetString("username");

        statusText.text = "Status: OK";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LeaveLobby()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("TitleScreen");
    }

    public void CreateRoom()
    {
        
        //setting properties
        Hastable roomProperties = new Hastable();
        roomProperties.Add("readyPlayer", 0);
        roomProperties.Add("alivePlayer", 0);

        Photon.Realtime.RoomOptions roomOpt =
            new Photon.Realtime.RoomOptions()
            {
                IsVisible = true,
                IsOpen = true,
                MaxPlayers = 10,
                CustomRoomProperties = roomProperties,
                CustomRoomPropertiesForLobby = new []{"readyPlayer","alivePlayer"}
            };
        
        //create room
        PhotonNetwork.CreateRoom(create.text, roomOpt);
        

        //PhotonNetwork.CreateRoom(create.text);
        
        PlayerPrefs.SetString("username", username.text);
        PhotonNetwork.NickName = username.text;
    }

    public void JoinRoom()
    {
        //join room
        PhotonNetwork.JoinRoom(join.text);

        PlayerPrefs.SetString("username", username.text);
        PhotonNetwork.NickName = username.text;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        statusText.text = "Status: join failed\n" + message;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        statusText.text = "Status: create failed:\n" + message;
    }

    public override void OnJoinedRoom()
    {
        statusText.text = "Status: Success";
        
        //PhotonNetwork.LoadLevel("MultiPlayer");
        PhotonNetwork.LoadLevel("WaitingRoom");

        PhotonNetwork.CurrentRoom.SetCustomProperties(
            new Hastable() 
            {
                { "alivePlayer", PhotonNetwork.CurrentRoom.PlayerCount } 
            });
    }

    
}
