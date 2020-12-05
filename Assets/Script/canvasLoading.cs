using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class canvasLoading : MonoBehaviour
{
    public static int sceneLoad;
    // 0 -> Storyline
    // 1 -> Tutorial
    // 2 -> Game
    // 3 -> Menu

    public GameObject progress;
    // Start is called before the first frame update
    float ctr = 0.03f;
    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update() {
        progress.transform.localScale = new Vector3(ctr/3, progress.transform.localScale.y, 1);
        if(ctr / 3 < 1f){
            ctr += 0.01f;
        }
        else{
            if(sceneLoad == 0){
                StoryAssistant.menang = false;
                SceneManager.LoadScene("Storyline");
            }
            else if(sceneLoad == 1){
                SceneManager.LoadScene("Tutorial");
            }
            else if(sceneLoad == 2){
                SceneManager.LoadScene("Game");
            }
        }
    }
}
