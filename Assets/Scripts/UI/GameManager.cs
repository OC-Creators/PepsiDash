using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{    
    public GameObject[] views;
    private static ViewMode vmode = ViewMode.Title;
    public static ViewMode VMode 
    {
        get { return vmode; }
        set 
        {
            vmode = value;
        }
    }

    private static SceneMode smode = SceneMode.Start;
    public static SceneMode SMode 
    {
        get { return smode; }
        set
        {
            smode = value;
            vmode = smode.GetEntryViewMode();
        }
    }

    void Start()
    {
        
    }

    // シーン切り替え処理
    void Update()
    {

    }

    public bool IsActive(ViewMode vm)
    {
        var vms = vm.ToStringQuickly();
        var obj = Array.Find(views, o => o.name == vms);
        return obj.activeSelf;
    }

    public void SetActive(ViewMode vm, bool active)
    {
        var vms = vm.ToStringQuickly();
        var obj = Array.Find(views, o => o.name == vms);
        obj.SetActive(active);
    }

    public void SwitchScene(SceneMode sm)
    {
        SceneManager.LoadScene(sm.ToStringQuickly(), LoadSceneMode.Single);
        Debug.Log($"changed to {sm}");
        smode = sm;
    }

}
