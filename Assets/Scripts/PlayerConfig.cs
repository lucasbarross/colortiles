using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConfig : MonoBehaviour {
    public Toggle colorBlind;
    public Toggle sound;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("firstTime", 1) == 1)
        {
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("firstTime", 0);
            PlayerPrefs.SetFloat("volume", 1);
        }
        else
        {
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
        }
    }

    void Start () {
        colorBlind.isOn = PlayerPrefs.GetInt("colorBlind") != 0;
        sound.isOn = PlayerPrefs.GetFloat("volume") != 0;
    }

    public void setColorBlind(bool active) {
        PlayerPrefs.SetInt("colorBlind", active ? 1 : 0);
    }

    public void toggleMute(bool toggle)
    {
        if (toggle)
        {
            AudioListener.volume = 1f;
        }
        else {
            AudioListener.volume = 0f;
        }

        PlayerPrefs.SetFloat("volume", toggle ? 1f : 0f);
    }


}
