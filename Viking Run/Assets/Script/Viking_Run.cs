using UnityEngine;
using System.Collections;
using UnityEngine.UI;


//[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]


public class Viking_Run : MonoBehaviour
{

    private bool turnLeft, turnRight;
    [SerializeField] public float speed = 5f;
    [SerializeField] private float jumpSpeed = 2f;
    [SerializeField] private AudioSource diamondSound;
    public Text youGotCaught1;
    public Text youGotCaught2;
    public Text youFall1;
    public Text youFall2;

    [SerializeField]private float onCollisionTime = 2f;
    private float colTime;
    private bool countDown;


    //private CharacterController myCharacter;
    private Animator animator;
    private Rigidbody rb;

    private bool midJump;
    private bool running;
    private bool jumping;
    private bool stopRunning;

    // Start is called before the first frame update
    void Start()
    {
        //myCharacter = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        midJump = false;
        stopRunning = true;
        colTime = onCollisionTime;
        countDown = false;

        youGotCaught1.enabled = false;
        youGotCaught2.enabled = false;
        youFall1.enabled = false;
        youFall2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        turnLeft = Input.GetKeyDown(KeyCode.Q);
        turnRight = Input.GetKeyDown(KeyCode.E);


        // turn left and turn right
        if (turnLeft)
        {
            transform.Rotate(new Vector3(0f, -90f, 0f));
        }
        else if (turnRight)
        {
            transform.Rotate(new Vector3(0f, 90f, 0f));
        }

        // run and pause
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (stopRunning) {
                stopRunning = false;
                running = true;
            }
            else
            {
                stopRunning = true;
                running = false;
            }
        }

        if (!stopRunning) {
            // keep running forward
            transform.localPosition += speed * Time.deltaTime * transform.forward;
            running = true;
            jumping = false;
        }

       
        // move left and right
        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition += speed * Time.deltaTime * transform.right * -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += speed * Time.deltaTime * transform.right;
        }
        // jump
        if (Input.GetKeyDown(KeyCode.W) && midJump == false)
        {
            // Debug.Log("Jump");
            rb.velocity = new Vector3(0, jumpSpeed, 0);

            // jump animation
            jumping = true;

            midJump = true;
            StartCoroutine(stopJump());
        }

        // count down collision on obstacle
        if (countDown)
        {
            colTime -= Time.deltaTime;
        }
        // if the collision time is more than onCollisionTime, player got catched by wolf
        if (colTime < 0)
        {
            FindObjectOfType<GameManager>().GameEnd();
            youGotCaught1.enabled = true;
            youGotCaught2.enabled = true;
        }

        // animation
        animator.SetBool("Run", running);
        animator.SetBool("Jump", jumping);
    }

    void FixedUpdate()
    {
        // fall down
        if(rb.position.y < -1f)
        {
            FindObjectOfType<GameManager>().GameEnd();
            youFall2.enabled = true;
            youFall1.enabled = true;
        }
    }

    // prevent double jump
    IEnumerator stopJump()
    {
        float time = jumpSpeed / 5;
        yield return new WaitForSeconds(time);
        rb.velocity = new Vector3(0, -jumpSpeed, 0);
        //yield return new WaitForSeconds(time);
        midJump = false;
    }


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Diamond")
        {
            Destroy(collision.gameObject);
            diamondSound.Play();
            ScoreManager.instance.AddPoints();
        }
        colTime = onCollisionTime;
        if(collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("obstacle");
            countDown = true;
        }
    }
    
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            colTime = onCollisionTime;
            countDown = false;
        }
    }
}
