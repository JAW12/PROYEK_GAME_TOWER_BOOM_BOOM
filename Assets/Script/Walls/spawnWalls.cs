using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
