using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    public GameObject optionPanel;
    public GameObject noticePanel;
    
    public void Notice_Button()
    {
        noticePanel.SetActive(true);
    }

    public void Notice_Exit()
    {
        noticePanel.SetActive(false);
    }

    public void Option_Button()
    {
        optionPanel.SetActive(true);
    }

    public void Option_Exit()
    {
        optionPanel.SetActive(false);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
