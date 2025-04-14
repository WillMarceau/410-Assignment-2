using Unity.VisualScripting;
using UnityEngine;

public class Key_Script : MonoBehaviour
{

    public bool following;
    public int count; 
    public AudioClip key_audio;
    private AudioSource audioSource;
    private GameObject player;
    private PickUp_Key pickup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pickup = player.GetComponent<PickUp_Key>();
        count = 0;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (following == false) {
            transform.Rotate(0,100*Time.deltaTime, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            if (count == 0) {
                audioSource.PlayOneShot(key_audio);
            }
            
        }
    }
}
