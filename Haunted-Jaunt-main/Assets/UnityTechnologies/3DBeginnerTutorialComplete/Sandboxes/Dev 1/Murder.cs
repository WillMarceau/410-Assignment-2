using UnityEngine;

public class Takedown : MonoBehaviour
{

    public const float takedownRange = 0.8f;
    public Transform urnTransform;
    public LayerMask Enemy;
    private bool death = false;
    public GameObject Urn;
    //public float yUrnOffset = -0.5f;
    public float spawnDistance = 20f;
    //public Transform urnTransform;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // get animator
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
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
                 if (dotProd > 0.90f) 
                 {
                    Debug.Log("Enemy can be Eliminated");
                    if (!death) 
                    {
                        // face enemy
                        //toEnemy.y = 0f;
                        //if (toEnemy != Vector3.zero)
                        //{
                          //  Quaternion targetRotation = Quaternion.LookRotation(toEnemy);
                           // transform.rotation= targetRotation;
                        //}

                        // spawn urn
                        //SpawnUrn();
                        //FaceUrn();
                        animator.SetTrigger("MurderTrigger");
                        animator.SetBool("Death", true);
                        SpawnUrn();
                        //FaceUrn();
                        enemy.GetComponent<TriggerDeath>().Death();
                    }
                    //Destroy(enemy.gameObject);
                 }

                 else {
                    Debug.Log("Must be behind the enemy");
                 }
            }

            // check for line of sight
            // might not need if we reduce the range

            // destroy ghost


            // break
        }
    }
    
    /*void FaceUrn() {

        // get urn direction
        Vector3 direction = urnTransform.position - transform.position;

        // disable up / down rotation
        direction.y = 0f;

        // check for zero
        if (direction != Vector3.zero)
        {
            // rotate goal
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }
    }
    */


    void SpawnUrn() 
    {
        // spawn urn in front of player, on the ground
        Vector3 spawnPosition = transform.position + transform.forward * 0.75f; //+ (-transform.right * 0.05f);
        //spawnPosition.y = 0.5f;
        Debug.Log("Spawn Position: " + spawnPosition);
        GameObject spawnedUrn = Instantiate(Urn, spawnPosition, Quaternion.identity);
        Debug.Log("Urn Actual Position: " + spawnedUrn.transform.position);

        urnTransform = spawnedUrn.transform;
    }
}
