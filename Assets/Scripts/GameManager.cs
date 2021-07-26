using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static ViewMode mode = ViewMode.Start;
    private static bool modeChanged = false;
    public static ViewMode Mode {
        get { return mode; }
        set {
            mode = value;
            modeChanged = true;
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // シーン切り替え処理
    void Update()
    {
        if (!modeChanged) {
            return;
        }

        switch (mode)
        {
            case ViewMode.Start:
                SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
                Debug.Log("changed to StartScreen");
                break;
            case ViewMode.StageSelect:
                SceneManager.LoadScene("StageSelectScreen", LoadSceneMode.Single);
                Debug.Log("changed to StageSelectScreen");
                break;
            case ViewMode.Game:
                SceneManager.LoadScene("GameScreen", LoadSceneMode.Single);
                Debug.Log("changed to GameScreen");
                break;
            case ViewMode.Result:
                SceneManager.LoadScene("ResultScreen", LoadSceneMode.Single);
                Debug.Log("changed to ResultScreen");
                break;
            case ViewMode.Pause:
                SceneManager.LoadScene("PauseScreen", LoadSceneMode.Single);
                Debug.Log("changed to PauseScreen");
                break;
            case ViewMode.Option:
                SceneManager.LoadScene("OptionScreen", LoadSceneMode.Single);
                Debug.Log("changed to OptionScreen");
                break;
        }

        modeChanged = false;
    }
}
