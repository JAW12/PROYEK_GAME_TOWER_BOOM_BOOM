using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze1 : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 posisiKamikaze;
    Transform parent;
    Rigidbody2D kamikazeRigidBody;
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

    //prefabs coin
    public GameObject coin;

    GameObject canvasGame;

    void Start()
    {        
        //deklarasi
        canvasGame = GameObject.Find("Canvas");
        grupExplosion = GameObject.Find("GrupEnemies/GrupExplosion");
        canvas = GameObject.Find("Canvas");
        coin = GameObject.Find("1024x128_0");
        kamikazeRigidBody = this.GetComponent<Rigidbody2D>();

        //waktu gerak nanti yg berubah adalah posisi gameobject parent nya
        //parent = gameObject.transform.parent;
        //posisiKamikaze = parent.gameObject.transform.position;

        //jadinya ga pake empty gameobject sbg parent
        posisiKamikaze = this.transform.position;
        x = posisiKamikaze.x;

        setKondisiAwalKamikaze();
    }

    public void setKondisiAwalKamikaze(){     
        //deklarasi value awal
        kenaWall = false;
        kenaTower = false;
        sudahExplode = false;

        //atur jarak bergerak
        //kamikaze kecil -> gerak lebih cepat
        // jarakBergerak = 0.009f;
        // jarakBergerak = 0.04f;
        // jarakBergerak = 0.012f;
        jarakBergerak = 0.007f;
    }

    // Update is called once per frame
    void Update()
    {
        // if(canvas.GetComponent<canvasGame>().isPaused == false){
        //     if (kenaTower == false && kenaWall == false)
        //     {
        //         // x -= jarakBergerak;
        //         // posisiKamikaze.x = x;
        //     }
        // }
    }

    private void FixedUpdate()
    {
        // if(canvas.GetComponent<canvasGame>().isPaused == false){
        //     //atur posisi
        //     //parent.transform.position = posisiKamikaze;
        //     transform.position = posisiKamikaze;
        // }

        if(canvas.GetComponent<canvasGame>().isPaused == false){
            if (kenaTower == false && kenaWall == false)
            {
                // x -= jarakBergerak;
                // posisiKamikaze.x = x;

                //gerak kamikaze
                float xforce = -1 * jarakBergerak * Time.deltaTime;
                Vector2 force = new Vector2(xforce, 0);
                kamikazeRigidBody.AddForce(force);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Collision: " + other.name);
        // if (ctrColidding == 0)
        // {
        //     ctrColidding = ctrColidding + 1;

        //     //cek apakah peluru mengenai walls / tower
        //     if (other.CompareTag("Walls"))
        //     {
        //         kenaWall = true;
        //         //kamikazeExplode();
        //         // Debug.Log("Kamikaze 1 kena wall. ctr : " + ctrColidding);
        //     }
        //     else if(other.CompareTag("Tower")){
        //         kenaTower = true;
        //         //kamikazeExplode();
        //         // Debug.Log("Kamikaze 1 kena tower. ctr : " + ctrColidding);
        //     }
        // }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
    }


    public void kamikazeExplode(){
        // if (! sudahExplode)
        // {
        //     sudahExplode = true;

        //     //buat coin
        //     GameObject tmpObj = Instantiate(coin);
        //     SpawnCoins.squares.Add(tmpObj);
        //     tmpObj.transform.position = posisiKamikaze;

        //     //munculkan explosion
        //     GameObject objExplosion = Instantiate(
        //         prefabsExplosion, posisiKamikaze, 
        //         Quaternion.identity
        //     );

        //     //atur parent explosion
        //     objExplosion.transform.parent = grupExplosion.transform;

        //     //kena damage
        //     GetComponent<EnemyBehaviour>().takeHit(20);
        // }
    }

    private void OnDestroy()
    {
        //waktu kamikaze dihancurkan, hancurkan juga parent
        //Destroy(transform.parent.gameObject);
    }
}
