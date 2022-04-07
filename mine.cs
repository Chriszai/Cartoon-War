using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mine : MonoBehaviour
{
    public int damage = 2;
    public Vector3 movePos;
    public GameObject importEffect;
    public float speed = 10f;
    public SpriteRenderer sr;
    public Color originColor;
    // Start is called before the first frame update
    void Start()
    {

        movePos = transform.position + new Vector3(0, -10f, 0);
        sr = GetComponent<SpriteRenderer>();
        originColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePos, speed * Time.deltaTime);
        if (transform.position == movePos)
        {
            // GameObject boom = Instantiate(initialboom, pos, transform.rotation);
            // Destroy(this.gameObject);
            FlashColor();
        }
    }
    public void FlashColor()
    {
        sr.color = Color.yellow;
        Invoke("ResetColor", 0.3f);
    }
    public void FlashColor2()
    {
        sr.color = Color.yellow;
        Invoke("ResetColor2", 0.3f);
    }
    public void ResetColor2()
    {
        sr.color = originColor;
        this.GetComponent<Renderer>().enabled = false;
    }
    public void ResetColor()
    {
        sr.color = originColor;
        Invoke("FlashColor2", 0.4f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            this.GetComponent<Renderer>().enabled = true;
            Invoke("boom", 0.1f);
        }
    }
    public void boom()
    {
        Vector3 pos = transform.position;
        GameObject effect = Instantiate(importEffect, pos, transform.rotation);
        // collision.SendMessage("BeAttack", damage);
        Destroy(this.gameObject);
    }
}
