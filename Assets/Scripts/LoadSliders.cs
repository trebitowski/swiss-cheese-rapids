using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadSliders : MonoBehaviour
{
    public Slider soundSlider;
    public Slider musicSlider;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager audio = FindObjectOfType<AudioManager>();

        soundSlider.value = audio.soundVolume;
        musicSlider.value = audio.musicVolume;
        
        audio.soundSlider = soundSlider;
        audio.musicSlider = musicSlider;
        audio.AddListeners();
    }
}
