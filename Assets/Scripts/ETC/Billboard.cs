using System;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private bool lockX;
    [SerializeField] private bool lockY;
    [SerializeField] private bool lockZ;
    private Camera cam;
    void Start()
    {
        if (Camera.main != null) cam = Camera.main;
        else enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 ogRotation = transform.eulerAngles;
        transform.LookAt(cam.transform.position, Vector3.up);
        Vector3 rotation = transform.eulerAngles;
        if (lockX) rotation.x = ogRotation.x;
        if (lockY) rotation.y = ogRotation.y;
        if (lockZ) rotation.z = ogRotation.z;
        
        transform.eulerAngles = rotation;


    }
}
