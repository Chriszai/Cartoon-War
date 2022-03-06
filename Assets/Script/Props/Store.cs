using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public GameObject gift1;
    public GameObject gift2;
    public GameObject gift3;
    public Vector3 pos;
    public int curCoin;
    public bool issold;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        pos = transform.position + new Vector3(0, -1f, 0);
        issold = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        curCoin = player.GetComponent<Player>().getCoin();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && issold == false)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && issold == false)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            this.transform.GetChild(0).GetComponent<TextMesh>().text = "Do you want to spend \n 20 coins to buy an item ? ";
        }
        else if (collision.tag.Equals("Player") && issold == true)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            this.transform.GetChild(0).GetComponent<TextMesh>().text = "You already bought it";
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && issold == false && curCoin >= 20)
            {
                issold = true;
                int randomNum = Random.Range(0, 3);//(0,4)
                GameObject[] list = { gift1, gift2, gift3 };//, gift2, gift3, gift4, gift5 };
                GameObject treasure = Instantiate(list[randomNum], pos, transform.rotation);
                this.transform.GetChild(0).GetComponent<TextMesh>().text = "Purchase successfully";
            }
            else if (Input.GetKeyDown(KeyCode.E) && issold == false && curCoin < 20)
            {
                this.transform.GetChild(0).GetComponent<TextMesh>().text = "Purchase failed";
            }
        }
    }
}
