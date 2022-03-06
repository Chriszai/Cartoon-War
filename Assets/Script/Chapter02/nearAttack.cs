using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nearAttack : MonoBehaviour
{
    private float interval = 0.8f;
    private float count = 0;
    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, -1f);
        if (count <= interval)
        {
            count += Time.deltaTime;
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (count >= interval)
            {
                collision.SendMessage("BeAttack", damage);
                count = 0;
            }

        }
    }
}
