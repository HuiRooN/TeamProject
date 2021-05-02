using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClose : MonoBehaviour
{
    private CanvasGroup canvas_group;
    void Awake()
    {
        canvas_group = GetComponent<CanvasGroup>();
    }

   
    public void OpenOrClose()
    {
        canvas_group.alpha = canvas_group.alpha > 0 ? 0 : 1;

        canvas_group.blocksRaycasts = canvas_group.blocksRaycasts == true ? false : true;
    }
}
