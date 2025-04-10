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

            // check if the enemy is ghost
            if (enemy.CompareTag("Ghost")) 
            {
                // log?
                Debug.Log("Enemy in range: " + enemy.name);

                 // check if player is behind enemy

                 // get the vector from the player to the enemy
                 Vector3 toEnemy = (enemy.transform.position - transform.position).normalized;

                 // get the vector of the players forward direction
                 Vector3 playerDirection = transform.forward;

                 // use dot product to compare the direction

                 float dotProd = Vector3.Dot(playerDirection, toEnemy);

                 // if in threshold
                 if (dotProd > 0.8f) 
                 {
                    Debug.Log("Enemy can be Eliminated");
                 }

                 else {
                    Debug.Log("Must be behind the enemy");
                 }
            }

            // check if player is behind enemy

            // check for line of sight

            // destroy ghost

            // break
        }
    }
}
