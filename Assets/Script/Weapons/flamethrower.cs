using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flamethrower : MonoBehaviour
{
    // Start is called before the first frame update
    private float interval = 0.33f;
    private float count = 0.33f;
    public GameObject initialbullet;
    public GameObject player;
    public GameObject bullet;
    public AudioClip Sound;
    private AudioSource source;
    public AudioClip Sound2;
    void Start()
    {
        Application.targetFrameRate = 60;
        player = GameObject.Find("player");
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (count <= interval)
        {
            count += Time.deltaTime;
        }

        if (Input.GetMouseButton(0) && this.transform.parent && player.transform.GetChild(0) == this.transform)
        {
            if (count >= interval)
            {
                source.PlayOneShot(Sound);
                player.GetComponent<Player>().changeMP(-1);
                count = 0;
            }
            if (player.GetComponent<Player>().getCurMP() > 0)
            {
                Vector3 pos = transform.position;
                if (!bullet)
                {
                    bullet = Instantiate(initialbullet, pos, transform.rotation);
                    this.transform.Translate(-0.1f, 0, 0, Space.Self);
                    Invoke("response", 0.1f);
                }
                else
                {
                    if (bullet.transform.localScale.x < 1.3f)
                    {
                        bullet.transform.localScale += new Vector3(0.05f, 0.06f, 0);
                    }
                    bullet.transform.position = this.transform.position;
                    bullet.transform.rotation = this.transform.rotation;
                }


            }
        }
        if (Input.GetMouseButtonUp(0) && this.transform.parent && player.transform.GetChild(0) == this.transform)
        {
            destroyFlame();
        }
    }
    public void destroyFlame()
    {
        while (bullet.transform.localScale.x > 0.05f)
        {
            bullet.transform.localScale -= new Vector3(0.05f, 0, 0);
        }
        if (bullet.transform.localScale.x <= 0.05f)
        {
            Destroy(bullet);
        }
    }
    public void response()
    {
        this.transform.position = this.transform.parent.transform.position;
        this.transform.Translate(0, -0.25f, 0, Space.World);
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
