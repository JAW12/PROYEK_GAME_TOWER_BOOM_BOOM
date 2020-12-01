using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeMenu : MonoBehaviour
{
    public void close(){
        gameObject.GetComponent<Animator>().SetBool("open", false);
    }
}
