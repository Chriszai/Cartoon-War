using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgun : MonoBehaviour
{
    // Start is called before the first frame update
    private float interval = 0.8f;
    private float count = 0.8f;
    public GameObject initialbullet;
    public GameObject player;
    public AudioClip Sound;
    public AudioClip Sound2;
    private AudioSource source;
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

        if (Input.GetMouseButtonDown(0) && this.transform.parent && player.transform.GetChild(0) == this.transform)
        {

            if (count >= interval && player.GetComponent<Player>().getCurMP() > 0)
            {
                source.PlayOneShot(Sound);
                count = 0;
                Vector3 pos = transform.position;
                GameObject bullet1 = Instantiate(initialbullet, pos, transform.rotation);
                for (var i = 1; i < 4; i++)
                {
                    GameObject bullet = Instantiate(initialbullet, pos, transform.rotation);
                    bullet.transform.Rotate(0, 0, i * 8f);
                }
                for (var i = 1; i < 4; i++)
                {
                    GameObject bullet = Instantiate(initialbullet, pos, transform.rotation);
                    bullet.transform.Rotate(0, 0, i * -8f);
                }

                this.transform.Translate(-0.15f, 0, 0, Space.Self);
                Invoke("response", 0.1f);//延时调用
                player.GetComponent<Player>().changeMP(-3);
            }
            // weapon.transform.Translate(0, -0.25f, 0);
        }

        if (Input.GetMouseButton(0) && this.transform.parent && player.transform.GetChild(0) == this.transform)
        {
            if (count >= interval && player.GetComponent<Player>().getCurMP() > 0)
            {
                source.PlayOneShot(Sound);
                count = 0;
                Vector3 pos = transform.position;
                GameObject bullet1 = Instantiate(initialbullet, pos, transform.rotation);
                for (var i = 1; i < 4; i++)
                {
                    GameObject bullet = Instantiate(initialbullet, pos, transform.rotation);
                    bullet.transform.Rotate(0, 0, i * 8f);
                }
                for (var i = 1; i < 4; i++)
                {
                    GameObject bullet = Instantiate(initialbullet, pos, transform.rotation);
                    bullet.transform.Rotate(0, 0, i * -8f);
                }
                this.transform.Translate(-0.1f, 0, 0, Space.Self);
                Invoke("response", 0.1f);
                player.GetComponent<Player>().changeMP(-3);
            }

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
