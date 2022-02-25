using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    
    private float moveSpeed;

    private Rigidbody2D rb;

    private Vector2 movement;
    private Vector2 mousePos;

    private PhotonView view;

    PlayerPower pw;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();

        pw = GetComponent<PlayerPower>();
    }

    // Update is called once per frame
    void Update()
    {

        moveSpeed = pw.moveSpeed;

        if (view.IsMine)
        {
            //calc move vector
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Camera.main.GetComponent<CameraFollowPlayer>().setTarget(gameObject.transform);
        }
       
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {           
            //apply move vector
            rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);

            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            rb.rotation = angle;

        }
        
    }

    public void Loot()
    {
        view.RPC("DestroySupply", RpcTarget.All);
    }

    [PunRPC]
    void DestroySupply()
    {
        GameObject[] needToDestroy = GameObject.FindGameObjectsWithTag("Destroyed");
        for (int i=0; i<needToDestroy.Length; i++)
        {
            Destroy(needToDestroy[i]);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Virus") && pw.enabled)
        {
            pw.isBleeding = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Virus") && pw.enabled)
        {
            pw.isBleeding = true;
        }
    }
}
