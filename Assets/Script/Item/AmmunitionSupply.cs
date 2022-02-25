using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionSupply : MonoBehaviourPun
{
    public string ammuType;
    public int quantity;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.tag = "Destroyed";
            PlayerPower pw = collision.gameObject.GetComponent<PlayerPower>();

            if (ammuType == "medicine")
                pw.medicine = Mathf.Min(pw.medicine + quantity, 5);

            if (ammuType == "bullet")
                pw.bullet = Mathf.Min(pw.bullet + quantity, 100);

            if (ammuType == "slow")
                pw.slowBomb = Mathf.Min(pw.slowBomb + quantity, 10);

            if (ammuType == "blind")
                pw.blindBomb = Mathf.Min(pw.blindBomb + quantity, 10);

            if (ammuType == "stun")
                pw.stunBomb = Mathf.Min(pw.stunBomb + quantity, 10);

            collision.gameObject.GetComponent<PlayerMovement>().Loot();
        }
    }
}
