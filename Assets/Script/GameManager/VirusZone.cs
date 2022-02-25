using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusZone : MonoBehaviour
{
    public Transform virusZoneMask;
    public CircleCollider2D virusZone;

    private float scaleDelay = 0.3f;
    private float currentScaleDelay;

    private float scaleAmount = 0.05f;
    
    // Start is called before the first frame update
    void Start()
    {
        currentScaleDelay = scaleDelay;
    }

    // Update is called once per frame
    void Update()
    {
        currentScaleDelay -= Time.deltaTime;
        if (currentScaleDelay <= 0)
        {
            ViruszoneScaleDown();
            currentScaleDelay = scaleDelay;
        }
    }

    void ViruszoneScaleDown()
    {
        if (virusZone.radius > 5)
        {
            virusZone.radius -= scaleAmount;

            Vector3 currentScale = virusZoneMask.localScale;
            virusZoneMask.localScale = new Vector3(currentScale.x - scaleAmount*2, currentScale.y - scaleAmount*2, 1);
        }
    }
}
