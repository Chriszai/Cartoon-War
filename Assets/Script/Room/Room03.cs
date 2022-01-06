﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room03 : MonoBehaviour
{
    Transform[] children;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "door_0")
            {
                child.gameObject.SetActive(false);
            }
            else if (child.tag == "door_1")
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        children = GetComponentsInChildren<Transform>();

        if (children.Length == 13)
        {
            foreach (Transform child in transform)
            {
                if (child.tag == "door_0" || child.tag == "door_1")
                {

                    child.gameObject.SetActive(false);

                }

            }
        }

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.tag.Equals("Player"))
        {
            foreach (Transform child in transform)
            {
                if (child.tag == "monster")
                {
                    child.gameObject.SetActive(true);
                }
            }
            children = GetComponentsInChildren<Transform>();
            if (children.Length > 13)
            {
                Invoke("CloseDoor", 1);//延时调用
            }
        }
    }

    public void CloseDoor()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "door_0")
            {
                child.gameObject.SetActive(true);
            }

        }
    }
}