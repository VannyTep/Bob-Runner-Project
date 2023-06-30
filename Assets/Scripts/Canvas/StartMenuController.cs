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

    [Header("AudioBtn")]
    bool isClick_Mute = false;
    [SerializeField] private AudioManager _audioManager;


    void Start()
    {
        _settingInfoAnimator = GameObject.FindGameObjectWithTag("SettingInfo").GetComponent<Animator>();
        _threeStripAnimator = GameObject.FindGameObjectWithTag("ThreeStrip").GetComponent<Animator>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void StartGame(string ScenceName)
    {
        SceneManager.LoadScene(ScenceName);
    }

    public void SettingInfo()
    {
        isClick_Outer_SettingInfo = !isClick_Outer_SettingInfo;
        _settingInfoAnimator.SetBool("IsOuter", isClick_Outer_SettingInfo);
    }

    public void ThreeStrip()
    {
        isClick_Outer_ThreeStrip = !isClick_Outer_ThreeStrip;
        _threeStripAnimator.SetBool("IsOuter", isClick_Outer_ThreeStrip);
    }

    public void MuteMusic()
    {
        isClick_Mute = !isClick_Mute;

        foreach (Sound s in _audioManager.sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.Clip;

            if (isClick_Mute) 
            {
                // Change Mute Icon

                // Mute Music
            
            } 
            else 
            {
                // Change UnMute Icon

                // UnMute Music
            }
        }
        
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quitting the Game.");
    }
}
