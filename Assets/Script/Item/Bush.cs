using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Bush : MonoBehaviour
{
    private SpriteRenderer sr;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("in");
        if (other.CompareTag("Player") && other.gameObject.GetComponent<PhotonView>().IsMine)
        {
            sr.color = new Color(1f, 1f, 1f, 0.1f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("out");
        if (other.CompareTag("Player") && other.gameObject.GetComponent<PhotonView>().IsMine)
        {
            sr.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
