﻿using System.Collections;
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

    List<GameObject> squares;

    public TextMeshProUGUI score_txt;

    // Start is called before the first frame update
    void Start()
    {
        squares = new List<GameObject>();
        releasedbutton = true;
        canplace = false;
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
                    GameObject tmpObj = Instantiate(text);
                    tmpObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
                    tmpObj.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("ForegroundLayer");
                    int score = int.Parse(score_txt.text);
                    score += 1;
                    score_txt.text = score.ToString();
                    squares.Remove(square);
                    DestroyImmediate(square);
                    canplace = false;
                }
            }
            
        }
        if(Input.GetMouseButtonUp(0))
        {
            releasedbutton = true;
            canplace = false;
        }
           
        if(releasedbutton == false && canplace)
        {
            GameObject tmpObj = Instantiate(coin);
            
            squares.Add(tmpObj);

            tmpObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
            canplace = false;

        }

    }
}