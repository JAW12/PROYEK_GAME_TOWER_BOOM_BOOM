using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class canvasGame : MonoBehaviour
{    
    public int shop = -1;
    // -1 -> ga pegang apa - apa
    // 0 -> wooden wall
    // 1 -> small stone wall
    // 2 -> large stone wall
    // 3 -> bomb

    public int wallLvl = 0;

    //bom
    public GameObject prefabsBom, grupBomb;
    public int bombLvl, bombPrice, bombDamage;
    public float bombAOE;
    public bool sedangMengaturBom;
    public TextMeshProUGUI textHP;
    public GameObject health;
    public TextMeshProUGUI textBombDamage, textBombAOE;
    public Button btnBuyBomb, btnUpgradeBomb;

    //stage dan game handler
    public int stage;
    public TextMeshProUGUI textStage;
    public GameObject gameHandler;

    public TextMeshProUGUI textCoin;
    public Button btnBuyWall;
    public Button btnUpgradeWall;

    public Sprite imgBuySmallStone;
    public Sprite imgBuyLargeStone;

    public Sprite imgUpStone;
    public Sprite imgMaxStone;

    public GameObject panelCancel;

    public bool isPaused = false;
    // Start is called before the first frame update
    public void pause(){
        if(isPaused){
            Time.timeScale = 1;
            isPaused = false;
        }
        else{
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    public void setPanelTextStatusGame(){
        //status bom
        textBombDamage.text = bombDamage.ToString();
        textBombAOE.text = bombAOE.ToString();     

        //status stage
        textStage.text = "Stage #" + stage.ToString();
    }

    public void buyWall(){
        if(shop < 0 || shop > 2){
            int coin = int.Parse(textCoin.text);
            if(wallLvl == 0){
                if(coin >= 5){
                    coin -= 5;
                    shop = wallLvl;
                    panelCancel.SetActive(true);
                }
            }
            else if(wallLvl == 1){
                if(coin >= 7){
                    coin -= 7;
                    shop = wallLvl;
                    panelCancel.SetActive(true);
                }
            }
            else if(wallLvl == 2){
                if(coin >= 10){
                    coin -= 10;
                    shop = wallLvl;
                    panelCancel.SetActive(true);
                }
            }
            textCoin.text = coin.ToString();
        }
    }

    public void upgradeWall(){
        int coin = int.Parse(textCoin.text);
            if(wallLvl == 0){
                if(coin >= 10){
                    coin -= 10;
                    wallLvl = 1;
                    btnBuyWall.GetComponent<Image>().sprite = imgBuySmallStone;
                    btnUpgradeWall.GetComponent<Image>().sprite = imgUpStone;
                }
            }
            else if(wallLvl == 1){
                if(coin >= 10){
                    coin -= 10;
                    wallLvl = 2;
                    btnBuyWall.GetComponent<Image>().sprite = imgBuyLargeStone;
                    btnUpgradeWall.GetComponent<Image>().sprite = imgMaxStone;
                }
            }
            textCoin.text = coin.ToString();
    }

    public void cancel(){
        int coin = int.Parse(textCoin.text);
        if(shop == 0){
            coin += 5;
        }
        else if(shop == 1){
            coin += 7;
        }
        else if(shop == 2){
            coin += 10;
        }
        else if(shop == 3){
            //gajadi beli bom
            coin += 10;
            sedangMengaturBom = false;

            //hancurkan bom
            for (int i = 0; i < grupBomb.transform.childCount; i++)
            {
                Destroy(grupBomb.transform.GetChild(i).gameObject);
            }
        }

        textCoin.text = coin.ToString();
        shop = -1;
        panelCancel.SetActive(false);
    }

    void initValueAwal(){
        //stage
        stage = 1;

        //bom
        bombLvl = 1;
        bombPrice = 10;
        bombDamage = 10;
        bombAOE = 2f;
        sedangMengaturBom = false;

        //atur text
        setPanelTextStatusGame();

        Debug.Log("init value");
    }

    public void buyBomb(){
        //harga bom = 10
        shop = 3;
        int coin = int.Parse(textCoin.text);
        //cek supaya ga beli multiple bom dlm 1x transaksi
        if (! sedangMengaturBom)
        {
            if (coin >= bombPrice)
            {
                //kurangi coin kalo coin cukup
                coin -= bombPrice;
                textCoin.text = coin.ToString();

                sedangMengaturBom = true;
                makeBomb();      

                //aktifkan panel berisi button cancel
                panelCancel.SetActive(true);
            }
        }            
    }

    private void makeBomb(){
        //empty object untuk menampung prefabs
        GameObject empty = new GameObject();
        empty.transform.position = new Vector2(0f, 0f);

        //buat bomb
        GameObject objprefabs = Instantiate(
            prefabsBom, new Vector2(0f, 0f), 
            Quaternion.identity);
        
        //atur parent group
        empty.transform.parent = grupBomb.transform;
        objprefabs.transform.parent = empty.transform;

        //atur name waktu dimasukkan ke world
        int ctr = grupBomb.transform.childCount + 1;
        empty.name = "bomb" + ctr + "_created";
        
        //atur posisi bomb sesuai dengan posisi mouse
        objprefabs.GetComponent<Bomb>().aturPosisi();
    }

    public void upgradeBomb(){
        int coin = int.Parse(textCoin.text);

        //hanya bisa upgrade kalo ga lagi ngatur bom dan level bom belum maxed out
        if (! sedangMengaturBom && bombLvl < 5){
            if (coin >= 10)
            {
                //upgrade damage dan AOE
                bombDamage += 10;
                bombAOE += 2f;

                //kurangi coin kalo coin cukup
                coin -= 10;

                //atur text
                textCoin.text = coin.ToString();
                setPanelTextStatusGame();
            }
        }        
    }

    // Start is called before the first frame update
    void Start()
    {
        initValueAwal();
        gameHandler.GetComponent<GameHandler>().setKondisiAwalGame();
        
        panelCancel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("stage : " + stage);
    }
}
