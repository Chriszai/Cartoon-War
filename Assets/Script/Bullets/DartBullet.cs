using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 15f;
    public int damage = 1;
    public float destroyDistance = 200f;
    public Transform playerTransform;
    public GameObject importEffect;
    public float rotateSpeed;
    public float tuning;
    public Vector2 startSpeed;
    private Rigidbody2D rb2d;
    public Vector2 aimPoint;
    public float backTime;
    public bool touched;
    public GameObject player;
    void Start()
    {
        backTime = 10f;
        player = GameObject.Find("player");
        touched = false;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right * speed;
        startSpeed = rb2d.velocity;
        Application.targetFrameRate = 60;
        playerTransform = GameObject.Find("player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed);

        if (backTime >= 0)
        {
            rb2d.velocity = rb2d.velocity - (startSpeed * Time.deltaTime);
            backTime = backTime - 0.15f;
        }
        else
        {
            speed = speed * 1.05f;
            aimPoint = (playerTransform.position - transform.position) * speed * Time.deltaTime;
            rb2d.velocity = aimPoint;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (touched)
            {
                Destroy(this.gameObject);
                player.transform.GetChild(0).gameObject.SendMessage("refresh");
            }
            touched = true;

        }
        if (collision.tag.Equals("wall") || collision.tag.Equals("door_0") || collision.tag.Equals("door_0"))
        {

            backTime = -1f;

        }
        if (collision.tag.Equals("monster"))
        {
            collision.SendMessage("BeAttack", damage);
        }
    }
}