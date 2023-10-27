using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
   

public class PlayerMovement : MonoBehaviour
{
    Camera camera;
    

    public float Speed = 5f;
    public float runSpeed = 8f;
    public float finalSpeed;
    public float smoothness = 10f;
    public bool toggleCameraRotation;
    public bool isRun;
    public float rotSpeed = 5f;
    private Animator ani;
    private Vector3 dir = Vector3.zero;

    [SerializeField] private Text[] ranking;
    void Start()
    {
        ani = GetComponent<Animator>();
        camera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            toggleCameraRotation = true;
        }
        else
        {
            toggleCameraRotation = false;

        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRun = true;
        }
        else
        {

            isRun = false;
        }
        InputMovment();

    }
    //private void LateUpdate()
    //{
    //    if (toggleCameraRotation != true)
    //    {
    //        Vector3 playerRotate = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
    //    }
    //}
    private void InputMovment()
    {
        finalSpeed = (isRun) ? runSpeed : Speed;

        Vector3 forward = transform.TransformDirection(Vector3.forward);

        Vector3 right = transform.TransformDirection(Vector3.right);




        //Vector3 moveDirection = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");
        Vector3 playerRotate = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));

        Vector3 moveDirection = playerRotate * Input.GetAxis("Vertical") + camera.transform.right * Input.GetAxis("Horizontal");
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 )
        {
            if (ani.GetBool("Walking") == false) { 
            ani.Play("Walk");
            ani.SetBool("Walking", true);
            }
        }
        else
        {
            ani.SetBool("Walking", false);

        }
     
        if (moveDirection != Vector3.zero)
        {

            // 회전
            transform.rotation = Quaternion.LookRotation(moveDirection);

            // 이동
            transform.position += (moveDirection.normalized * finalSpeed * Time.deltaTime);


        }


        float percent = ((isRun) ? 1 : 0.5f) * moveDirection.magnitude;

    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dead"))
        {
            GameManager.instance.GameOver();   
        }
    }
}
