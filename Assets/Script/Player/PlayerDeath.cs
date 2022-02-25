using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerDeath : MonoBehaviour
{
    public Behaviour[] disabledComponents;
    public GameObject[] disabledGameobjects;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActiveDisabling()
    {
        foreach (var t in disabledComponents)
        {
            t.enabled = false;
        }

        foreach (var t in disabledGameobjects)
        {
            t.SetActive(false);
        }
    }
}
