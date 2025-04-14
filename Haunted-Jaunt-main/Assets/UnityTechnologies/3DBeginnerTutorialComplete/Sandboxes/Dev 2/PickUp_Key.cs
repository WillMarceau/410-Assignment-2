using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Rendering.PostProcessing;
using UnityEngine;
using UnityEngine.UIElements;

public class PickUp_Key : MonoBehaviour
{
    public const float key_range = 0.9f;
    public const float key_speed = 6.0f;
    public const int rotation_speed = 3;

    public GameObject door_lock;
    private Key_Script key_script;
    private GameObject door;
    private GameObject remove_door;
    public GameObject key;

    private GameObject open_wall;

    private bool key_found;
    private GameObject player;
    public ParticleSystem ps;
    private ParticleSystem.MainModule _main;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        door_lock = GameObject.FindGameObjectWithTag("Lock");
        key_script = key.GetComponent<Key_Script>();
        //key = GameObject.FindGameObjectWithTag("Key");
        player = GameObject.FindGameObjectWithTag("Player");
        door = GameObject.FindGameObjectWithTag("Open Door");
        remove_door = GameObject.FindGameObjectWithTag("Remove Door");
        open_wall = GameObject.FindGameObjectWithTag("Remove Wall");
        //ps = door_lock.GetComponent<ParticleSystem>();
        _main = ps.main;
    }

    public GameObject Get_Player() {
        return player;
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
        float timeElapsed = 0;
        Quaternion start = door.transform.rotation;
        Quaternion target = door.transform.rotation * Quaternion.Euler(0,100,0);
        while (timeElapsed < key_range) {
            door.transform.rotation = Quaternion.Lerp(start, target, timeElapsed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        door.transform.rotation = target;
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
            key.gameObject.tag = "Following_Key";
            key_found = true;
            key_script.following = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            UnlockDoor();
        }
    }

    void FixedUpdate()
    {
        if (key_found == true) {
            Vector3 player_position = player.transform.position;
            Vector3 offset = player_position - key.transform.position;
            player_position.y = player_position.y + 0.5f;
            player_position = player_position - (offset.normalized * 1.0f);
            key.transform.position = Vector3.MoveTowards(key.transform.position, player_position, key_speed * Time.deltaTime);
            Quaternion new_euler = Quaternion.Euler(player.transform.eulerAngles - new Vector3(0,180,0));
            key.transform.rotation = Quaternion.RotateTowards(key.transform.rotation,new_euler,Time.deltaTime * 130);
        }  
    }



    void UnlockDoor() {

        float distanceToKey = Vector3.Distance(door_lock.transform.position, player.transform.position);
        if (distanceToKey < key_range) {
            // Lock is close enough
            if (key_script.following == true) {
                // User has key
                key.SetActive(false);
                key_found = false;
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
