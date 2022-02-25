using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameFollow : MonoBehaviour
{
    Transform cam;

    Vector3 offset = new Vector3(0f, 1f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cam.rotation *
            Vector3.forward, cam.rotation * Vector3.up);

        transform.position = transform.parent.transform.position
            + offset;
    }
}
