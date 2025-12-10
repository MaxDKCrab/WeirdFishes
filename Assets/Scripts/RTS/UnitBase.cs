using UnityEngine;

public class UnitBase : MonoBehaviour
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
}
