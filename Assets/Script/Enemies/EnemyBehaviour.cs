﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
    //healthbar : https://youtu.be/v1UGTTeQzbo

    //atk boss
    float currentTime = 0f;
    float startingTime = 30f;

    public static int cekStartTimer = 1;


    //untuk healthbar
    float hp;
    float maxhp;
    public HealthBarBehaviour HealthBarBehaviour;

    public GameObject tower;

    public GameObject laserBoss;

    float towerHP;

    //prefabs coin
    public GameObject coin;
    public bool isDead;

    GameObject canvasGame;

    public string jenisMusuh;

    //cek apakah script sudah dijalankan
    bool isUsed;

    GameObject canvas;

    private void Start()
    {
        //init
        canvasGame = GameObject.Find("Canvas");

        if(jenisMusuh == "Boss"){
            //laserBoss = GameObject.Find("GrupEnemies/LaserBoss");
        }

        if(tower != null){
            towerHP = tower.GetComponent<diserang>().hp;
        }

        if(jenisMusuh == "Boss"){
            maxhp = 150;
        }
        else if(jenisMusuh == "Shooter"){
            maxhp = 30;
        }
        else{
            maxhp = 20;
        }
        
        hp = maxhp;
        isDead = false;

        //atur healthbar setiap terjadi perubahan hp
        HealthBarBehaviour.SetHealth(hp, maxhp);

        //coin
        coin = GameObject.Find("1024x128_0");
        currentTime = startingTime;

        canvas = GameObject.Find("Canvas");
    }
    
    public void takeHit(float damage){
        hp -= damage;

        if(jenisMusuh == "Boss"){
            //Debug.Log("Kena bomb");
        }

        //atur healthbar setiap terjadi perubahan hp
        HealthBarBehaviour.SetHealth(hp, maxhp);

        //cek mati
        if (hp <= 0)
        {
            hp = 0;
            
            isDead = true;

            //tambahkan skor
            int skor = int.Parse(canvas.GetComponent<canvasGame>().textScore.text.ToString());

            canvas.GetComponent<canvasGame>().textScore.SetText(skor.ToString());

            //hancurkan objek
            if(jenisMusuh == "Boss"){
                //canvas.GetComponent<canvasGame>().winGame();
                SceneManager.LoadScene("Storyline");
                StoryAssistant.menang = true;
                Debug.Log("BOS SUDAH MATI");
            }
            else if(jenisMusuh == "Kamikaze"){
                skor += 20;
                Destroy(gameObject);
                //buat coin
                GameObject tmpObj = Instantiate(coin);
                tmpObj.name = "2";
                tmpObj.tag = "Coin";
                 // set color
                byte color = canvasGame.GetComponent<canvasGame>().changeColor();
                tmpObj.transform.GetComponent<SpriteRenderer>().color = new Color32(color, color, color, 255);

                SpawnCoins.squares.Add(tmpObj);
                tmpObj.transform.position = gameObject.transform.position;
            }
            else{
                skor += 30;
                Destroy(gameObject);
                //buat coin
                GameObject tmpObj = Instantiate(coin);
                tmpObj.name = "5";
                tmpObj.tag = "Coin";

                 // set color
                byte color = canvasGame.GetComponent<canvasGame>().changeColor();
                tmpObj.transform.GetComponent<SpriteRenderer>().color = new Color32(color, color, color, 255);

                SpawnCoins.squares.Add(tmpObj);
                tmpObj.transform.position = gameObject.transform.position;
            }
            canvasGame.GetComponent<canvasGame>().textScore.text = skor.ToString();
        }
        else{
            Debug.Log("HP " + jenisMusuh + " BERKURANG MENJADI " + hp);
        }
    }

    private void Update() {
        if(jenisMusuh == "Boss"){
            timerBoss();
        }
    }

    private void FixedUpdate()
    {
        // if(isDead){
        //     spawnCoin();

        //     isDead = false;

        //     //hancurkan objek
        //     if(jenisMusuh == "Boss"){
        //         Debug.Log("BOS SUDAH MATI");
        //     }
        //     else{
        //         Debug.Log(jenisMusuh + " MATI !!");
        //         Destroy(gameObject);
        //     }
        // }
    }

    public void timerBoss(){
        if(cekStartTimer != 1){
            currentTime -= 1 * Time.deltaTime;
            //Debug.Log(currentTime);
        }
        if(currentTime < 1){
            currentTime = startingTime;
            Debug.Log("Waktu habis");
            //laserBoss.SetActive(true);
            if (laserBoss != null){
                buatPeluru(-3);   
            }
            float temp = towerHP * 30 / 100;
            tower.GetComponent<diserang>().attacked(temp);
            // GameObject wallParent = GameObject.Find("GrupWalls");
            
            // for (int i = 0; i < wallParent.transform.childCount; i++)
            // {
            //     Destroy(wallParent.transform.GetChild(i).gameObject);
            // }
        }
        // if(currentTime == 58){
        //     laserBoss.SetActive(false);
        // }
    }

    public void buatPeluru(float paramDx)
    {
        Debug.Log("x : " + transform.position.x.ToString() + ", y : " + transform.position.y.ToString());
        Vector2 lokasiMuncul = new Vector2(transform.position.x -1f, transform.position.y -0.4f);
        //Vector2 lokasiMuncul = new Vector2(-688.01f,-336.57f);
        GameObject peluru_mc = Instantiate(laserBoss, lokasiMuncul, transform.rotation);

        // setelah dibuat Instantiate baru diatur posisinya
        // paramdx = jarak antar peluru ? (aku krg tau)
        Vector2 posisi = new Vector2(2 * paramDx, 0);
        peluru_mc.GetComponent<PeluruBoss>().init(posisi);

        //atur supaya shooter yg nembak menjadi parent peluru
        peluru_mc.transform.parent = gameObject.transform;

        Destroy(peluru_mc, 10f);
    }
}
