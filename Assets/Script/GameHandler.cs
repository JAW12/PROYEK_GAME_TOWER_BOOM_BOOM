using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject prefabsKamikaze1, prefabsKamikaze2, prefabsShooter;
    public int stageWave;

    public bool modeTutorial;

    GameObject grupShooter, grupKamikaze1, grupKamikaze2;
    
    public Vector2 lokasiStartSpawn;

    //timer
    float detik;
    bool sedangJalan;

    // Start is called before the first frame update
    void Start()
    {
        //init grup
        grupShooter = GameObject.Find("GrupEnemies/GrupShooter");
        grupKamikaze1 = GameObject.Find("GrupEnemies/GrupKamikaze1");
        grupKamikaze2 = GameObject.Find("GrupEnemies/GrupKamikaze2");

        //lokasi start spawn : y nya disesuaikan dengan area peletakkan walls
        float x_start = (float)10;
        float y_start = (float)(-2.14);
        lokasiStartSpawn = new Vector2(x_start, y_start);

        //init variable stage
        stageWave = 3;
        modeTutorial = false;

        //deklarasi kondisi awal timer
        sedangJalan = true;
        detik = 0;

        //spawn musuh
        //spawnKamikaze1(2f, -1f);
        //spawnKamikaze1(3f, -1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //setiap 5 detik sekali spawn musuh
        if (sedangJalan)
        {
            if (detik > 0)
            {
                detik -= Time.deltaTime;
            }
            else
            {
                //spawn musuh
                spawnEnemiesByStageWave();                

                //reset ctr timer
                Debug.Log("spawn enemies!");
                detik = 5;
            }
        }
    }

    void spawnEnemiesByStageWave(){
        //cek stage
        if(stageWave >= 1){
            int jumlahKamikazeSpawned = 2 * stageWave;

            //max kamikaze yg bisa dispawn adalah 5
            if (jumlahKamikazeSpawned > 5)
            {
                jumlahKamikazeSpawned = 5;
            }
            spawnKamikazes(jumlahKamikazeSpawned);
        }
        
        //shooter baru muncul setelah stage 3
        if(stageWave >= 3){
            spawnEnemyByType("shooter");
        }

        //kalo sudah stage final (stage 5) baru bos muncul
        //selain bos, kamikaze dan shooter juga muncul
        if (stageWave == 5)
        {
            spawnBoss();
        }
    }

    void spawnKamikazes(int jumlahKamikazeSpawned){
        for (int i = 0; i < jumlahKamikazeSpawned; i++)
        {
            spawnEnemyByType("kamikaze1");
            spawnEnemyByType("kamikaze2");
        }
    }

    void spawnEnemyByType(string jenisMusuh){
        //untuk set lokasi spawn
        Vector2 lokasiSpawn = lokasiStartSpawn;

        //cek kalo shooter, x spawn lgsg muncul di layar        
        int ctr = 1;
        bool validSpawn = true;;
        if(jenisMusuh == "shooter"){
            ctr = grupShooter.transform.childCount + 1;
            double posX = 7.5 - ctr;

            if (posX > 4)
            {
                lokasiSpawn.x = (float)posX;
            }
            else{
                //memastikan supaya posisi x ga sampe nabrak area munculin walls
                validSpawn = false;
            }
        }
        else if(jenisMusuh == "kamikaze1"){
            //serang atas tembok
            lokasiSpawn.y = (float)(-1.6);
            ctr = grupKamikaze1.transform.childCount + 1;
        }
        else if(jenisMusuh == "kamikaze2"){
            //serang bawah tembok
            lokasiSpawn.y = (float)(-2.5);
            ctr = grupKamikaze2.transform.childCount + 1;
        }

        //kalo valid baru dispawn
        if (validSpawn)
        {
            //empty object untuk menampung prefabs
            GameObject empty = new GameObject();
            empty.transform.position = lokasiSpawn;

            //atur supaya empty object masuk ke grup musuh sesuai dengan jenisnya
            GameObject prefabsMusuh = prefabsKamikaze1;

            if(jenisMusuh == "kamikaze1"){            
                empty.transform.parent = grupKamikaze1.transform;
            }
            else if (jenisMusuh == "kamikaze2")
            {
                prefabsMusuh = prefabsKamikaze2;
                empty.transform.parent = grupKamikaze2.transform;
            }
            else if(jenisMusuh == "shooter"){
                prefabsMusuh = prefabsShooter;            
                empty.transform.parent = grupShooter.transform;
            }
        
            //ubah posisi anchor location dari prefabs yg mau dibuat
            GameObject objprefabs = Instantiate(
                prefabsMusuh, new Vector2(0f, 0f), 
                Quaternion.identity);
            
            //buat empty gameobject yg baru aja dibuat sbg parent dari prefabs musuh yg diinstantiate
            objprefabs.transform.parent = empty.transform;

            //untuk dimasukkan ke dalam dunia game
            empty.name = jenisMusuh + "_" + ctr + "_created";
            objprefabs.name = jenisMusuh + "_" + (empty.transform.childCount + 1);
        }
    }
    
    //untuk munculin boss
    void spawnBoss(){
        
    }
}
