using UnityEngine;

public class Disable : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    public GameObject lid;
    public GameObject urnWithLid;
    private Transform rightHand;
    private GameObject spawnedLid;

    public GameObject urnParticles;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        // get player
        PlayerMovement player = animator.GetComponent<PlayerMovement>();

        if (player != null)
        {
            // disable input
            player.canMove = false;
        }

        //Transform rightHand = animator.transform.Find("Root/Hips/Spine/RightShoulder/RightArm/RightForeArm/RightHand");
        Debug.Log("Searching for hand");
        GameObject rightHandObj = GameObject.FindGameObjectWithTag("Right_Hand");

        if (rightHandObj != null)
        {
            Debug.Log("Assigning Hand Transform");
            rightHand = rightHandObj.transform;
        }

        // get right hand
        //rightHand = animator.GetBoneTransform(HumanBodyBones.Righthand);
        if (rightHand != null && layerIndex != null) 
        {
            Debug.Log("Got to here");
            spawnedLid = GameObject.Instantiate(lid, rightHand);
            spawnedLid.transform.localPosition = new Vector3(0.05f, 0.3f, 0f);
            spawnedLid.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        // remove lid
        GameObject.Destroy(spawnedLid);

        // create lidded urn on old urn
        Takedown script = FindObjectOfType<Takedown>();
        GameObject currentUrn = script.currentUrn;

        if (currentUrn != null)
        {
            Vector3 position = currentUrn.transform.position;
            Quaternion rotation = currentUrn.transform.rotation;

            GameObject lidUrn = GameObject.Instantiate(urnWithLid, position, rotation);
            GameObject.Destroy(currentUrn);

            // spawn particles
            GameObject particles = Instantiate(urnParticles, position + new Vector3(0f, 0.3f, 0f), Quaternion.identity);
            //particles.transform.SetParent(lidUrn);
        }

    }
}

