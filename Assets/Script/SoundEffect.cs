using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SoundEffect : MonoBehaviour
{
    public static bool soundOn = true;

    public Sprite getSprite(){
        if(soundOn){
            return staticResources.Instance().getSprite("sound_enable");
        }
        else{
            return staticResources.Instance().getSprite("sound_disable");
        }
    }

    public void enableDisable(){
        soundOn = !soundOn;
        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        if(soundOn){
            button.GetComponent<Image>().sprite = staticResources.Instance().getSprite("sound_enable");
            audio.UnPause();
        }
        else{
            button.GetComponent<Image>().sprite = staticResources.Instance().getSprite("sound_disable");
            audio.Pause();
        }
    }

    public AudioClip[] soundEff; 
    AudioSource audio;

    // Start is called before the first frame update
    void Awake()
    {   audio = gameObject.AddComponent(typeof(AudioSource)) as AudioSource; 
    }

    public void playSound(int i, bool loop, float volume)
    {   
        //i = index audio source dari array audio clip
        audio.Stop();
        audio.volume = volume;
        if (loop == true)
        {
            audio.clip = soundEff[i];
            audio.loop = true;
            audio.Play();
        }
        else
        { 
            audio.PlayOneShot(soundEff[i]); 
        }
        if(!soundOn){
            audio.Pause();
        }
    }
}
