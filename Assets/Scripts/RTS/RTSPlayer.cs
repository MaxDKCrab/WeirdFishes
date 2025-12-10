using System;
using System.Collections.Generic;
using UnityEngine;

public class RTSPlayer : MonoBehaviour
{
    List<UnitBase> SelectedUnits = new  List<UnitBase>();
    [SerializeField] Camera rtsCam;
    public LayerMask selectionLayer;
    public LayerMask groundLayer;

    private Vector3 gizmoCenter;
    private Vector3 gizmoSize;

    public float boxTightening;
    
    public void TryBoxSelect(Vector2 start, Vector2 end)
    {
        DeselectUnit();
        
        Physics.Raycast(rtsCam.ScreenPointToRay(start), out RaycastHit startHit, Mathf.Infinity,groundLayer);
        Physics.Raycast(rtsCam.ScreenPointToRay(end), out RaycastHit endHit, Mathf.Infinity,groundLayer);
        
        
        float width = Mathf.Abs(endHit.point.x - startHit.point.x);
        float height = Mathf.Abs(endHit.point.z - startHit.point.z);
        
        width -= boxTightening;
        height -= boxTightening;

        Vector3 center = (startHit.point + endHit.point) * 0.5f;
        
        Collider[] found = Physics.OverlapBox(center,new Vector3(width,1000f,height), Quaternion.identity, selectionLayer);
        
        gizmoCenter = center;
        gizmoSize = new Vector3(width, 5f,height );
        
        foreach (Collider obj in found)
        {
            Debug.Log(obj.gameObject.name);
            if (obj.TryGetComponent(out UnitBase unit))
            {
                AddToSelection(unit);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(gizmoCenter, gizmoSize);
    }

    public void TrySelectAtLocation(Vector2 mouseLocation)
    {
        DeselectUnit();

        Ray ray = rtsCam.ScreenPointToRay(mouseLocation);
        
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, selectionLayer))
        {
            if (hit.collider.TryGetComponent(out UnitBase unit))
            {
                SelectUnit(unit);
            }
        }
        
    }

    public void TryCommandAtLocation(Vector2 mouseLocation)
    {
        
    }
    
    void SelectUnit(UnitBase unit)
    {
        Debug.Log("SelectUnit");
        DeselectUnit();
        SelectedUnits.Add(unit);
        unit.OnSelected();
    }

    void DeselectUnit(UnitBase unit)
    {
        SelectedUnits.Remove(unit);
        unit.OnDeselected();
    }
    
    void DeselectUnit()
    {
        foreach (var unit in SelectedUnits)
        {
            unit.OnDeselected();
        }
        SelectedUnits.Clear();
    }

    void AddToSelection(UnitBase unit)
    {
        SelectedUnits.Add(unit);
        unit.OnSelected();
    }
}
