using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class canvasMenu : MonoBehaviour
{
    public GameObject panelHelp;
    public GameObject panelMenu;
    public GameObject panelExit;
    public Button btnSound;
    public Texture2D cursor;
    GameObject sound;
    
    public AudioClip main;

    // Start is called before the first frame update
    void Start()
    {
        sound = GameObject.FindGameObjectWithTag("Music");
        if(sound == null){
            GameObject Sound = new GameObject();
            Sound.tag = "Music";
            Sound.name = "Sound";
            Sound.AddComponent<SoundEffect>();
            Sound.AddComponent<staticSound>();
            Sound.GetComponent<SoundEffect>().soundEff = new AudioClip[1] {main};
            sound = GameObject.FindGameObjectWithTag("Music");
            sound.GetComponent<SoundEffect>().playSound(0, true, 0.2f);
        }
        if(staticResources.Instance() != null){
            btnSound.GetComponent<Image>().sprite = sound.GetComponent<SoundEffect>().getSprite();
        }
        Time.timeScale = 1;
        Cursor.visible = true;
        
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        Debug.Log("played");
    }

    public void showHelp(){
        panelHelp.GetComponent<Animator>().SetBool("open", true);
    }

    public void enableDisable(){
        sound.GetComponent<SoundEffect>().enableDisable();
    }

    public void startGame(){
        // SceneManager.LoadScene("Tutorial");
        // SceneManager.LoadSceneAsync("Loading");
        canvasLoading.sceneLoad = 0;
        SceneManager.LoadScene("Loading");
        Time.timeScale = 1;
    }

    public void openAbout(){
        SceneManager.LoadScene("About");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openExit(){
        panelExit.GetComponent<Animator>().SetBool("open", true);
    }

    public void closeExit(){
        panelExit.GetComponent<Animator>().SetBool("open", false);
    }

    public void exit(){
        Application.Quit();
    }
}
