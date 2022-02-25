using UnityEngine;
using UnityEngine.UI;

public class ShowFramerate : MonoBehaviour
{
    public Text fpsText;
    public float deltaTime;

    float delay = 1f;

    void Update()
    {
        delay -= Time.deltaTime;

        if (delay <= 0)
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsText.text = "FPS " + Mathf.Ceil(fps).ToString();
            delay = 1f;
        }
    }
}
