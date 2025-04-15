using UnityEngine;

public class TriggerDeath : MonoBehaviour
{
    public float spinSpeed = 360f;
    public float shrinkSpeed = 1f;
    public float fallSpeed = 2f;

    public float moveSpeed = 2f;

    private bool dying = false;

    private Transform urnTransform;

    //public GameObject Urn;

    //public float yUrnOffset = -0.5f;

     public void Death() 
    {
        // activate dying
        dying = true;

        // urn spawn point
        //Vector3 spawnPoint = transform.position + Vector3.up * yUrnOffset;

        // spawn urn
        //Instantiate(Urn, spawnPoint, Quaternion.identity);

        // stop movement
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }

        // disable observer
        Observer obs = GetComponentInChildren<Observer>();
        if (obs != null) 
        {
            obs.enabled = false;
        }

        // get urn location
        Takedown takedownScript = FindObjectOfType<Takedown>();

        if (takedownScript != null) 
        {
            urnTransform = takedownScript.urnTransform;
        }


        //WaypointPatrol wp = GetComponent<WaypointPatrol>();
        //if (wp != null) 
        //{
          //  wp.enabled = false;
        //}
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        if (dying)
        {

            // get direction of urn
            Vector3 directionToUrn = urnTransform.position - transform.position;
            directionToUrn.y = 0f;
            //urnTransform.position = Vector3.MoveTowards(transform.position, urnTransform.position, Time.deltaTime * 2f);
            transform.position = Vector3.MoveTowards(transform.position, urnTransform.position, Time.deltaTime * 2f);

            // spin
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);

            // shrink
            transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;

            // fall
            transform.position -= Vector3.up * fallSpeed * Time.deltaTime;

            // destroy when gets small enough
            if (transform.localScale.x <= 0.01f)
            {
                Destroy(gameObject);
            }
        }

    }

}
