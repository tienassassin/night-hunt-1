using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarColor : MonoBehaviour
{
    Image hpImage;

    // Start is called before the first frame update
    void Start()
    {
        hpImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hpImage.fillAmount > 0.5f) hpImage.color = Color.green;
        else if (hpImage.fillAmount > 0.25f) hpImage.color = Color.yellow;
        else hpImage.color = Color.red;
    }
}
