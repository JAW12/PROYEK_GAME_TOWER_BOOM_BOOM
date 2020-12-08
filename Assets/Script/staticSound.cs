using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticSound : MonoBehaviour
{
    private static staticSound _instance ;

    void Awake()
    {
        // //if we don't have an [_instance] set yet
        // if(!_instance)
        //     _instance = this ;
        // //otherwise, if we do, kill this thing
        // else
        //     Destroy(this.gameObject) ;


        DontDestroyOnLoad(this.gameObject) ;
    }
}
