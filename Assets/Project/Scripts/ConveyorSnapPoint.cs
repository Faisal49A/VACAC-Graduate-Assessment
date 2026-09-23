using UnityEngine;

public class ConveyorSnapPoint : MonoBehaviour
{
    public enum SnapType
    {
        Input,
        Output
    }

    [SerializeField] private SnapType snapType;
    [SerializeField] private ConveyorSegment conveyor;

    public SnapType Type => snapType;
    public ConveyorSegment Conveyor => conveyor;

    private void OnDrawGizmos()
    {
        if (snapType == SnapType.Input)
            Gizmos.DrawWireSphere(transform.position, 0.12f);
        else
            Gizmos.DrawSphere(transform.position, 0.12f);
    }
}