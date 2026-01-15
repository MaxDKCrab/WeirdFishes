using System;
using System.Collections.Generic;
using UnityEngine;

public class RTSPlayer : MonoBehaviour
{
    List<RtsEntityBase> SelectedEntities = new  List<RtsEntityBase>();
    [SerializeField] Camera rtsCam;
    public LayerMask selectionLayer;
    public LayerMask commandLayers;
    public LayerMask groundLayer;
    BoxSelectionUI boxSelectionUI;
    private Vector3 gizmoCenter;
    private Vector3 gizmoSize;

    public bool debugVisuals;
    

// handle this reff and information better later
    private void Start()
    {
        boxSelectionUI = gameObject.GetComponent<BoxSelectionUI>();
    }

    public void TryBoxSelect(Vector2 start, Vector2 end)
    {
        DeselectEntity();
        
        Physics.Raycast(rtsCam.ScreenPointToRay(start), out RaycastHit startHit, Mathf.Infinity,groundLayer);
        Physics.Raycast(rtsCam.ScreenPointToRay(end), out RaycastHit endHit, Mathf.Infinity,groundLayer);
        
        float width = Mathf.Abs(endHit.point.x - startHit.point.x);
        float height = Mathf.Abs(endHit.point.z - startHit.point.z);
        

        Vector3 center = (startHit.point + endHit.point) * 0.5f;
        
        Collider[] found = Physics.OverlapBox(center,new Vector3(width,1000f,height), Quaternion.identity, selectionLayer);
        
        gizmoCenter = center;
        gizmoSize = new Vector3(width, 5f,height );

        RectTransform rect = boxSelectionUI.boxSelectUI.GetComponent<RectTransform>();
        
        Vector2 min = rect.anchoredPosition - (rect.sizeDelta / 2);
        Vector2 max = rect.anchoredPosition + (rect.sizeDelta / 2);
        
        foreach (Collider obj in found)
        {
            Debug.Log(obj.gameObject.name);
            if (obj.TryGetComponent(out RtsEntityBase entity))
            {
                Vector3 screenPos = rtsCam.WorldToScreenPoint(obj.transform.position);

                if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
                {
                    AddToSelection(entity);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!debugVisuals) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(gizmoCenter, gizmoSize);
    }

    public void TrySelectAtLocation(Vector2 mouseLocation)
    {
        DeselectEntity();

        Ray ray = rtsCam.ScreenPointToRay(mouseLocation);
        
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, selectionLayer))
        {
            if (hit.collider.TryGetComponent(out RtsEntityBase entity))
            {
                SelectEntity(entity);
            }
        }
        
    }


    enum CommandType
    {
        OnEntity = 1,
        OnGround = 2,
        OnNothing = 3,
    }
    
    public void TryCommandAtLocation(Vector2 mouseLocation)
    {
        Ray ray = rtsCam.ScreenPointToRay(mouseLocation);
        
        CommandType command = CommandType.OnNothing;
        RtsEntityBase targetRtsEntity = null;
        Vector3 targetPosition = new Vector3(0,0,0);
        
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, commandLayers))
        {
            Debug.Log("Command ray hit: " + hit.collider.gameObject.name);
            if (hit.collider.TryGetComponent(out RtsEntityBase entity))
            {
                command = CommandType.OnEntity;
                targetRtsEntity = entity;
            }
            else
            {
                command = CommandType.OnGround;
                targetPosition = hit.point;
            }
        }
        

        foreach (var entity in SelectedEntities)
        {
            switch (command)
            {
                case CommandType.OnEntity:
                    entity.CommandOnEntity(targetRtsEntity);
                    break;
                case CommandType.OnGround:
                    Debug.Log("Player CommandOnGround");
                    entity.CommandOnGround(targetPosition);
                    break;
                case CommandType.OnNothing:
                    entity.CommandOnNothing();
                    break;
            }    
        }
        
        Debug.Log("Tried Command");
    }
    
    void SelectEntity(RtsEntityBase rtsEntity)
    {
        Debug.Log("SelectUnit");
        DeselectEntity();
        SelectedEntities.Add(rtsEntity);
        rtsEntity.OnSelected();
    }

    void DeselectEntity(RtsEntityBase rtsEntity)
    {
        SelectedEntities.Remove(rtsEntity);
        rtsEntity.OnDeselected();
    }
    
    void DeselectEntity()
    {
        foreach (var entity in SelectedEntities)
        {
            entity.OnDeselected();
        }
        SelectedEntities.Clear();
    }

    void AddToSelection(RtsEntityBase rtsEntity)
    {
        SelectedEntities.Add(rtsEntity);
        rtsEntity.OnSelected();
    }
}
