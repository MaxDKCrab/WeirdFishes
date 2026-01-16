using System;
using UnityEngine;

public class RtsEntityBase : MonoBehaviour
{
    public bool allied;
    //public RTSPlayer player;
    [SerializeField] private GameObject selectedVisual;
    public int UiPriority;


    private void Start()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        
    }

    public void OnSelected()
    {
        selectedVisual.SetActive(true);
    }

    public void OnDeselected()
    {
        selectedVisual.SetActive(false);
    }


    public virtual bool CommandOnEntity(RtsEntityBase other)
    {
        return allied;
    }

    public virtual bool CommandOnGround(Vector3 pos)
    {
        Debug.Log("Entity CommandOnGround");
        return allied; 
    }

    public virtual bool CommandOnNothing()
    {
        return allied;
    }
    
}
