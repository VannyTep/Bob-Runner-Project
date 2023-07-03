using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AudioManager : MonoBehaviour
{
    [Header("AudioBtn")]
    bool isClick_Mute = false;
    [SerializeField] private GameObject _musicBtn;
    [SerializeField] private Sprite _MuteBtnImage;
    [SerializeField] private Sprite _UnMuteBtnImage;

    [Space]

    public Sound[] sounds;

    public static AudioManager instance;

    void Start() 
    {
        Play("Theme Song");

        _musicBtn = GameObject.Find("Music");
    }
    void Awake()
    {

        // if (instance == null)
        //     instance = this;
        // else
        // {
        //     Destroy(gameObject);
        //     return;
        // }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.Clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loops;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if(s == null)
        {
            Debug.LogWarning($"{name} is not Found");
            return;
        }

        s.source.Play();
    }

    public void MuteMusic(string name)
    {
        isClick_Mute = !isClick_Mute;

        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (isClick_Mute) 
        {
            // Change Mute Icon
            _musicBtn.GetComponent<Image>().sprite = _MuteBtnImage;

            // Mute Music
            s.source.volume = 0;
        } 
        else 
        {
            // Change UnMute Icon
            _musicBtn.GetComponent<Image>().sprite = _UnMuteBtnImage;
            
            // UnMute Music
            s.source.volume = 1;
        }
    }
}
