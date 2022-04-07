using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom_0 : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 2;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void die()
    {
        Destroy(this.gameObject);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.SendMessage("BeAttack", damage);
        }
    }
}
