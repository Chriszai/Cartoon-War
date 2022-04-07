using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile : MonoBehaviour
{
    public Vector3 movePos;
    public float speed = 10f;
    public GameObject initialboom;
    // Start is called before the first frame update
    void Start()
    {
        movePos = transform.position + new Vector3(0, -10f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePos, speed * Time.deltaTime);
        if (transform.position == movePos)
        {
            Vector3 pos = transform.position;
            GameObject boom = Instantiate(initialboom, pos, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
