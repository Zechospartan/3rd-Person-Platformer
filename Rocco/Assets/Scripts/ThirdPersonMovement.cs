using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
     CharacterController controller;
    public float speed = 6F;
    public float turnSmoothTime = 0.1F;
    float turnSmoothVelocity;
    public Transform camTrans;
    float distance;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        distance = 5F;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal , 0, vertical ).normalized;

        if (direction.magnitude >= 0.1f)
        { 
            //roation 
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camTrans.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity , turnSmoothTime);

            transform.rotation = Quaternion.Euler(0F, angle, 0F);
            //move 

            Vector3 moveDir = Quaternion.Euler(0F, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

    }
}
