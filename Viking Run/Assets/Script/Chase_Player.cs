using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_Player : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;
    [SerializeField] private float distance = 8f;

    [SerializeField] private float speed = 5f;

    public Animator wolfAnimator;
    // private bool turnLeft, turnRight;

    private bool run;

    void Start()
    {
        wolfAnimator = GetComponent<Animator>();
        run = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (run == false)
            {
                run = true;
            }
            else
                run = false;
        }

        if (run)
        {
            offset = player.forward;
            transform.position = player.position - offset * distance;
            transform.forward = player.forward;
             // transform.localPosition += speed * Time.deltaTime * transform.forward;
        }
        wolfAnimator.SetBool("Run", run);
    }


}
