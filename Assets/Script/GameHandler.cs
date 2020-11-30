using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameHandler : MonoBehaviour
{
    /* == KONSEP STAGE WAVE GAME == 
     * tiap stage (selain stage boss) masing2 punya 5 wave sebelum dia ganti jd next stage
     * kalo stage boss wave nya jalan terus
    */

    public GameObject canvasGame;
    
    public GameObject prefabsKamikaze1, prefabsKamikaze2, prefabsShooter;
    public int stage, stageWave, maxStageWave;

    GameObject grupShooter, grupKamikaze1, grupKamikaze2;

    //boss
    GameObject grupboss,laserBoss;

    public Vector2 lokasiStartSpawn;

    //timer
    float detik;
    float jarakWaktuNextWave;
    bool sedangJalan;

    //stage difficulty
    int maxJumlahKamikaze;
    int minJumlahKamikaze;

    bool cekMunculBoss = false;

    //tutorial
    public bool modeTutorial;
    public static bool cekBeliWall = false, cekBeliBomb = false;

    // Start is called before the first frame update
    void Start()
    {
        //init grup
        grupShooter = GameObject.Find("GrupEnemies/GrupShooter");
        grupKamikaze1 = GameObject.Find("GrupEnemies/GrupKamikaze1");
        grupKamikaze2 = GameObject.Find("GrupEnemies/GrupKamikaze2");
        grupboss = GameObject.Find("GrupEnemies/Boss");
        laserBoss = GameObject.Find("GrupEnemies/LaserBoss");
        grupboss.SetActive(false);
        laserBoss.SetActive(false);
        setKondisiAwalGame();
    }

    public void setKondisiAwalGame(){
        //lokasi start spawn : y nya disesuaikan dengan area peletakkan walls
        float x_start = 10f;
        float y_start = (float)(-2.14);
        lokasiStartSpawn = new Vector2(x_start, y_start);

        //init variable stage
        stage = canvasGame.GetComponent<canvasGame>().stage;
        stageWave = 1;
        maxStageWave = 10;
        jarakWaktuNextWave = 10;
        //modeTutorial = false;

        //deklarasi kondisi awal timer
        sedangJalan = true;
        detik = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (modeTutorial == false)
        {
            //setiap beberapa detik sekali spawn musuh (detik sesuai jarakWaktuNextWave)
            if (sedangJalan)
            {
                setStageDifficulty();
                
                if (detik > 0)
                {
                    detik -= Time.deltaTime;
                }
                else
                {
                    //spawn musuh
                    spawnEnemiesByStageWave();                

                    //reset ctr timer
                    //Debug.Log("spawn enemies!");
                    detik = jarakWaktuNextWave;
                }
            }
        }
        else{
            if (sedangJalan && cekBeliWall)
            {
                spawnKamikazes(1);
                cekBeliWall = false;
            }
            else if(sedangJalan && cekBeliBomb){
                spawnEnemyByType("shooter");
                cekBeliBomb = false;
            }
        }
    }

    void setStageDifficulty(){
        //cek jumlah max kamikaze yang bisa muncul
        //kalo di bawah stage 3, jumlah max yang bisa muncul cuma 5
        maxJumlahKamikaze = 7;
        minJumlahKamikaze = 2;

        if(stage == 1){
            minJumlahKamikaze = 2;
            maxJumlahKamikaze = 3;

            jarakWaktuNextWave = 10;
        }
        else if(stage >= 2 && stage <= 3){
            minJumlahKamikaze = 4;
            maxJumlahKamikaze = 6;

            jarakWaktuNextWave = 5;
        }
        else{
            jarakWaktuNextWave = 5;
        }
    }

    void spawnEnemiesByStageWave(){
        //cek stage
        if(stage >= 1){
            int jumlahKamikazeSpawned = stageWave / 2 + stage;

            //atur minimal jumlah kamikaze dispawn
            if(jumlahKamikazeSpawned < minJumlahKamikaze){
                jumlahKamikazeSpawned = minJumlahKamikaze;
            }
            
            //atur jumlah max kamikaze yg bisa dispawn
            if (jumlahKamikazeSpawned > maxJumlahKamikaze)
            {
                jumlahKamikazeSpawned = maxJumlahKamikaze;
            }
            spawnKamikazes(jumlahKamikazeSpawned);
        }
        
        //shooter baru muncul setelah stage 3
        if(stage >= 3){
            spawnEnemyByType("shooter");
        }

        //kalo sudah stage final (stage 5) baru bos muncul
        //selain bos, kamikaze dan shooter juga muncul
        if (stage == 5 && !cekMunculBoss)
        {
            cekMunculBoss = true;
            spawnBoss();
        }

        //testing bos langsung muncul
        //spawnBoss();

        //cek penambahan stage
        stageWave++;
        if(stage <= 4 && stageWave > maxStageWave){
            //cek berganti kalo sudah terjadi 5 wave di dalam stage tsb
            stage++;
            stageWave = 1;

            //atur text stage
            canvasGame.GetComponent<canvasGame>().stage = stage;
            canvasGame.GetComponent<canvasGame>().setPanelTextStatusGame();
        }
        else{
            stageWave = maxStageWave;
        }
    }

    void spawnKamikazes(int jumlahKamikazeSpawned){
        //JANGAN DI FOR
        // for (int i = 0; i < jumlahKamikazeSpawned; i++)
        // {
        //     spawnEnemyByType("kamikaze1");
        //     spawnEnemyByType("kamikaze2");
        // }
        spawnEnemyByType("kamikaze1");
        spawnEnemyByType("kamikaze2");
    }

    void spawnEnemyByType(string jenisMusuh){
        //untuk set lokasi spawn
        Vector2 lokasiSpawn = lokasiStartSpawn;

        //cek kalo shooter, x spawn lgsg muncul di layar        
        int ctr = 1;
        bool validSpawn = true;
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
            //GameObject empty = new GameObject();
            //empty.transform.position = lokasiSpawn;

            //atur supaya empty object masuk ke grup musuh sesuai dengan jenisnya
            GameObject prefabsMusuh;
            GameObject objprefabs;

            //ubah posisi anchor location dari prefabs yg mau dibuat
            // objprefabs = Instantiate(
            //     prefabsMusuh, new Vector2(0f, 0f), 
            //     Quaternion.identity);
            

            if(jenisMusuh == "kamikaze1"){            
                prefabsMusuh = prefabsKamikaze1;
                objprefabs = Instantiate(
                    prefabsMusuh, lokasiSpawn, 
                    Quaternion.identity);

                //set kondisi awal
                 objprefabs.GetComponent<Kamikaze1>()
                    .setKondisiAwalKamikaze();

                //untuk dimasukkan ke dalam dunia game
                //empty.name = jenisMusuh + "_" + ctr + "_created";
                //objprefabs.name = jenisMusuh + "_" + (empty.transform.childCount + 1);
                objprefabs.name = jenisMusuh + "_" + ctr + "_created";

                //parent
                objprefabs.transform.parent = grupKamikaze1.transform;

                //empty.transform.parent = grupKamikaze2.transform;
            }
            else if (jenisMusuh == "kamikaze2")
            {
                prefabsMusuh = prefabsKamikaze2;
                objprefabs = Instantiate(
                    prefabsMusuh, lokasiSpawn, 
                    Quaternion.identity);

                //set kondisi awal
                 objprefabs.GetComponent<Kamikaze2>()
                    .setKondisiAwalKamikaze();

                //untuk dimasukkan ke dalam dunia game
                //empty.name = jenisMusuh + "_" + ctr + "_created";
                //objprefabs.name = jenisMusuh + "_" + (empty.transform.childCount + 1);
                objprefabs.name = jenisMusuh + "_" + ctr + "_created";

                //parent
                objprefabs.transform.parent = grupKamikaze2.transform;

                //empty.transform.parent = grupKamikaze2.transform;
            }
            else if(jenisMusuh == "shooter"){
                prefabsMusuh = prefabsShooter;
                objprefabs = Instantiate(
                    prefabsMusuh, lokasiSpawn, 
                    Quaternion.identity);    

                //untuk dimasukkan ke dalam dunia game
                //empty.name = jenisMusuh + "_" + ctr + "_created";
                //objprefabs.name = jenisMusuh + "_" + (empty.transform.childCount + 1);
                objprefabs.name = jenisMusuh + "_" + ctr + "_created";

                //parent
                objprefabs.transform.parent = grupShooter.transform;
                
                //empty.transform.parent = grupShooter.transform;
            }

            //buat empty gameobject sbg parent dari prefabs musuh yg diinstantiate
            //objprefabs.transform.parent = empty.transform;
        }
    }
    
    //untuk munculin boss
    void spawnBoss(){
        grupboss.SetActive(true);
        EnemyBehaviour.cekStartTimer = 2;
    }
}
