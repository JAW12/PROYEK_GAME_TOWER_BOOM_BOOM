using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeluruShooter : MonoBehaviour
{
    float kecepatan = 0.6f;

    public void init(Vector2 posisi)
    {
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        GetComponent<Rigidbody2D>().mass = 1f;

        GetComponent<Rigidbody2D>().AddForce(posisi * kecepatan, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Walls"))
        {
            GameObject wall = collision.gameObject;
            //target.GetComponent<SiMerah2>().kenaSerang2();

            //hancurkan prefabs peluru
            Destroy(gameObject);
        }
    }
}
