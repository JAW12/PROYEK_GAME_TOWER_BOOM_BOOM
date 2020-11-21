using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject prefabsKamikaze1, prefabsKamikaze2, prefabsShooter;

    // Start is called before the first frame update
    void Start()
    {
        spawnKamikaze1(2f, -1f);
        spawnKamikaze1(3f, -1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawn(){

    }

    void spawnKamikaze1(float x_spawn, float y_spawn){
        //untuk random lokasi spawn
        Vector2 lokasiSpawn = new Vector2(x_spawn, y_spawn);

        GameObject empty = new GameObject();
        empty.transform.position = lokasiSpawn;

        //ubah posisi anchor location
        GameObject obj = Instantiate(prefabsKamikaze1, new Vector2(0f, 0f), 
            Quaternion.identity);

        //untuk dimasukkan ke dalam dunia game
        empty.name = "Kamikaze1_created";
        obj.name = "obj" + (empty.transform.childCount + 1);
        
        //buat empty gameobject yg baru aja dibuat sbg parent dari kamikaze yg digenerate
        obj.transform.parent = empty.transform;
        
        Debug.Log("new kamikaze 1 created : " + x_spawn);
    }
    
}
