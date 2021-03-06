﻿using System.Collections;
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

    
    //tutorial
    public bool modeTutorial;
    public TextMeshProUGUI textTutorial;
    public GameObject aplaceWall,abuyBomb;
    
    bool cekCtr = false;

    private void Start() {
        grupWalls = GameObject.Find("GrupWalls");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
                                PolygonCollider2D col = w.GetComponent<PolygonCollider2D>();
                                if(shop == 2){
                                    for(float i = -50; i < 50; i+=0.1f){
                                        for(float j = -50; j < 50; j+= 0.1f){
                                            if(col.OverlapPoint(Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x + i, mousePos.y + j, 10)))){
                                                can = false;
                                            }
                                        }
                                    } 
                                }
                                else{
                                    for(float i = -15; i < 15; i+=0.1f){
                                        for(float j = -15; j < 15; j+= 0.1f){
                                            if(col.OverlapPoint(Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x + i, mousePos.y + j, 10)))){
                                                can = false;
                                            }
                                        }
                                    } 
                                }
                            }
                        }
                        if(shop == 2){
                            if(mousePos.x < -5){
                                can = false;
                            }
                        }
                        if(can == true){
                            GameObject empty = new GameObject();
                            empty.transform.parent = grupWalls.transform;

                            GameObject tmpObj = Instantiate(wall);
                            tmpObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
                            tmpObj.transform.parent = empty.transform;
                            byte color = canvas.GetComponent<canvasGame>().changeColor();
                            tmpObj.transform.GetComponent<SpriteRenderer>().color = new Color32(color, color, color, 255);
                            
                            empty.name = "walls_luar";
                            tmpObj.name = "wall_dalam";
                            canvas.GetComponent<canvasGame>().shop = -1;
                            canvas.GetComponent<canvasGame>().panelCancel.SetActive(false);
                            Cursor.SetCursor(canvas.GetComponent<canvasGame>().cursor, Vector2.zero, CursorMode.ForceSoftware);
                            if(modeTutorial == true){
                                if(cekCtr == false){
                                    cekCtr = true;
                                    aplaceWall.SetActive(false);
                                    textTutorial.text = "When the kamikaze type enemy dies, it will drop a coin. Click on the coin to collect it.";
                                }
                                else{
                                    abuyBomb.SetActive(true);
                                    aplaceWall.SetActive(false);
                                    textTutorial.text = "Try to buy a bomb";
                                }
                            }
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
