using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //explosion
    public GameObject prefabsExplosion;

    GameObject canvasGame, grupExplosion;
    GameObject damageAOE;

    Rigidbody2D rigidBodyBomb;
    Animator animator;

    bool isDropped;
    float jarakAtasGame;
    int bombDamage;

    // Start is called before the first frame update
    void Start()
    {
        canvasGame = GameObject.Find("Canvas");
        grupExplosion = GameObject.Find("GrupEnemies/GrupExplosion");

        rigidBodyBomb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        //deklarasi value awal
        isDropped = false;
        bombDamage = canvasGame.GetComponent<canvasGame>().bombDamage;

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

            dropBomb();
        }
    }

    public void aturPosisi(){
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
            // mendapatkan posisi mouse 
            Vector2 mousePos;
            // posisi mouse diconvert sesuai menjadi posisi di dunia (main camera)
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //cek supaya pas klik shop bomb nya ga di drop
            if (mousePos.y > 1f)
            {
                isDropped = true;
            }
        }
    }

    private void dropBomb(){
        bombExplode();

        //atur supaya bisa beli bom lagi
        canvasGame.GetComponent<canvasGame>().sedangMengaturBom = false;
    }

    private void bombExplode(){
        //munculkan explosion
        GameObject objExplosion = Instantiate(
        prefabsExplosion, gameObject.transform.position, 
        Quaternion.identity);

        //atur parent explosion
        objExplosion.transform.parent = grupExplosion.transform;

        //langsung hancurkan parent empty gameobject dr prefabs saat ini
        Destroy(gameObject.transform.parent.gameObject);
    }

    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            GameObject hitGameObject = hitCollider.gameObject;
        }
    }
    
}
