using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public float soundVolume = 0.5f;
    public float musicVolume = 0.5f;
    public static AudioManager instance;
    public Slider soundSlider;
    public Slider musicSlider;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start() {
        AddListeners();
        Play("Music");
    }

    public static void Play(string name) {
        Sound s = Array.Find(instance.sounds, sound => sound.Name == name);
        if (s == null) { return; }

        float volumeScale = instance.soundVolume;
        if (s.isMusic) { volumeScale = instance.musicVolume; }
        s.source.volume = s.volume * volumeScale;
        s.source.Play();
    }

    public static void Play(string name, float delay) {
        Sound s = Array.Find(instance.sounds, sound => sound.Name == name);
        if (s == null) { return; }

        float volumeScale = instance.soundVolume;
        if (s.isMusic) { volumeScale = instance.musicVolume; }
        s.source.volume = s.volume * volumeScale;
        s.source.PlayDelayed(delay);
    }

    public void AddListeners() {
        soundVolume = soundSlider.value;
        soundSlider.onValueChanged.AddListener((v) => {
            soundVolume = v;
        });
        musicVolume = musicSlider.value;
        musicSlider.onValueChanged.AddListener((v) => {
            musicVolume = v;
            Sound s = Array.Find(sounds, sound => sound.Name == "Music");
            if (s != null) { s.source.volume = s.volume * musicVolume; }
        });
    }
}
