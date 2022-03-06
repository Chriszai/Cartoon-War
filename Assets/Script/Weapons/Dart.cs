using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private Transform dart;
    private Vector2 startSpeed;
    public GameObject initialbullet;
    public bool ishold;
    public AudioClip Sound;
    private AudioSource source;
    public AudioClip Sound2;
    void Start()
    {
        Application.targetFrameRate = 60;
        player = GameObject.Find("player");
        ishold = true;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && this.transform.parent && player.transform.GetChild(0) == this.transform)
        {
            if (ishold == true)
            {
                source.PlayOneShot(Sound);
                ishold = false;
                Vector3 pos = transform.position;
                GameObject bullet = Instantiate(initialbullet, pos, transform.rotation);
                player.GetComponent<Player>().changeMP(-2);
            }
        }

        if (Input.GetMouseButton(0) && this.transform.parent && player.transform.GetChild(0) == this.transform)
        {
            if (ishold == true)
            {
                source.PlayOneShot(Sound);
                ishold = false;
                Vector3 pos = transform.position;
                GameObject bullet = Instantiate(initialbullet, pos, transform.rotation);
                player.GetComponent<Player>().changeMP(-2);
            }
        }
    }
    public void response()
    {
        this.transform.position = this.transform.parent.transform.position;
        this.transform.Translate(0, -0.25f, 0, Space.World);
    }
    public void refresh()
    {
        ishold = true;
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Transform[] Transforms = player.transform.GetComponentsInChildren<Transform>(true);
                if (Transforms.Length >= 3)
                {
                    source.PlayOneShot(Sound2);
                    player.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    player.transform.GetChild(0).gameObject.transform.SetParent(null);
                    this.transform.SetParent(player.transform);
                    this.transform.SetAsFirstSibling();
                    response();
                }
                else
                {
                    source.PlayOneShot(Sound2);
                    this.transform.SetParent(player.transform);
                    this.transform.SetAsFirstSibling();
                    player.transform.GetChild(1).gameObject.SetActive(false);
                    response();
                }
            }
        }
    }
}
