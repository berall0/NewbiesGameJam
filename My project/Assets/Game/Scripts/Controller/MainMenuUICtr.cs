using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Event;
using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUICtr : MonoBehaviour
{
    public Button startBtn;

    public Button QuitBtn;
    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(() =>
        {
            TypeEventSystem.Global.Send<UIBtnDownEvt>();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        });
        QuitBtn.onClick.AddListener(() =>
        {
            TypeEventSystem.Global.Send<UIBtnDownEvt>();
            Application.Quit();
        });
    }

    
}
