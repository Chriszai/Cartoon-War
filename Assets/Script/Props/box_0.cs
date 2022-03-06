using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box_0 : MonoBehaviour
{
    public GameObject gift1;
    public GameObject gift2;
    public GameObject gift3;
    // public GameObject gift5;
    Vector3 pos;
    public bool isopen;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Animator>().SetBool("open", false);
        pos = transform.position + new Vector3(0, 0.3f, 0);
        isopen = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && isopen == false)
        {   
            int randomNum = Random.Range(0, 3);//(0,4)
            this.GetComponent<Animator>().SetBool("open", true);
            GameObject[] list = { gift1, gift2, gift3 };//, gift2, gift3, gift4, gift5 };
            GameObject treasure = Instantiate(list[randomNum], pos, transform.rotation);
            isopen = true;
        }

    }
}
