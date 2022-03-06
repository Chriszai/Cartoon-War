using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBottle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public int HP;
    public int MP;

    void Start()
    {
        player = GameObject.Find("player");
        HP = 2;
        MP = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag.Equals("Player"))
        {
            other.GetComponent<Player>().Heal(HP, MP);
            Destroy(this.gameObject);
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<Player>().Heal(HP, MP);
            Destroy(this.gameObject);
        }
    }
}
