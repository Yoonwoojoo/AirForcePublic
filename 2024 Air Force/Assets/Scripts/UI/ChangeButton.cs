using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeButton : MonoBehaviour
{
    public void LoadStartScene()
    {
        //게임 시간을 다시 흐르게 설정
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }

    public void LoadGameScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        // 에디터 모드에서 게임 종료
        EditorApplication.isPlaying = false;
#else
        // 빌드된 게임에서 게임 종료
        Application.Quit();
#endif
    }
}
