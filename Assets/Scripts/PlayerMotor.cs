using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;
    private float verticalVelocity = 0.0f;
    private float gravity=50.0f;
    private float speed = 20.0f;
    private float animationDuration = 2.0f;
    private bool isDead = false;
    private float startTime;
    private Animator animator;
    private float jumpForce = 27.0f;

    private const float Lanedistance = 3.3f;
    private int desiredlane = 1;
    private float count=0.0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController> ();
        animator = GetComponent<Animator>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            animator.SetTrigger("death");
            return;
        }
        if(Time.time-startTime < animationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }
        moveVector = Vector3.zero;

        if(controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                verticalVelocity = jumpForce;
                animator.SetBool("jumping", true);
                Invoke("stopJumping", 0.1f);
            }
            /*else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                StartSliding();
                Invoke("StopSliding", 0.1f);
                Invoke("StopS1", 0.8f);
            }*/
            count = 0.0f;
        }
        else
        {
            count++;
            verticalVelocity -= gravity * Time.deltaTime;
        }
        if(count==100.0f)
        {
            Death();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartSliding();
            Invoke("StopSliding", 0.1f);
            Invoke("StopS1", 0.8f);
        }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveLane(false);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveLane(true);

        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredlane == 0)
            targetPosition += Vector3.left * Lanedistance;
        else if(desiredlane == 2)
            targetPosition += Vector3.right * Lanedistance;

        //moveVector.x = Input.GetAxisRaw("Horizontal") * 20;
        //  moveVector.y = Input.GetAxisRaw("Vertical") * speed;
        moveVector.x = (targetPosition - transform.position).normalized.x * 20;
        moveVector.y = verticalVelocity;
        
        moveVector.z = speed;
        controller.Move (moveVector *Time.deltaTime);
   
    }

    private void MoveLane(bool goingRight)
    {
        desiredlane += (goingRight) ? 1 : -1;
        desiredlane = Mathf.Clamp(desiredlane, 0, 2);
    }
    public void SetSpeed(float modifier)
    {
        speed =speed + modifier;
        if(gravity<=60.0f)
            gravity+=2.0f;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "enemy")
        {
            Death();
        }
    }
    private void Death()
    {
        isDead = true;
        GetComponent<Score>().OnDeath();
    }
    void stopJumping()
    {
        animator.SetBool("jumping", false);
    }
    private void StartSliding()
    {
        animator.SetBool("sliding", true);
        controller.height /= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y / 2, controller.center.z);
    }
    private void StopSliding()
    {
        animator.SetBool("sliding", false);
    }
    private void StopS1()
    {
        controller.height *= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y * 2, controller.center.z);
    }

}
