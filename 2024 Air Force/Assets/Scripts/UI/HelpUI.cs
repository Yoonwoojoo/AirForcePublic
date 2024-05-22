using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpUI : MonoBehaviour
{
    public Button HelpUIBttn;
    public Button ContinueBttn;

    public GameObject Help;

    // Start is called before the first frame update
    void Start()
    {
        HelpUIBttn.onClick.AddListener(ActivateHelpUI);
        ContinueBttn.onClick.AddListener(Continue);
    }

    // 게임 계속하기
    private void Continue()
    {
        Time.timeScale = 1f;
        Help.SetActive(false);
    }

    // 도움 UI 활성화, 게임 일시정지
    private void ActivateHelpUI()
    {
        Help.SetActive(true);
        Time.timeScale = 0f;
    }
}
