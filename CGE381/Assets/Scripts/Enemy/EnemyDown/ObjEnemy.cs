using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjEnemy : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] public float speedDown;
    // Start is called before the first frame update
    void Start()
    {
        this.AddComponent<PolygonCollider2D>();
        rb = this.AddComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, speedDown);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
