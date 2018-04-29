using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraMovement : MonoBehaviour
{
    public bool LockCursor;
    public float MouseSensitivity = 10f;
    public Transform Target;
    public float BackDistance;
    public float RightDistance;
    public float UpDistance;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSmoothTime = .12f;
    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;

    public bool IsZooming;
    public float ZoomSmooth;
    public float MaxZoom;
    private float currentZoom = 1f;

    public float SideSwitchSmooth;
    private bool isCameraRight = true;
    private float currentCamSide;

    private float yaw;
    private float pitch;

    private void Start()
    {
        if (LockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        currentCamSide = -RightDistance;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * MouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * MouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        IsZooming = Input.GetKey(KeyCode.Mouse1);
        if (IsZooming)
        {
            currentZoom = Mathf.Lerp(currentZoom, MaxZoom, ZoomSmooth);
        }
        else
        {
            currentZoom = Mathf.Lerp(currentZoom, 1f, ZoomSmooth);
        }

        if (Input.GetKeyDown(KeyCode.Q))
            isCameraRight = !isCameraRight;

        if (isCameraRight)
            currentCamSide = Mathf.Lerp(currentCamSide, -RightDistance, SideSwitchSmooth);
        else
            currentCamSide = Mathf.Lerp(currentCamSide, RightDistance, SideSwitchSmooth);

        Vector3 offset = (transform.forward * (BackDistance * currentZoom) + transform.right * currentCamSide + transform.up * -UpDistance);

        transform.position = Target.position - offset;
    }
}