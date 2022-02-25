using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DisableAttribute : MonoBehaviour
{
    PhotonView view;

    public Behaviour[] componentsToDisable;
    public GameObject[] objectsToDisable;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();

        if (!view.IsMine)
        {
            for (int i=0; i<componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }

            for (int i = 0; i < objectsToDisable.Length; i++)
            {
                objectsToDisable[i].SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
