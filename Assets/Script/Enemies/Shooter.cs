using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject prefabs_peluru;

    public GameObject target;

    float delayTembak;
    bool tembak;
    Vector2 flip;

    // Start is called before the first frame update
    void Start()
    {
        // delayTembak = 30f;
        delayTembak = 90f;

        //untuk flip
        flip = transform.localScale;

        //untuk flip
        flip.x = -1;  //disesuaikan dengan prop scale x object nya
        transform.localScale = flip;
    }

    // Update is called once per frame
    void Update()
    {
        // setiap 30 detik dia nembak lagi, nembak lagi
        delayTembak = delayTembak - 1;
        if (delayTembak < 0)
        {
            //mencari posisi lokal scale dari simerah
            // kalo 1 artinya gerak ke kanan. kalo -1 gerak ke kiri
            float posisi = target.transform.localScale.x;

            // cek kalo player di kiri, dia nembak ke kiri
            // kalo player di kanan nembak ke kanan
            // kalo sudah dapat tinggal kirim ke parameter
            // if (posisi == 1)
            //     buatPeluru(-2);
            // else
            //     buatPeluru(2);

            //nembak peluru ke kiri
            buatPeluru(-3);

            delayTembak = 90f;
        }
    }

    public void buatPeluru(float paramDx)
    {
        Vector2 lokasiMuncul = new Vector2(transform.position.x - 0.4f, 
            transform.position.y);
        GameObject peluru_mc = Instantiate(prefabs_peluru, lokasiMuncul, transform.rotation);

        // setelah dibuat Instantiate baru diatur posisinya
        // paramdx = jarak antar peluru ? (aku krg tau)
        Vector2 posisi = new Vector2(2 * paramDx, 0);
        peluru_mc.GetComponent<PeluruShooter>().init(posisi);
        Destroy(peluru_mc, 3f);
    }
}
