using UnityEngine;
using System.Collections;

public class AsteroidControl : MonoBehaviour
{

    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }


    void Update()
    {
        rb2D.velocity = rb2D.velocity.normalized * 2f;
    }
}