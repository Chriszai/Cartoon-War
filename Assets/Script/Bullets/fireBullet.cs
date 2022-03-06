using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBullet : MonoBehaviour
{
    // Start is called before the first frame update

    public int damage = 2;

    public GameObject importEffect;
    public GameObject player;
    public Vector3 movedir = Vector3.right;
    void Start()
    {
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -0.03f, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("wall") || collision.tag.Equals("door_0") || collision.tag.Equals("door_0"))
        {
            Vector3 pos = collision.transform.position;
            GameObject effect = Instantiate(importEffect, pos, transform.rotation);
        }
        if (collision.tag.Equals("monster"))
        {
            Vector3 pos = collision.transform.position;
            GameObject effect = Instantiate(importEffect, pos, transform.rotation);
            collision.SendMessage("BeAttack", damage);
        }
    }
}
