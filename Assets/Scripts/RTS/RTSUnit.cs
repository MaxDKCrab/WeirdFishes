using UnityEngine;

public class RTSUnit : RtsEntityBase
{
    public override bool CommandOnEntity(RtsEntityBase other)
    {
        if (!base.CommandOnEntity(other)) return false;
            

        //implement basic unit behaviour
        
        
        Debug.Log("CommandOnEntity");
        return true;
    }

    public override bool CommandOnGround(Vector3 pos)
    {
        if (!base.CommandOnGround(pos)) return false;
        
        
        
        Debug.Log("CommandOnGround");
        return true;
    }
}
