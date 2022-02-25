using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Roof : MonoBehaviour
{
    public GameObject[] roofImages;
    public GameObject[] dirGuide;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var t in roofImages)
        {
            t.SetActive(true);
        }

        foreach (var d in dirGuide)
        {
            d.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && col.gameObject.GetComponent<PhotonView>().IsMine)
        {
            foreach (var t in roofImages)
            {
                t.SetActive(false);
            }
            
            foreach (var d in dirGuide)
            {
                d.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.gameObject.GetComponent<PhotonView>().IsMine)
        {
            foreach (var t in roofImages)
            {
                t.SetActive(true);
            }
            
            foreach (var d in dirGuide)
            {
                d.SetActive(false);
            }
        }
    }
}
