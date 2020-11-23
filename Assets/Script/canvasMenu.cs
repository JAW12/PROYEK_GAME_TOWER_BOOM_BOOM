using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class canvasMenu : MonoBehaviour
{
    public GameObject panelHelp;
    public GameObject panelMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        panelHelp.SetActive(false);
    }

    public void showHelp(){
        panelHelp.SetActive(true);
    }

    public void closeHelp(){
        panelHelp.SetActive(false);
    }

    public void startGame(){
        // SceneManager.LoadScene("Tutorial");
        SceneManager.LoadScene("Game");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
