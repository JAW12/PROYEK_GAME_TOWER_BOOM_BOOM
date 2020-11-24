﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze2 : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 posisiKamikaze;
    Transform parent;
    float x;

    //kecepatan gerak
    public float jarakBergerak;

    //prefabs explosion
    public GameObject prefabsExplosion;
    GameObject grupExplosion;
    GameObject canvas;
    bool kenaWall, kenaTower;
    bool sudahExplode;

    private int ctrColidding = 0;

    void Start()
    {        
        //deklarasi
        grupExplosion = GameObject.Find("GrupEnemies/GrupExplosion");
        canvas = GameObject.Find("Canvas");

        //waktu gerak nanti yg berubah adalah posisi gameobject parent nya
        parent = gameObject.transform.parent;
        posisiKamikaze = parent.gameObject.transform.position;
        x = posisiKamikaze.x;

        setKondisiAwalKamikaze();
    }

    public void setKondisiAwalKamikaze(){     
        //deklarasi value awal
        kenaWall = false;
        kenaTower = false;
        sudahExplode = false;

        //atur jarak bergerak
        //kamikaze panjang -> gerak lebih lambat
        // jarakBergerak = 0.007f;
        // jarakBergerak = 0.02f;
        jarakBergerak = 0.009f;
    }

    // Update is called once per frame
    void Update()
    {
        if(canvas.GetComponent<canvasGame>().isPaused == false){
            //kurangi posisi kl masih blm meledak
            if (kenaTower == false && kenaWall == false)
            {
                x -= jarakBergerak;
                posisiKamikaze.x = x;
            }
        }
    }

    private void FixedUpdate()
    {
        if(canvas.GetComponent<canvasGame>().isPaused == false){
            //atur posisi
            parent.transform.position = posisiKamikaze;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collision: " + other.name);
        if (ctrColidding == 0)
        {
            //cek apakah peluru mengenai walls / tower
            if (other.CompareTag("Walls"))
            {
                kenaWall = true;
                kamikazeExplode();
                // Debug.Log("Kamikaze 2 kena wall. ctr : " + ctrColidding);
            }
            else if(other.CompareTag("Tower")){
                kenaTower = true;
                kamikazeExplode();
                // Debug.Log("Kamikaze 2 kena tower. ctr : " + ctrColidding);
            }

            ctrColidding = ctrColidding + 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(ctrColidding == 1){
            //ctrColidding = 0;
        }
    }


    private void kamikazeExplode(){
        if (! sudahExplode)
        {
            sudahExplode = true;

            //hancurkan kamikaze
            Destroy(gameObject);

            //munculkan explosion
            GameObject objExplosion = Instantiate(
                prefabsExplosion, posisiKamikaze, 
                Quaternion.identity
            );

            //atur parent explosion
            objExplosion.transform.parent = grupExplosion.transform;
        }
    }

    private void OnDestroy()
    {
        //waktu kamikaze dihancurkan, hancurkan juga parent
        Destroy(transform.parent.gameObject);
    }
}
