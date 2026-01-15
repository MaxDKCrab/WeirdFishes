using System;
using UnityEngine;
using UnityEngine.AI;

public class RTSUnit : RtsEntityBase
{
    private NavMeshAgent myAgent;


    public override void Initialize()
    {
        base.Initialize();
        
        myAgent = GetComponent<NavMeshAgent>();
    }
    
    public override bool CommandOnEntity(RtsEntityBase other)
    {
        if (!base.CommandOnEntity(other)) return false;
            

        //implement basic unit behaviour
        
        
        Debug.Log("CommandOnEntity");
        return true;
    }

    public override bool CommandOnGround(Vector3 pos)
    {
        if (!base.CommandOnGround(pos))
        {
            Debug.Log("Entity commandOnGround returned false on Unit");
            return false;
        }
        
        myAgent.SetDestination(pos);
        
        Debug.Log("Unit CommandOnGround");
        return true;
    }
}
