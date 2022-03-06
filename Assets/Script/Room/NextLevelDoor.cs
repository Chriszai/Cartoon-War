using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public globalControl gc;
    public AudioClip Sound;
    private AudioSource source;
    void Start()
    {
        gc = GameObject.Find("globalControl").GetComponent<globalControl>();
        source = GetComponent<AudioSource>();
        // this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag.Equals("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                gc.save();
                source.PlayOneShot(Sound);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            }

        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                gc.save();
                source.PlayOneShot(Sound);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            }

        }
    }
}
