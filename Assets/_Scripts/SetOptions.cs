using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetOptions : MonoBehaviour {

	public AudioMixer mainMixer;					//Used to hold a reference to the AudioMixer mainMixer
    public GameOptions GameOptions;

    private void Awake()
    {
        if (GameOptions != null)
        {
            GameOptions.IsInvertedPitch = true;
        }
    }

	//Call this function and pass in the float parameter musicLvl to set the volume of the AudioMixerGroup Music in mainMixer
	public void SetMusicLevel(float musicLvl)
	{
		mainMixer.SetFloat("musicVol", musicLvl);
	}

	//Call this function and pass in the float parameter sfxLevel to set the volume of the AudioMixerGroup SoundFx in mainMixer
	public void SetSfxLevel(float sfxLevel)
	{
		mainMixer.SetFloat("sfxVol", sfxLevel);
	}

    public void SetInvertPitch(bool ignore)
    {
        GameOptions.IsInvertedPitch = !GameOptions.IsInvertedPitch;
    }

}
