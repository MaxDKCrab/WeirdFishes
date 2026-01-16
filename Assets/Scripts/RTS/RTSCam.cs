using System;
using UnityEngine;

public class RTSCam : MonoBehaviour
{

    private Camera cam;
    
    public float moveCamSpeed = 1.0f;
    public float zoomCamSpeed = 1.0f;
    public Vector2 CameraZoomBounds;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    
    public void MoveCam(Vector2 moveInput)
    {
        transform.position = new Vector3(transform.position.x + (moveInput.x * moveCamSpeed * Time.deltaTime),transform.position.y , transform.position.z + (moveInput.y * moveCamSpeed * Time.deltaTime));
    }

    public void ZoomCam(float ActionValue)
    {



        if (ActionValue > 0.1)
        {
            if (cam.orthographicSize - zoomCamSpeed > CameraZoomBounds.x) cam.orthographicSize -= zoomCamSpeed;
        }
        else if (ActionValue < -0.1)
        {
            if(cam.orthographicSize + zoomCamSpeed < CameraZoomBounds.y) cam.orthographicSize += zoomCamSpeed;
        }
    }
}
