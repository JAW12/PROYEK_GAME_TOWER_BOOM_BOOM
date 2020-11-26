using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //langsung play sound effect explosion setelah prefabs ini dibuat
        GetComponent<SoundEffect>().playSound(0, false, 0.2f);

        //hancurkan explosion setelah beberapa saat
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
