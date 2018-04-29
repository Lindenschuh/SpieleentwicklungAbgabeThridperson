using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThridPersonController : MonoBehaviour
{
    public float WalkSpeed;
    public float RunSpeed;
    public float JumpSpeed;

    public float TurnSmoothTime;
    private float turnSmoothVelocity;

    public float speedSmoothTime;
    private float speedSmoothVelocity;
    private float currentSpeed;

    public ThirdPersonCameraMovement CameraT;

    // Update is called once per frame
    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 inputDir = input.normalized;

        if (CameraT.IsZooming)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, CameraT.transform.eulerAngles.y, transform.eulerAngles.z);
            float targetSpeed = WalkSpeed;

            transform.Translate(new Vector3(inputDir.x, 0, inputDir.y) * targetSpeed * Time.deltaTime, Space.Self);
        }
        else
        {
            if (inputDir != Vector2.zero)
            {
                float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + CameraT.transform.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, TurnSmoothTime);
            }

            bool running = Input.GetKey(KeyCode.LeftShift);
            float targetSpeed = (running ? RunSpeed : WalkSpeed) * inputDir.magnitude;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
            Vector3 UpSpeed = (Input.GetKeyDown(KeyCode.Space)) ? transform.up * JumpSpeed : Vector3.zero;
            transform.Translate((transform.forward * targetSpeed * Time.deltaTime) + UpSpeed * Time.deltaTime, Space.World);
        }
    }
}