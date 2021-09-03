using UnityEngine;
using UnityEngine.SceneManagement;
 
public class Backtitle : MonoBehaviour {
 
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("StartScreen");
    }
 
}