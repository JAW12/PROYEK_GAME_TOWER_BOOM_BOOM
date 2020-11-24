using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class diserang : MonoBehaviour
{
    public float hp;
    public int jenis;
    // 0 -> wooden wall
    // 1 -> stone wall
    // 2 -> large stone wall
    // 3 -> tower
    
    public GameObject prefabsExplosion;
    GameObject grupExplosion;
    GameObject canvas;

    private void Start() {
        grupExplosion = GameObject.Find("GrupEnemies/GrupExplosion");
        canvas = GameObject.Find("Canvas");
    }

    void Update(){
        if(hp <= 0 && jenis >= 0 && jenis <= 2){
            wallDestroyed();
        }
    }

    public void attacked(float damage){
        hp -= damage;
        if(jenis == 3){
            canvas.GetComponent<canvasGame>().textHP.text = hp.ToString() + "/100";
        }
    }

    private void wallDestroyed(){
        //munculkan explosion
        GameObject objExplosion = Instantiate(
        prefabsExplosion, gameObject.transform.position, 
        Quaternion.identity);

        //atur parent explosion
        objExplosion.transform.parent = grupExplosion.transform;

        Destroy(gameObject.transform.parent.gameObject);
    }
}
