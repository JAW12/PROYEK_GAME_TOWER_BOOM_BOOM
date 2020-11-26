using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //explosion
    public GameObject prefabsExplosion;

    GameObject canvasGame, grupExplosion;
    // GameObject damageAOE;

    Rigidbody2D rigidBodyBomb;
    Animator animator;
    CircleCollider2D circleColliderBomb;

    bool isDropped;
    bool hasExploded;

    float jarakAtasGame;
    float bombDamage;
    float bombAoeRadius;

    //public float fallSpeed = 80.0f;

    // Start is called before the first frame update
    void Start()
    {
        canvasGame = GameObject.Find("Canvas");
        grupExplosion = GameObject.Find("GrupEnemies/GrupExplosion");

        //komponen
        rigidBodyBomb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        circleColliderBomb = GetComponent<CircleCollider2D>();
        
        //deklarasi value awal
        isDropped = false;
        hasExploded = false;
        bombDamage = canvasGame.GetComponent<canvasGame>().bombDamage;
        bombAoeRadius = canvasGame.GetComponent<canvasGame>().bombAOE * 0.03f;

        //atur radius bom
        circleColliderBomb.radius = bombAoeRadius;

        //offset y
        float offsetY = bombAoeRadius - 0.15f;
        circleColliderBomb.offset = new Vector2(0f, offsetY);

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
        else{
            dropBomb();
        }
    }

    private void FixedUpdate()
    {        
        if(isDropped){
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        //kalo bom sudah jatuh ke tanah dia meledak
        bombExplode();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        explosionDamage();
    }

    private void cekMouseClicked(){
        //klik kiri mouse global
        if(Input.GetMouseButtonUp(0)){
            // mendapatkan posisi mouse 
            Vector2 mousePos;
            // posisi mouse diconvert sesuai menjadi posisi di dunia (main camera)
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //cek supaya pas klik shop bomb nya ga di drop
            bool sedangMengaturBom = canvasGame.GetComponent<canvasGame>().sedangMengaturBom;
            if (sedangMengaturBom)
            {
                //Debug.Log("pos mouse y : " + mousePos.y);
                float batasAtas = jarakAtasGame - 2f;

                if (mousePos.y <= batasAtas)
                {
                    isDropped = true;
                }
                
            }
        }
    }

    private void dropBomb(){
        //jatuhin bom
        rigidBodyBomb.isKinematic = false;

        //atur supaya bisa beli bom lagi
        canvasGame.GetComponent<canvasGame>().sedangMengaturBom = false;

        //Debug.Log("bomb dropped");
    }

    private void bombExplode(){
        hasExploded = true;

        //munculkan explosion
        GameObject objExplosion = Instantiate(
        prefabsExplosion, gameObject.transform.position, 
        Quaternion.identity);

        //atur ukuran scale explosion
        objExplosion.transform.localScale = new Vector3(4f, 4f, 4f);

        //atur parent explosion
        objExplosion.transform.parent = grupExplosion.transform;

        //langsung hancurkan parent empty gameobject dr prefabs saat ini
        Destroy(gameObject.transform.parent.gameObject);
    }

    void explosionDamage()
    {
        var hitColliders = Physics2D.OverlapCircleAll(transform.position, bombAoeRadius);
        foreach (var hitCollider in hitColliders)
        {
            //splash damage : https://youtu.be/6YvEDbBjgAY
            //kalo berada di episentrum bakalan dapat damage terbesar
            var enemy = hitCollider.GetComponent<EnemyBehaviour>();
            if (enemy)
            {
                var closestPoint = hitCollider.ClosestPoint(transform.position);
                var distance = Vector3.Distance(closestPoint, transform.position);

                var damagePercent = Mathf.InverseLerp(bombAoeRadius, 0, distance);
                enemy.takeHit(damagePercent * bombDamage);
            }
        }
    }
    
}
