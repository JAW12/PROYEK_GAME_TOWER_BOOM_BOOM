using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeluruShooter : MonoBehaviour
{
    float kecepatan = 0.6f;
    bool kenaWall, kenaTower;

    public void init(Vector2 posisi)
    {
        //deklarasi value awal
        kenaTower = false;
        kenaWall = false;

        //atur kecepatan peluru
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        GetComponent<Rigidbody2D>().mass = 1f;

        GetComponent<Rigidbody2D>().AddForce(posisi * kecepatan, ForceMode2D.Impulse);
    }

    bool damage = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Walls"))
        {
            //set bool
            kenaWall = true;
            
            //tembok yg kena peluru
            GameObject wall = collision.gameObject;

            //hancurkan prefabs peluru
            Destroy(gameObject);
            if(collision != null && collision.gameObject != null){
                if(damage){
                    StartCoroutine (WaitForSeconds());
                    collision.gameObject.GetComponent<diserang>().attacked(1);
                }
            }
        }
        else if(collision.CompareTag("Tower")){
            //set bool
            kenaTower = true;

            //tower yang kena peluru
            GameObject tower = collision.gameObject;

            //hancurkan prefabs peluru
            Destroy(gameObject);   
            if(collision != null && collision.gameObject != null){
                if(damage){
                    StartCoroutine (WaitForSeconds());
                    collision.gameObject.GetComponent<diserang>().attacked(1f);
                }
            }         
        }
    }

    IEnumerator WaitForSeconds()
    {
        damage = false;
        yield return new WaitForSecondsRealtime (3);
        damage = true;
    }
}
