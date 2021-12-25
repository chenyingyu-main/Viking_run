using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Spawner : MonoBehaviour
{
    public GameObject GroundToSpawn;
    public GameObject General1;
    public GameObject General2;
    public GameObject General3;
    public GameObject Special1;
    public GameObject Special2;
    public GameObject Special3;

    public GameObject ReferenceObject;
    public Animator player;
    private Vector3 PreviousGroundPos;
    private Vector3 direction;
    private Vector3 mainDirection = new Vector3(0, 0, 1), otherDirection = new Vector3(1, 0, 0);
    public float DistanceBetweenGround = 8f;

    [SerializeField]private float timeOffset = 0.3f;
    public float randomValueOfDirection = 0.8f;
    private float specialRate = 0.3f;
    private float startTime;
    private GameObject ob;


    // Start is called before the first frame update
    void Start()
    {
        PreviousGroundPos = ReferenceObject.transform.position;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // generate speed (stop when player stop running)
        if (Time.time - startTime > timeOffset && player.GetBool("Run") == true)
        {
            // random chance to have same direction
            if(Random.value < randomValueOfDirection)
            {
                direction = mainDirection;
            }
            else // (1-random) chance to have different direction
            {
                Vector3 temp = direction;
                direction = otherDirection;
                mainDirection = direction;
                otherDirection = temp;
            }
            // generate special
            if(Random.value < specialRate)
            {
                float r = Random.value;
                if (r <= 0.33)
                {
                    ob = Special1;
                }
                else if (r > 0.33 && r <= 0.6) 
                {
                    ob = Special2;
                }
                else
                {
                    ob = Special3;
                }
            }
            else
            {
                float r = Random.value;
                if (r <= 0.25)
                {
                    ob = GroundToSpawn;
                }
                else if (r > 0.25 && r <= 0.45)
                {
                    ob = General1;
                }
                else if (r > 0.45 && r <= 0.75) 
                {
                    ob = General2;
                }
                else
                {
                    ob = General3;
                }
            }

            Vector3 groundPos = PreviousGroundPos + DistanceBetweenGround * direction;
            startTime = Time.time;
            Instantiate(ob, groundPos, Quaternion.Euler(0, 0, 0));
            PreviousGroundPos = groundPos;
        }
    }
    
}
