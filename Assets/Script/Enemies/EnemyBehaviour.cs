using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    //healthbar : https://youtu.be/v1UGTTeQzbo
    
    float hp;
    float maxhp = 20;

    //untuk healthbar
    public HealthBarBehaviour HealthBarBehaviour;

    //prefabs coin
    public GameObject coin;


    private void Start()
    {
        hp = maxhp;

        //atur healthbar setiap terjadi perubahan hp
        HealthBarBehaviour.SetHealth(hp, maxhp);

        coin = GameObject.Find("1024x128_0");
    }
    
    public void takeHit(float damage){
        hp -= damage;

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
            Debug.Log("hp berkurang menjadi : " + hp);
        }
    }
}
