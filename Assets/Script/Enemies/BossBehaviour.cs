using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 60f;
    public static int cekStartTimer = 1;
    // Start is called before the first frame update

    float hp;
    float maxhp = 120;
    //untuk healthbar
    public BossHealthBarBehaviour HealthBarBehaviour;

    public GameObject wallParent;
    public GameObject canvasGame;

    Vector3 mousePos;
    void Start()
    {
        hp = maxhp;

        //atur healthbar setiap terjadi perubahan hp
        HealthBarBehaviour.SetHealth(hp, maxhp);
        currentTime = startingTime;
        takeHit(30);
        canvasGame = GameObject.Find("Canvas");
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

    // void OnCollisionEnter2D(Collision2D other) {
    //     takeHit(30);
    //     cekStartTimer = 1;
    // }

    // Update is called once per frame
    void Update()
    {
        if(canvasGame.GetComponent<canvasGame>().isPaused == false){
            if(cekStartTimer != 1){
                currentTime -= 1 * Time.deltaTime;
                //Debug.Log(currentTime);
                takeHit(1);
            }
            if(currentTime < 1){
                currentTime = startingTime;
                cekStartTimer = 1;
                Debug.Log("Waktu habis");
                
                for (int i = 0; i < wallParent.transform.childCount; i++)
                {
                    Destroy(wallParent.transform.GetChild(i).gameObject);
                }
            }
            // BoxCollider2D col = this.GetComponent<BoxCollider2D>();

            // if(col.OverlapPoint(Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10))))
            // {
            //     cekStartTimer = 1;
            // }
        }
    }
}
