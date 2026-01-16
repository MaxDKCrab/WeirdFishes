using UnityEngine;

public class ActiveBase
{
    public Texture2D icon;
    public string activeName;
    
    public virtual void Activate()
    {
        Debug.Log("Activate Ability: " + activeName);
    }
}


public class ProductionActive : ActiveBase
{
    public GameObject unitToProduce;
    public float productionTime;
    public float primaryResourceCost;
    public float specialResourceCost;
    
    public override void Activate()
    {
        base.Activate();
        
        
    }
}

// public class AbilityActive : ActiveBase
// {
//     public float energyCost;
//     public float cooldown;
// }