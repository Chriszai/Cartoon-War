﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Atk1 : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;
    public int damage = 1;
    public float destroyDistance = 50f;
    private Vector3 startPos;
    public GameObject importEffect;
    public Vector3 movedir = Vector3.right;
    void Start()
    {
        startPos = transform.position;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        float dy = speed * Time.deltaTime;
        this.transform.Translate(movedir * dy);
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
