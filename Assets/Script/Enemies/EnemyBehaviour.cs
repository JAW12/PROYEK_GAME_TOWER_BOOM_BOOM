using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    //healthbar : https://youtu.be/v1UGTTeQzbo

    //atk boss
    float currentTime = 0f;
    float startingTime = 60f;

    public static int cekStartTimer = 1;


    //untuk healthbar
    float hp;
    float maxhp;
    public HealthBarBehaviour HealthBarBehaviour;

    //prefabs coin
    public GameObject coin;
    bool isDead;

    GameObject canvasGame;

    public string jenisMusuh;

    private void Start()
    {
        //init
        canvasGame = GameObject.Find("Canvas");

        if(jenisMusuh == "Boss"){
            maxhp = 120;
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
    }
    
    public void takeHit(float damage){
        hp -= damage;

        if(jenisMusuh == "Boss"){
            //Debug.Log("Kena bomb");
        }

        //atur healthbar setiap terjadi perubahan hp
        HealthBarBehaviour.SetHealth(hp, maxhp);

        //tambahkan skor
        int skor = int.Parse(canvasGame.GetComponent<canvasGame>().
            textScore.text.ToString());

        if (jenisMusuh == "Kamikaze")
        {
            skor += 20;
        }
        else if(jenisMusuh == "Shooter"){
            skor += 30;
        }

        canvasGame.GetComponent<canvasGame>().textScore.SetText(skor.ToString());

        //cek mati
        if (hp <= 0)
        {
            hp = 0;
            isDead = true;
           
            //buat coin
            GameObject tmpObj = Instantiate(coin);
            SpawnCoins.squares.Add(tmpObj);
            tmpObj.transform.position = gameObject.transform.position;

            //hancurkan objek
            if(jenisMusuh == "Boss"){
                Debug.Log("BOS SUDAH MATI");
            }
            else{
                Destroy(gameObject);
            }
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
            Debug.Log(currentTime);
        }
        if(currentTime < 1){
            currentTime = startingTime;
            Debug.Log("Waktu habis");
            GameObject wallParent = GameObject.Find("GrupWalls");
            
            for (int i = 0; i < wallParent.transform.childCount; i++)
            {
                Destroy(wallParent.transform.GetChild(i).gameObject);
            }
        }
    }
}
