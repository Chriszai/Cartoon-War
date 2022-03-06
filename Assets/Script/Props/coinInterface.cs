using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinInterface : MonoBehaviour
{
    // Start is called before the first frame update
    public int MP;
    public int coin;
    public AudioClip Sound;
    public AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            source.PlayOneShot(Sound);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().getProp(coin, MP);
            Destroy(this.gameObject);
        }
    }
}
