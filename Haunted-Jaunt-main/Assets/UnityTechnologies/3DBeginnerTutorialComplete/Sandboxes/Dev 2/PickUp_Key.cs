using UnityEngine;

public class PickUp_Key : MonoBehaviour
{
    public const float key_range = 0.5f;
    private GameObject door_lock;
    private GameObject key;
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        door_lock = GameObject.FindGameObjectWithTag("Lock");
        key = GameObject.FindGameObjectWithTag("Key");
        player = GameObject.FindGameObjectWithTag("Player");
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
            } else {
                return;
            }
        } else {
            return;
        }

    }
}
