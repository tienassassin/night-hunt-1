using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSupply : MonoBehaviour
{
    public float time;
    
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
        Destroy(gameObject);

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerPower>().GetArmor(time);
        }
    }
}
