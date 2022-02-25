using System;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Hastable = ExitGames.Client.Photon.Hashtable;

public class Player : MonoBehaviourPunCallbacks
{
    public Text username;

    public Text alivePlayer;

    public bool endGame = false;

    public GameObject winNotif;
    public GameObject loseNotif;


    public Slider music;
    public Slider sfx;
    
    // Start is called before the first frame update
    void Start()
    {
        username.text = photonView.Owner.NickName;
        //Debug.Log("Hello " + username.text);

        winNotif.SetActive(false);
        loseNotif.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PhotonView>().IsMine &&Input.GetKeyDown(KeyCode.Backspace) && PhotonNetwork.InRoom)
        {
            if (GetComponent<PlayerPower>().alive && !endGame)
                GetComponent<PlayerPower>().Die();

            StartCoroutine(EscapeTown(1));
        }

        alivePlayer.text = "Alive: " + 
            PhotonNetwork.CurrentRoom.CustomProperties["alivePlayer"];

        
        if (GetComponent<PhotonView>().IsMine && loseNotif.activeSelf && Input.GetKeyDown(KeyCode.H))
        {
            loseNotif.SetActive(false);
            //GameObject.Find("Global Light 2D");
        }
        
        
        PlayerPrefs.SetFloat("music",music.value);
        PlayerPrefs.SetFloat("sfx",sfx.value);
    }

    private void LateUpdate()
    {
        if (alivePlayer.text == "Alive: 1" && !endGame)
        {
            endGame = true;
            //Debug.Log("You win!");
            if (GetComponent<PlayerPower>().alive)
            {
                winNotif.SetActive(true);
            }
        }
    }

    public void LoseGame()
    {
        StartCoroutine(LoseNotifAppear(1f));
    }

    IEnumerator LoseNotifAppear(float time)
    {
        yield return new WaitForSeconds(time);

        loseNotif.SetActive(true);
    }

    IEnumerator EscapeTown(float time)
    {
        yield return new WaitForSeconds(time);

        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel("LoadingServer");
        
        //PhotonNetwork.LeaveRoom();
        //PhotonNetwork.LoadLevel("Lobby");
    }

}
