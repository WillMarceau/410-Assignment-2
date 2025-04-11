using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PickUp_Key : MonoBehaviour
{
    public const float key_range = 0.9f;
    public GameObject door_lock;
    private GameObject door;
    private GameObject key;
    private GameObject player;
    public ParticleSystem ps;
    private ParticleSystem.MainModule _main;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        door_lock = GameObject.FindGameObjectWithTag("Lock");
        key = GameObject.FindGameObjectWithTag("Key");
        player = GameObject.FindGameObjectWithTag("Player");
        door = GameObject.FindGameObjectWithTag("Open Door");
        //ps = door_lock.GetComponent<ParticleSystem>();
        _main = ps.main;
    }

    void OnEnable()
    {
        //ps = GetComponent<ParticleSystem>();
    }

    void OnParticleTrigger()
    {
        Gradient gradient = new Gradient();
        GradientColorKey[] gradients = new GradientColorKey[2];
        gradients[0] = new GradientColorKey(Color.red, 0.0f);
        gradients[1] = new GradientColorKey(Color.blue, 1.0f);

        GradientAlphaKey[] alphas = new GradientAlphaKey[2];
        alphas[0] = new GradientAlphaKey(1.0f, 0.0f);
        alphas[1] = new GradientAlphaKey(0.0f, 1.0f);

        gradient.SetKeys(gradients,alphas);

        _main.startColor = gradient;
        _main.loop = false;
        ps.Play();
        AnimateDoor();
    }

    void AnimateDoor()
    {
        
    }
    void OnTriggerEnter(Collider key) {
        if (key.gameObject.CompareTag("Key")) 
        {
        key.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            UnlockDoor();
        }
    }


    void UnlockDoor() {

        float distanceToKey = Vector3.Distance(door_lock.transform.position, player.transform.position);
        if (distanceToKey < key_range) {
            // Lock is close enough
            if (key.activeSelf == false) {
                // User has key
                OnParticleTrigger();
                door_lock.SetActive(false);

            } else {
                return;
            }
        } else {
            return;
        }

    }
}
