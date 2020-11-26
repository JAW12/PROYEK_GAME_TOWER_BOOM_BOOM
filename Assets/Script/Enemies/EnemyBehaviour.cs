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

    private void Start()
    {
        hp = maxhp;

        //atur healthbar setiap terjadi perubahan hp
        HealthBarBehaviour.SetHealth(hp, maxhp);
    }
    
    public void takeHit(float damage){
        hp -= damage;

        //atur healthbar setiap terjadi perubahan hp
        HealthBarBehaviour.SetHealth(hp, maxhp);

        if (hp <= 0)
        {
            hp = 0;
            Destroy(gameObject);
        }
        else{
            Debug.Log("hp berkurang menjadi : " + hp);
        }
    }
}
