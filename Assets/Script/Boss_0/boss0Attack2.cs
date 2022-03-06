using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss0Attack2 : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3.5f;
    public int damage = 1;
    public float destroyDistance = 200f;
    private Vector3 startPos;
    public GameObject importEffect;
    public Transform playerTransform;
    public Vector3 aimPoint;
    void Start()
    {
        Application.targetFrameRate = 60;
        startPos = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float dy = speed * Time.deltaTime;
        this.transform.Translate(Vector3.up * dy);
        // this.transform.position = Vector2.MoveTowards(transform.position, aimPoint, speed * Time.deltaTime);
        distanceCheck();
    }
    public void distanceCheck()
    {
        float distance = (transform.position - startPos).sqrMagnitude;
        if (distance > destroyDistance)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag.Equals("wall") || collision.tag.Equals("door_0") || collision.tag.Equals("door_0"))
        {
            Destroy(this.gameObject);
            Vector3 pos = transform.position;
            // GameObject effect = Instantiate(importEffect, pos, transform.rotation);
        }
        if (collision.tag.Equals("Player"))
        {
            Destroy(this.gameObject);
            Vector3 pos = transform.position;
            // GameObject effect = Instantiate(importEffect, pos, transform.rotation);
            collision.SendMessage("BeAttack", damage);
        }
    }
}