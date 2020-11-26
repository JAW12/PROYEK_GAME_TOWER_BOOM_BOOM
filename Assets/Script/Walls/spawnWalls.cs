using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class spawnWalls : MonoBehaviour
{
    Vector3 mousePos;
    public GameObject wooden_wall;
    public GameObject stone_wall;
    public GameObject large_stone_wall;
    GameObject wall;
    public GameObject area;
    bool can = true;
    public GameObject canvas;
    GameObject grupWalls;

    //deklarasi coin
    public static List<GameObject> squares;
    public GameObject text;
    public TextMeshProUGUI textCoin;
    private void Start() {
        grupWalls = GameObject.Find("GrupWalls");
        //deklarasi coin
        text = GameObject.Find("PopUpText");
        squares = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //cek ada coin
            foreach (GameObject square in squares)
            {
                BoxCollider2D col = square.GetComponent<BoxCollider2D>();

                if(col.OverlapPoint(Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10))))
                {
                    GameObject tmpObj = Instantiate(text);
                    tmpObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
                    tmpObj.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("ForegroundLayer");
                    int score = int.Parse(textCoin.text);
                    score += 1;
                    textCoin.text = score.ToString();
                    GameObject sound = GameObject.Find("Main Camera");
                    sound.GetComponent<SoundEffect>().playSound(0, false, 1f);
                    squares.Remove(square);
                    DestroyImmediate(square);
                }
            }

            int shop = canvas.GetComponent<canvasGame>().shop;
            Debug.Log(shop);
            mousePos = Input.mousePosition;

            if(shop >= 0 && shop <= 2){
                if(shop == 0){
                    wall = wooden_wall;
                }
                else if(shop == 1){
                    wall = stone_wall;
                }
                else if(shop == 2){
                    wall = large_stone_wall;
                }
                RaycastHit hit = new RaycastHit();      
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == area.gameObject)
                    {
                        Debug.Log("Click");
                        can = true;
                        GameObject[] walls = GameObject.FindGameObjectsWithTag("Walls");
                        foreach(GameObject w in walls){
                            if(w != null){
                                BoxCollider2D col = w.GetComponent<BoxCollider2D>();
                                if(col.OverlapPoint(Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10)))){
                                    can = false;
                                }
                            }
                        }
                        if(can == true){
                            GameObject empty = new GameObject();
                            empty.transform.parent = grupWalls.transform;

                            GameObject tmpObj = Instantiate(wall);
                            tmpObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
                            tmpObj.transform.parent = empty.transform;

                            empty.name = "walls_luar";
                            tmpObj.name = "wall_dalam";
                            canvas.GetComponent<canvasGame>().shop = -1;
                            canvas.GetComponent<canvasGame>().panelCancel.SetActive(false);
                        }
                        else{
                            Debug.Log("nabrak");
                        }
                    }
                    else
                    {
                        Debug.Log("Click outside");
                    }
                }
                else
                {
                    Debug.Log("Click outside of any object");
                }
            }
        }
    }
}
