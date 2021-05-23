using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    public AudioMixer mainMixer;
    public AudioMixer joMixer;
    public Toggle theToggle;

    private void Start()
    {
        theToggle.onValueChanged.AddListener(delegate {
            ToggleValueChange(theToggle);
        });
    }

    void ToggleValueChange(Toggle tglValue)
    {
        if(tglValue.isOn == false){
            mainMixer.SetFloat("volume", -80);
            joMixer.SetFloat("volume", -80);
        } else {
            mainMixer.SetFloat("volume", 0);
            joMixer.SetFloat("volume", 0);
        }
    }

}
