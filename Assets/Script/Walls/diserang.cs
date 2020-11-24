﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class diserang : MonoBehaviour
{
    public float max_hp;
    public float hp;
    public int jenis;
    // 0 -> wooden wall
    // 1 -> stone wall
    // 2 -> large stone wall
    // 3 -> tower
    
    public GameObject prefabsExplosion;
    GameObject grupExplosion;
    GameObject canvas;

    public Sprite wooden_wall_0;
    public Sprite wooden_wall_1;
    public Sprite wooden_wall_2;
    public Sprite wooden_wall_3;
    public Sprite wooden_wall_4;
    public Sprite[] wooden_wall;

    private void Start() {
        grupExplosion = GameObject.Find("GrupEnemies/GrupExplosion");
        canvas = GameObject.Find("Canvas");
        wooden_wall = new Sprite[] {wooden_wall_0, wooden_wall_1, wooden_wall_2, wooden_wall_3, wooden_wall_4};
    }

    private void changeSkin(int swap){
        if(jenis == 0){
            gameObject.GetComponent<SpriteRenderer>().sprite = wooden_wall[swap];
        }
    }

    void Update(){
        if(hp <= 0 && jenis >= 0 && jenis <= 2){
            wallDestroyed();
        }
    }

    public int hit = 0;
    public void attacked(float damage){
        hp -= damage;
        int swap = 0;
        if(hp / max_hp < 0.9f){
            swap = 1;
        }
        if(hp / max_hp < 0.7f){
            swap = 2;
        }
        if(hp / max_hp <= 0.5f){
            swap = 3;
        }
        if(hp / max_hp < 0.2f){
            swap = 4;
        }
        changeSkin(swap);
        if(jenis == 3){
            canvas.GetComponent<canvasGame>().textHP.text = hp.ToString() + "/100";
            var rect = canvas.GetComponent<canvasGame>().health.transform;
            rect.transform.localScale = new Vector2(hp/100, rect.transform.localScale.y);
        }
        // if(jenis == 0){
        //     Debug.Log(hp);
        // }
        StartCoroutine(WaitForSeconds());
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Kamikaze1")){
            if(hit == 0){
                Debug.Log("kena serang kamikaze1");
                hit = 1;
                attacked(5f);
            }
        }
        else if(other.CompareTag("Kamikaze2")){
            if(hit == 0){
                Debug.Log("kena serang kamikaze2");
                hit = 1;
                attacked(5f);
            }
        }
    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        hit = 0;    
    }

    private void wallDestroyed(){
        Destroy(gameObject.transform.parent.gameObject);
    }
}
