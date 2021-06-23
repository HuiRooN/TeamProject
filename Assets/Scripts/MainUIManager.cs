using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject ExplainPanel;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        ExplainPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GameEnd()
    {
        Application.Quit();
    }

    public void GameExplain()
    {
        ExplainPanel.SetActive(true);
    }

    public void CloseButton()
    {
        ExplainPanel.SetActive(false);
    }
}

