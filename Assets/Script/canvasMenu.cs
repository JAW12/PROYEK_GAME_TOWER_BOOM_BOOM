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
    
    // Start is called before the first frame update
    void Start()
    {
        
        Time.timeScale = 1;
        Cursor.visible = true;
        if(staticResources.Instance() != null){
            btnSound.GetComponent<Image>().sprite = gameObject.GetComponent<SoundEffect>().getSprite();
        }
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        GetComponent<SoundEffect>().playSound(0, true, 0.2f);
        Debug.Log("played");
    }

    public void showHelp(){
        panelHelp.GetComponent<Animator>().SetBool("open", true);
    }

    public void startGame(){
        // SceneManager.LoadScene("Tutorial");
        // SceneManager.LoadSceneAsync("Loading");
        canvasLoading.sceneLoad = 0;
        SceneManager.LoadScene("Loading");
        Time.timeScale = 1;
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
