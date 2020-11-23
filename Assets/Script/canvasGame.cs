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

    public TextMeshProUGUI textCoin;
    public Button btnBuyWall;
    public Button btnUpgradeWall;

    public Sprite imgBuySmallStone;
    public Sprite imgBuyLargeStone;

    public Sprite imgUpStone;
    public Sprite imgMaxStone;

    public GameObject panelCancel;

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
        textCoin.text = coin.ToString();
        shop = -1;
        panelCancel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        panelCancel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
