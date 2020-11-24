using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : MonoBehaviour
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
    bool kenaWall, kenaTower;

    void Start()
    {        
        //deklarasi
        grupExplosion = GameObject.Find("GrupEnemies/GrupExplosion");

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

        //atur jarak bergerak
        if(gameObject.CompareTag("Kamikaze1")){
            //kamikaze kecil -> gerak lebih cepat
            // jarakBergerak = 0.009f;
            // jarakBergerak = 0.03f;
            jarakBergerak = 0.012f;
        }
        else if(gameObject.CompareTag("Kamikaze2")){
            //kamikaze panjang -> gerak lebih lambat
            // jarakBergerak = 0.007f;
            // jarakBergerak = 0.02f;
            jarakBergerak = 0.009f;
        }
        else{
            jarakBergerak = 0f;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        //kurangi posisi
        x -= jarakBergerak;
        posisiKamikaze.x = x;
    }

    private void FixedUpdate()
    {
        //atur posisi
        parent.transform.position = posisiKamikaze;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //cek apakah peluru mengenai walls
        if (other.CompareTag("Walls"))
        {
            kenaWall = true;
            kamikazeExplode();
            Debug.Log("Kamikaze kena wall");
        }
        else if(other.CompareTag("Tower")){
            kenaTower = true;
            kamikazeExplode();
            Debug.Log("Kamikaze kena tower");
        }
    }

    private void kamikazeExplode(){
        //munculkan explosion
        GameObject objExplosion = Instantiate(
        prefabsExplosion, posisiKamikaze, 
        Quaternion.identity);

        //atur parent explosion
        objExplosion.transform.parent = grupExplosion.transform;

        //langsung hancurkan parent empty gameobject dr prefabs saat ini
        Destroy(gameObject.transform.parent.gameObject);
    }
}
