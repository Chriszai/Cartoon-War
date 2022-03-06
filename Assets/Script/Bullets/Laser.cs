using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour

{
    public int damage = 1;
    private float interval = 0.01f;
    private float count = 0.01f;
    private bool isBigger;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        isBigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (count <= interval)
        {
            count += Time.deltaTime;
        }
        if (count >= interval && transform.localScale.y < 0.45f && isBigger)
        {
            transform.localScale += new Vector3(0, 0.05f, 0);
            if (transform.localScale.y >= 0.45)
            {
                isBigger = false;
            }
            count = 0;
        }
        if (count >= interval && isBigger == false)
        {
            transform.localScale -= new Vector3(0, 0.05f, 0);
            if (transform.localScale.y <= 0)
            {
                Destroy(this.gameObject);
            }
            count = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag.Equals("wall") || collision.tag.Equals("door_0") || collision.tag.Equals("door_0"))
        {
            Destroy(this.gameObject);
        }
        if (collision.tag.Equals("monster"))
        {
            collision.SendMessage("BeAttack", damage);
        }
    }
}
