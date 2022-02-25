using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public GameObject liveWpp;
    public Toggle liveWppToggle;

    public GameObject infoPanel;
    public GameObject comingSoonDialog;

    // Start is called before the first frame update
    void Start()
    {
        infoPanel.SetActive(false);
        comingSoonDialog.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        liveWpp.SetActive(liveWppToggle.isOn);
    }

    public void SinglePlayer()
    {
        //SceneManager.LoadScene("SinglePlayer");
        comingSoonDialog.SetActive(true);
        infoPanel.SetActive(false);
    }

    public void CloseComingSoonDialog()
    {
        comingSoonDialog.SetActive(false);
    }

    public void MultiPlayer()
    {
        SceneManager.LoadScene("LoadingServer");
    }

    public void Info()
    {
        infoPanel.SetActive(!infoPanel.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
