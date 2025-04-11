using UnityEngine;

public class TriggerDeath : MonoBehaviour
{
    public float spinSpeed = 360f;
    public float shrinkSpeed = 1f;
    public float fallSpeed = 2f;

    private bool dying = false;

     public void Death() 
    {
        // activate dying
        dying = true;

        // disable observer
        Observer obs = GetComponentInChildren<Observer>();
        if (obs != null) 
        {
            obs.enabled = false;
        }
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
