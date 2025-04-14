using Unity.VisualScripting;
using UnityEngine;

public class Key_Script : MonoBehaviour
{

    public bool following;
    private GameObject player;
    private PickUp_Key pickup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pickup = player.GetComponent<PickUp_Key>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (following == false) {
            transform.Rotate(0,100*Time.deltaTime, 0);
        }
    }
}
