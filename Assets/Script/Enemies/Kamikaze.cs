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
    bool kenaWalls;

    void Start()
    {        
        //waktu gerak nanti yg berubah adalah posisi gameobject parent nya
        parent = gameObject.transform.parent;
        posisiKamikaze = parent.gameObject.transform.position;
        x = posisiKamikaze.x;

        //deklarasi value awal
        kenaWalls = false;

        //atur jarak bergerak
        if(gameObject.CompareTag("Kamikaze1")){
            //kamikaze kecil -> gerak lebih cepat
            // jarakBergerak = 0.009f;
            // jarakBergerak = 0.03f;
            jarakBergerak = 0.01f;
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
        if(! kenaWalls){
            
        }        
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
            kenaWalls = true;

            //munculkan explosion
            GameObject objExplosion = Instantiate(
            prefabsExplosion, posisiKamikaze, 
            Quaternion.identity);

            //langsung hancurkan parent empty gameobject dr prefabs saat ini
            Destroy(gameObject.transform.parent.gameObject);

            //hancurkan explosion setelah beberapa saat
            Destroy(objExplosion, 2f);
        }
    }

   

}
