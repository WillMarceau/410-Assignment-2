using UnityEngine;

public class Takedown : MonoBehaviour
{

    public float takedownRange = 2f;
    public LayerMask Enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            Murder();
        }
    }

    void Murder() 
    {
        // get enemies in takedown range
        Collider[] closeEnemies = Physics.OverlapSphere(transform.position, takedownRange, Enemy);

        // loop over close enemies
        foreach (Collider enemy in closeEnemies) {

            // log?
            Debug.Log("Enemy in range: " + enemy.name);
        

            // check if the enemy is ghost

            // check if player is behind enemy

            // check for line of sight

            // destroy ghost

            // break
        }
    }
}
