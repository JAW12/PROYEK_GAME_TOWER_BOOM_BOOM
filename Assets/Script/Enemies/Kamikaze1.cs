using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze1 : MonoBehaviour
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
    bool damage = true;

    bool sudahExplode;

    private int ctrColidding = 0;

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
        sudahExplode = false;

        //atur jarak bergerak
        //kamikaze kecil -> gerak lebih cepat
        // jarakBergerak = 0.009f;
        // jarakBergerak = 0.03f;
        jarakBergerak = 0.012f;
    }

    // Update is called once per frame
    void Update()
    {
        //kurangi posisi kl masih blm meledak
        if (kenaTower == false && kenaWall == false)
        {
            x -= jarakBergerak;
            posisiKamikaze.x = x;
        }
    }

    private void FixedUpdate()
    {
        //atur posisi
        parent.transform.position = posisiKamikaze;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collision: " + other.name);
        if (ctrColidding == 0)
        {
            ctrColidding = ctrColidding + 1;

            //cek apakah peluru mengenai walls / tower
            if (other.CompareTag("Walls"))
            {
                kenaWall = true;
                kamikazeExplode(other);
                // Debug.Log("Kamikaze 1 kena wall. ctr : " + ctrColidding);
            }
            else if(other.CompareTag("Tower")){
                kenaTower = true;
                kamikazeExplode(other);
                // Debug.Log("Kamikaze 1 kena tower. ctr : " + ctrColidding);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(ctrColidding == 1){
            //ctrColidding = 0;
        }
    }


    private void kamikazeExplode(Collider2D other){
        if (! sudahExplode)
        {
            sudahExplode = true;

            //hancurkan kamikaze
            Destroy(gameObject);

            //munculkan explosion
            GameObject objExplosion = Instantiate(
                prefabsExplosion, posisiKamikaze, 
                Quaternion.identity
            );

            //atur parent explosion
            objExplosion.transform.parent = grupExplosion.transform;
        }
        

        if(other != null){
            if(damage){
                StartCoroutine (WaitForSeconds());
                other.gameObject.GetComponent<diserang>().attacked(1.25f);
            }
        }
    }

    IEnumerator WaitForSeconds()
    {
        damage = false;
        yield return new WaitForSecondsRealtime (3);    
        damage = true;
    }

    private void OnDestroy()
    {
        //waktu kamikaze dihancurkan, hancurkan juga parent
        Destroy(transform.parent.gameObject);
    }
}
