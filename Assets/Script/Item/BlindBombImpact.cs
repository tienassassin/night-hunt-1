using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindBombImpact : MonoBehaviour
{
    public GameObject ImpactPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 20));
    }

    private void OnDestroy()
    {
        GameObject impact = Instantiate(ImpactPrefab, transform.position, Quaternion.identity);
        Destroy(impact, 0.33f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerPower>().TakeBlind(2f);
            Debug.Log("blind");
        }
    }
}
