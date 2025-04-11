using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Rendering.PostProcessing;
using UnityEngine;

public class PickUp_Key : MonoBehaviour
{
    public const float key_range = 0.9f;
    public const int rotation_speed = 3;

    bool rotating;
    public GameObject door_lock;
    private GameObject door;
    private GameObject remove_door;
    private GameObject key;

    private GameObject open_wall;
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
        remove_door = GameObject.FindGameObjectWithTag("Remove Door");
        open_wall = GameObject.FindGameObjectWithTag("Remove Wall");
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
    }

    IEnumerator Rotate90() {
        rotating = true;
        float timeElapsed = 0;
        Quaternion start = door.transform.rotation;
        Quaternion target = door.transform.rotation * Quaternion.Euler(0,100,0);
        while (timeElapsed < key_range) {
            door.transform.rotation = Quaternion.Lerp(start, target, timeElapsed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        door.transform.rotation = target;
        rotating = false;
        door.transform.rotation = new Quaternion(door.transform.rotation.x, 180f - door.transform.rotation.y, door.transform.rotation.z, door.transform.rotation.w);
    }

    void AnimateDoor()
    {
        open_wall.SetActive(false);
        remove_door.SetActive(false);
        door.transform.position = new Vector3(2.79f, 0, -6.1f);
        StartCoroutine(Rotate90());
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
                AnimateDoor();
            } else {
                return;
            }

        } else {
            return;
        }

    }
}
