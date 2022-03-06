using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss0Attack3 : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3.5f;
    public int damage = 2;
    public float destroyDistance = 200f;
    private Vector3 startPos;
    public GameObject importEffect;
    public Transform playerTransform;
    public Vector3 aimPoint;
    private float interval = 0.5f;
    private float count = 0f;
    private bool left;
    void Start()
    {
        left = true;
        Application.targetFrameRate = 60;
        startPos = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        this.transform.right = playerTransform.position - transform.position;
        this.transform.Rotate(0, 0, -30f);
    }

    // Update is called once per frame
    void Update()
    {
        if (count <= interval)
        {
            count += Time.deltaTime;
        }
        if (count >= interval && left == true)
        {
            count = 0;
            left = false;
        }
        else if (count >= interval && left == false)
        {
            count = 0;
            left = true;
        }
        if (left == true)
        {
            transform.Rotate(0, 0, 120 * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0, 0, -120 * Time.deltaTime);
        }

        float dy = speed * Time.deltaTime;
        this.transform.Translate(Vector3.right * dy);
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