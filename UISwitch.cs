using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwitch : MonoBehaviour
{
    public GameObject Begin;
    public GameObject Choose;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openChoose()
    {
        Begin.SetActive(false);
        Choose.SetActive(true);
    }

    public void closeChoose()
    {
        Choose.SetActive(false);
        Begin.SetActive(true);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        // 在 Unity 编辑器中停止运行游戏
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 在打包的游戏中退出游戏
        Application.Quit();
#endif
    }
}
