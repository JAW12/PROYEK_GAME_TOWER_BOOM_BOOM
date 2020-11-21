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
    float jarakBergerak;

    void Start()
    {        
        //waktu gerak nanti yg berubah adalah posisi gameobject parent nya
        parent = gameObject.transform.parent;
        posisiKamikaze = parent.gameObject.transform.position;
        x = posisiKamikaze.x;

        //atur jarak bergerak
        if(gameObject.CompareTag("Kamikaze1")){
            //gerak lebih cepat
            jarakBergerak = 0.009f;
        }
        else if(gameObject.CompareTag("Kamikaze2")){
            //gerak lebih lambat
            jarakBergerak = 0.007f;
        }
        else{
            jarakBergerak = 0.007f;
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
}
