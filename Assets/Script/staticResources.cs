using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticResources : MonoBehaviour
{
    
    public Sprite imageEnable;
    public Sprite imageDisable;

    private static staticResources _instance;

    void Start() {
        _instance = this;
    }

    public static staticResources newInstance(){
        return new staticResources();
    }

    public static staticResources Instance() {
        return _instance;
    }

    public Sprite getSprite(string get)
    {
        if(get == "sound_enable"){
            return imageEnable;
        }
        else if(get == "sound_disable"){
            return imageDisable;
        }
        else{
            return null;
        }
    }
}
