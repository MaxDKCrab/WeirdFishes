using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RTSInput : MonoBehaviour
{
    RTSPlayer player;
    
    InputAction LMBAction;
    InputAction RMBAction;
    
    [HideInInspector] public Vector2 mousePos;

    public float boxSelectDistanceThreshold;

    public struct BoxSelectInfo
    {
        public bool relevant;
        public bool started;
        public Vector2 startPos;
        public Vector2 endPos;
    }
    
    [HideInInspector] public BoxSelectInfo boxSelectInfo = new BoxSelectInfo();
    
    void Start()
    {
        player = GetComponent<RTSPlayer>();
        
        LMBAction = InputSystem.actions.FindAction("LMB");
        RMBAction = InputSystem.actions.FindAction("RMB");
        
        LMBAction.performed += LMBActionOnPerformed;
        RMBAction.performed += RMBActionOnPerformed;
    }

    private void OnDisable()
    {
        LMBAction.performed -= LMBActionOnPerformed;
        RMBAction.performed -= RMBActionOnPerformed;
    }
    
    private void LMBActionOnPerformed(InputAction.CallbackContext obj)
    {
        if (boxSelectInfo.relevant)
        {
            player.TryBoxSelect(boxSelectInfo.startPos, boxSelectInfo.endPos);
        }
        else player.TrySelectAtLocation(mousePos);
        
        boxSelectInfo= new BoxSelectInfo();
    }
    
    private void RMBActionOnPerformed(InputAction.CallbackContext obj)
    {
        player.TryCommandAtLocation(mousePos);
    }
    
    void Update()
    {
        mousePos = InputSystem.actions.FindAction("MousePos").ReadValue<Vector2>();
        
        if (LMBAction.inProgress) HandleBoxSelect();
    }

    void HandleBoxSelect()
    {
        if (!boxSelectInfo.started)
        {
            boxSelectInfo.startPos = mousePos;
            boxSelectInfo.started = true;
        } 
        
        boxSelectInfo.endPos = mousePos;

        if (Vector2.Distance(boxSelectInfo.startPos, boxSelectInfo.endPos) > boxSelectDistanceThreshold)
        {
            boxSelectInfo.relevant = true;
        }
    }
    
    
}
