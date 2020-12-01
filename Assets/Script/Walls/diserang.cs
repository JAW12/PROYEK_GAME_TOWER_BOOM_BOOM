using System.Collections;
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

    public Sprite wall_0;
    public Sprite wall_1;
    public Sprite wall_2;
    public Sprite wall_3;
    public Sprite wall_4;
    public Sprite[] wall;

    //untuk prefabs coin
    public GameObject coin;
    Vector2 posisiMeledak;

    //cek
    Collider2D musuhHit;

    private void Start() {
        grupExplosion = GameObject.Find("GrupEnemies/GrupExplosion");
        canvas = GameObject.Find("Canvas");
        wall = new Sprite[] {wall_0, wall_1, wall_2, wall_3, wall_4};

        //coin
        coin = GameObject.Find("1024x128_0");
    }

    private void changeSkin(int swap){
        gameObject.GetComponent<SpriteRenderer>().sprite = wall[swap];
    }

    void Update(){
        //pengecekan musuh nabrak buat coin
        if(musuhNabrak){

            //reset
            musuhNabrak = false;
        }
        
        if(hp <= 0 && jenis >= 0 && jenis <= 2){
            wallDestroyed();
        }
    }

    private void FixedUpdate()
    {
        
    }

    public int hit = 0;
    public void attacked(float damage){
        hp -= damage;
        if(hp < 0){
            hp = 0;
        }
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
        if(jenis != 3){
            changeSkin(swap);
        }
        if(jenis == 3){
            canvas.GetComponent<canvasGame>().textHP.text = hp.ToString() + "/100";
            var rect = canvas.GetComponent<canvasGame>().health.transform;
            rect.transform.localScale = new Vector2(hp/100, rect.transform.localScale.y);
            if(hp <= 0){
                canvas.GetComponent<canvasGame>().gameOver();
            }
        }
        
        // if(jenis == 0){
        //     Debug.Log(hp);
        // }
        StartCoroutine(WaitForSeconds());
    }

    bool musuhNabrak;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Kamikaze1") || other.CompareTag("Kamikaze2")){
            EnemyBehaviour scriptEnemyBehaviour = other.GetComponent<EnemyBehaviour>();
            if(! scriptEnemyBehaviour.isDead){
                musuhNabrak = true;
                kamikazeMeledak(other);

                // if (other.CompareTag("Kamikaze1"))
                // {
                //     musuhNabrak = true;
                //     kamikazeMeledak(other);
                // }
                // else if (other.CompareTag("Kamikaze2"))
                // {
                //     musuhNabrak = true;
                //     kamikazeMeledak(other);
                // }
            }       
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Kamikaze1")){
            if(hit == 0){
                //Debug.Log("kena serang kamikaze1");
                hit = 1;
                attacked(5f);
            }
        }
        else if(other.CompareTag("Kamikaze2")){
            if(hit == 0){
                //Debug.Log("kena serang kamikaze2");
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

    private void kamikazeMeledak(Collider2D musuh){
        posisiMeledak = musuh.gameObject.transform.position;
        // Vector2 posisiMeledak = gameObject.transform.position;

        // //buat coin
        // GameObject tmpObj = Instantiate(coin);
        // spawnWalls.squares.Add(tmpObj);
        // tmpObj.transform.position = posisiMeledak;

        //munculkan explosion
        GameObject objExplosion = Instantiate(
            prefabsExplosion, posisiMeledak, 
            Quaternion.identity
        );

        //atur parent explosion
        objExplosion.transform.parent = grupExplosion.transform;
        
        // set color
        byte color = canvas.GetComponent<canvasGame>().changeColor();
        objExplosion.transform.GetComponent<SpriteRenderer>().color = new Color32(color, color, color, 255);


        musuh.GetComponent<EnemyBehaviour>().takeHit(20);
    }
}
