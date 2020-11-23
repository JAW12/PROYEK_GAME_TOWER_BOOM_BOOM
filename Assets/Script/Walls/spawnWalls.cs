﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnWalls : MonoBehaviour
{
    Vector3 mousePos;
    public GameObject wall;
    public GameObject area;
    bool can = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();      
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == area.gameObject)
                {
                    mousePos = Input.mousePosition;
                    Debug.Log("Click");
                    can = true;
                    GameObject[] walls = GameObject.FindGameObjectsWithTag("Walls");
                    foreach(GameObject w in walls){
                        BoxCollider2D col = w.GetComponent<BoxCollider2D>();
                        if(col.OverlapPoint(Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10)))){
                            can = false;
                        }
                    }
                    if(can == true){
                        GameObject tmpObj = Instantiate(wall);
                        mousePos = Input.mousePosition;
                        tmpObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, 142.5f, 10));
                        tmpObj.gameObject.tag = "Walls";
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
