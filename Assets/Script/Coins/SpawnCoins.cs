using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnCoins : MonoBehaviour
{
    public GameObject coin;

    public GameObject text;

    bool canplace;
    bool releasedbutton;
    Vector3 mousePos;

    public static List<GameObject> squares;

    public TextMeshProUGUI score_txt;

    //tutorial
    public bool modeTutorial;
    public TextMeshProUGUI textTutorial;
    public GameObject aupgradeWall;

    // Start is called before the first frame update
    void Start()
    {
        canvasGame.cekCtr = false;
        squares = new List<GameObject>();
        releasedbutton = true;
        canplace = false;
        //text = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            releasedbutton = false;
            canplace = true;

            foreach (GameObject square in squares)
            {
                BoxCollider2D col = square.GetComponent<BoxCollider2D>();

                if(col.OverlapPoint(Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10))))
                {
                    text.GetComponent<TextMesh>().text = "Coins + " + square.name.ToString(); 
                    GameObject tmpObj = Instantiate(text);
                    tmpObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
                    tmpObj.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("ForegroundLayer");
                    int score = int.Parse(score_txt.text);
                    score += int.Parse(square.name.ToString());
                    score_txt.text = score.ToString();
                    GameObject sound = GameObject.Find("Main Camera");
                    sound.GetComponent<SoundEffect>().playSound(0, false, 1f);
                    squares.Remove(square);
                    DestroyImmediate(square);
                    canplace = false;

                    if(modeTutorial == true){
                        if(canvasGame.cekCtr == false){
                            aupgradeWall.SetActive(true);
                            textTutorial.text = "Upgrade the wall so it can withstand more damage.";
                            canvasGame.cekCtr = true;
                        }
                        else{
                            textTutorial.text = "Click the → button to go to the game";
                        }
                    }
                }
            }
            
        }
        // if(Input.GetMouseButtonUp(0))
        // {
        //     releasedbutton = true;
        //     canplace = false;
        // }
           
        // if(releasedbutton == false && canplace)
        // {
        //     GameObject tmpObj = Instantiate(coin);
            
        //     squares.Add(tmpObj);

        //     tmpObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
        //     canplace = false;

        // }

    }
}
