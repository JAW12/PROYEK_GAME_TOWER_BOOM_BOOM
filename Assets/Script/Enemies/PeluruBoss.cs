using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeluruBoss : MonoBehaviour
{
    float kecepatan = 2f;

    public void init(Vector2 posisi)
    {
        //atur kecepatan peluru
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        GetComponent<Rigidbody2D>().mass = 1f;

        GetComponent<Rigidbody2D>().AddForce(posisi * kecepatan, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Walls")){
            Destroy(collision.gameObject);
        }
    }

    // IEnumerator WaitForSeconds()
    // {
    //     yield return new WaitForSecondsRealtime (3);
    // }
}
