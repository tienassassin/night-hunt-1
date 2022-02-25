using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ShowPing : MonoBehaviour
{
    public Text pingText;

    float delay = 1f;

    void Update()
    {
        delay -= Time.deltaTime;

        if (delay <= 0)
        {
            pingText.text = PhotonNetwork.GetPing().ToString() + " ms";
            delay = 1f;
        }
    }
}
