using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), other.gameObject.GetComponent<BoxCollider2D>(), false);
    }
}
