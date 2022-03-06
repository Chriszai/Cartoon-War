using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundFire : MonoBehaviour
{
    // Start is called before the first frame update
    private float interval = 1f;
    private float count = 0;
    private int damage = 2;
    void Start()
    {
        Invoke("die", 4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (count <= interval)
        {
            count += Time.deltaTime;
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag.Equals("Player") && count >= interval)
        {
            count = 0;
            other.SendMessage("BeAttack", damage);
        }
    }
    public void die()
    {
        Destroy(this.gameObject);
    }
}
