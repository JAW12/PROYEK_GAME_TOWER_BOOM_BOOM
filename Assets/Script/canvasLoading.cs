using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class canvasLoading : MonoBehaviour
{
    public GameObject progress;
    // Start is called before the first frame update
    float ctr = 0.03f;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2.7f);
        //SceneManager.LoadScene("Game");
        SceneManager.LoadScene("Tutorial");
    }

    private void Update() {
        progress.transform.localScale = new Vector3(ctr/3, progress.transform.localScale.y, 1);
        if(ctr / 3 < 1f){
            ctr += 0.01f;
        }
    }
}
