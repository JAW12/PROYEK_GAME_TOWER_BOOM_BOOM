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

    float hp;
    float maxhp;

    //untuk healthbar
    public HealthBarBehaviour HealthBarBehaviour;

    //prefabs coin
    public GameObject coin;

    public string jenisMusuh;
    private void Start()
    {
        if(jenisMusuh == "Boss"){
            maxhp = 120;
        }
        else{
            maxhp = 20;
        }
        hp = maxhp;

        //atur healthbar setiap terjadi perubahan hp
        HealthBarBehaviour.SetHealth(hp, maxhp);

        coin = GameObject.Find("1024x128_0");
        currentTime = startingTime;
    }
    
    public void takeHit(float damage){
        hp -= damage;
        if(jenisMusuh == "Boss"){
            Debug.Log("Kena bomb");
        }
        //atur healthbar setiap terjadi perubahan hp
        HealthBarBehaviour.SetHealth(hp, maxhp);

        if (hp <= 0)
        {
            hp = 0;
            //buat coin
            GameObject tmpObj = Instantiate(coin);
            SpawnCoins.squares.Add(tmpObj);
            tmpObj.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
        else{
            //Debug.Log("hp berkurang menjadi : " + hp);
        }
    }

    private void Update() {
        timerBoss();
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
