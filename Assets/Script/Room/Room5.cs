using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room5 : MonoBehaviour
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

        if (children.Length == 9)
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
        if (collision.tag.Equals("Player") && children.Length > 9)
        {

            Invoke("CloseDoor", 1);//延时调用

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
