using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class canvasAbout : MonoBehaviour
{
    
    public GameObject about1, about2, about3;

    public void back(){
        SceneManager.LoadScene("Menu");
    }

    private void Start() {
        about1.GetComponent<Animator>().SetBool("visible", true);
        StartCoroutine(WaitForSeconds(0));
    }

    IEnumerator WaitForSeconds(int ke)
    {
        if(ke == 0){
            yield return new WaitForSecondsRealtime(5f);
            about1.GetComponent<Animator>().SetBool("visible", false);
            StartCoroutine(WaitForSeconds(1));
        }
        else if(ke == 1){
            yield return new WaitForSecondsRealtime(2f);
            about2.GetComponent<Animator>().SetBool("visible", true);
            StartCoroutine(WaitForSeconds(2));
        }
        else if(ke == 2){
            yield return new WaitForSecondsRealtime(5f);
            about2.GetComponent<Animator>().SetBool("visible", false);
            StartCoroutine(WaitForSeconds(3));
        }
        else if(ke == 3){
            yield return new WaitForSecondsRealtime(2f);
            about3.GetComponent<Animator>().SetBool("visible", true);
        }
    }
}
