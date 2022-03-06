using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserGun : MonoBehaviour
{
    // Start is called before the first frame update
    private float interval = 0.5f;
    private float count = 0.5f;
    public GameObject initialbullet;
    public GameObject player;
    public LineRenderer lineRenderer;
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

        if (Input.GetMouseButtonDown(0) && this.transform.parent && player.transform.GetChild(0) == this.transform)
        {

            if (count >= interval && player.GetComponent<Player>().getCurMP() > 0)
            {
                source.PlayOneShot(Sound);
                count = 0;
                // lineRenderer.SetPosition(0, transform.position + transform.right);
                // lineRenderer.SetPosition(1, transform.position + transform.right * 5);
                Vector3 pos = transform.position + transform.right / 1.9f;
                GameObject bullet = Instantiate(initialbullet, pos, transform.rotation);
                this.transform.Translate(-0.1f, 0, 0, Space.Self);
                Invoke("response", 0.1f);
                player.GetComponent<Player>().changeMP(-5);
            }
        }

        if (Input.GetMouseButton(0) && this.transform.parent && player.transform.GetChild(0) == this.transform)
        {
            if (count >= interval && player.GetComponent<Player>().getCurMP() > 0)
            {
                source.PlayOneShot(Sound);
                count = 0;
                Vector3 pos = transform.position + transform.right / 1.9f;
                GameObject bullet = Instantiate(initialbullet, pos, transform.rotation);
                this.transform.Translate(-0.1f, 0, 0, Space.Self);
                Invoke("response", 0.1f);
                player.GetComponent<Player>().changeMP(-5);
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
