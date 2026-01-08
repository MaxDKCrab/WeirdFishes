using UnityEngine;

public class RtsEntityBase : MonoBehaviour
{
    public bool allied;
    public RTSPlayer player;
    [SerializeField] private GameObject selectedVisual;

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
        return allied; 
    }

    public virtual bool CommandOnNothing()
    {
        return allied;
    }
    
}
