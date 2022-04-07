using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cap2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject boss_1;

    void Start()
    {
        Invoke("die", 5f);
        boss_1 = GameObject.Find("boss_1/boss_1_body");

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = boss_1.transform.position;
    }
    public void die()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("wall") || collision.tag.Equals("door_0") || collision.tag.Equals("door_0"))
        {
            Destroy(this.gameObject);
        }
        if (collision.tag.Equals("bullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
