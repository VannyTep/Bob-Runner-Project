using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class StartMenuController : MonoBehaviour
{
    [Header("SettingInfoBtn")]
    [SerializeField] private Animator _settingInfoAnimator;
    bool isClick_Outer_SettingInfo = false;
    
    [Space]

    [Header("ThreeSpritBtn")]
    [SerializeField] private Animator _threeStripAnimator;
    bool isClick_Outer_ThreeStrip = false;

    void Start()
    {
        _settingInfoAnimator = GameObject.FindGameObjectWithTag("SettingInfo").GetComponent<Animator>();
        _threeStripAnimator = GameObject.FindGameObjectWithTag("ThreeStrip").GetComponent<Animator>();
        
    }

    public void StartGame(string ScenceName)
    {
        SceneManager.LoadScene(ScenceName);
    }

    public void SettingInfo()
    {
        isClick_Outer_SettingInfo = !isClick_Outer_SettingInfo;
        _settingInfoAnimator.SetBool("IsOuter", isClick_Outer_SettingInfo);

        // Play Button Sounds
    }

    public void ThreeStrip()
    {
        isClick_Outer_ThreeStrip = !isClick_Outer_ThreeStrip;
        _threeStripAnimator.SetBool("IsOuter", isClick_Outer_ThreeStrip);

        // Play Button Sounds
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quitting the Game.");

        // Play Button Sounds
    }
}
