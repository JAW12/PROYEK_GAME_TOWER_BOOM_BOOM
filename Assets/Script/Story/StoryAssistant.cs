using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryAssistant : MonoBehaviour
{
    private TextWriter.TextWriterSingle textWriterSingle;
    private TextMeshProUGUI messageText;
    private int currentStory = 0;
    private string[] messageArray;
    private Sprite[] spriteArray;
    public Sprite story_1;
    public Sprite story_2;
    public Sprite story_3;
    public Sprite story_4;
    public GameObject panel;

    public GameObject btnPrev;
    public GameObject btnNext;
    public GameObject panelExit;
    public GameObject panelTutorial;
    public Texture2D cursor;
    public Button btnSound;
    
    private void Awake() {
        messageText = GameObject.Find("messageText").GetComponent<TextMeshProUGUI>();
        Application.targetFrameRate = 3;
        messageArray = new string[]
        {
            "Once upon a time, in a happily peaceful kingdom...",
            "Some strange things invading. A modern colony comes and wants to invade your stronghold.",
            "Luckily, you have secret weapon that you already prepared. You just need money to use that weapon.",
            "Protect your kingdom by defending your stronghold..",
            "Passing through all the waves of enemies incoming...",
            "and kill the big boss of this modern colony.."
        };
        spriteArray = new Sprite[]{
            story_1,
            story_2,
            story_3,
            story_4,
            story_4,
            story_4
        };
    }        
    
    private void Start() {
        Cursor.visible = true;
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        string message = messageArray[currentStory];
        textWriterSingle = TextWriter.AddWriter_Static(messageText, message, 0.1f, true, true);
        panel.GetComponent<Image>().sprite = spriteArray[currentStory];
        btnPrev.SetActive(false);

        if(staticResources.Instance() != null){
            btnSound.GetComponent<Image>().sprite = gameObject.GetComponent<SoundEffect>().getSprite();
        }
        GetComponent<SoundEffect>().playSound(0, false, 0.2f);
    }

    public void nextStory(){
        if(textWriterSingle != null && textWriterSingle.IsActive()){
            textWriterSingle.WriteAllAndDestroy();
            if(currentStory == 5){
                panelTutorial.GetComponent<Animator>().SetBool("open", true);
            }
        }
        else{
            if(currentStory < 5){
                currentStory++;
                string message = messageArray[currentStory];
                textWriterSingle = TextWriter.AddWriter_Static(messageText, message, 0.1f, true, true);
                panel.GetComponent<Image>().sprite = spriteArray[currentStory];
                btnPrev.SetActive(true);
                btnNext.SetActive(true);
            }
            else if(currentStory == 5){
                panelTutorial.GetComponent<Animator>().SetBool("open", true);
            }
        }
    }

    public void skipStory(){
        currentStory = 5;
        string message = messageArray[currentStory];
        textWriterSingle = TextWriter.AddWriter_Static(messageText, message, 0.1f, true, true);
        panel.GetComponent<Image>().sprite = spriteArray[currentStory];
        btnPrev.SetActive(true);
        btnNext.SetActive(true);
        panelTutorial.GetComponent<Animator>().SetBool("open", true);
    }

    public void startGame(){
        canvasLoading.sceneLoad = 2;
        SceneManager.LoadScene("Loading");
    }

    public void startTutorial(){
        canvasLoading.sceneLoad = 1;
        SceneManager.LoadScene("Loading");
    }

    public void prevStory(){
        if(textWriterSingle != null && textWriterSingle.IsActive()){
            textWriterSingle.WriteAllAndDestroy();
        }
        else{
            btnPrev.SetActive(true);
            btnNext.SetActive(true);
            currentStory--;
            if(currentStory < 0){
                currentStory = 0;
                btnPrev.SetActive(false);
            }
            string message = messageArray[currentStory];
            textWriterSingle = TextWriter.AddWriter_Static(messageText, message, 0.1f, true, true);
            panel.GetComponent<Image>().sprite = spriteArray[currentStory];
        }
    }

    public void openExit(){
        panelExit.GetComponent<Animator>().SetBool("open", true);
    }

    public void closeExit(){
        panelExit.GetComponent<Animator>().SetBool("open", false);
    }

    public void exit(){
        SceneManager.LoadScene("Menu");
    }

    
}
