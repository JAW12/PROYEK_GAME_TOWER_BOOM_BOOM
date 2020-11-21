using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Rigidbody2D rigidBodyBomb;

    Animator animator;

    bool isDropped;
    float jarakAtasGame;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyBomb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        isDropped = false;

        //jarak spawn bom dengan menu atas
        jarakAtasGame = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        cekMouseClicked();
        if(! isDropped){
            aturPosisi();
        }
        
    }

    private void FixedUpdate()
    {        
        if(isDropped){
            //kalikan desimal buat slow down falling speed
            rigidBodyBomb.velocity = rigidBodyBomb.velocity * 0.9f;
        }
    }

    private void aturPosisi(){
        // mendapatkan posisi mouse 
        Vector2 mousePos;
        // posisi mouse diconvert sesuai menjadi posisi di dunia (main camera)
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //atur lokasi
        gameObject.transform.localPosition = new Vector2(mousePos.x, jarakAtasGame);
    }

    private void OnMouseDown()
    {
        //isDropped = true;
    }

    private void cekMouseClicked(){
        //klik kiri mouse global
        if(Input.GetMouseButtonUp(0)){
            isDropped = true;
            Debug.Log("left mouse clicked");
        }
    }
}
