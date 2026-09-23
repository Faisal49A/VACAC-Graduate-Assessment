using UnityEngine;

public class ConveyorSegment : MonoBehaviour
{
    [Header("Connection Points")]
    [SerializeField] private Transform inputPoint;
    [SerializeField] private Transform outputPoint;

    [Header("Conveyor Settings")]
    [SerializeField] private float speed = 1.5f;

    [SerializeField] private ConveyorSegment nextSegment;

    public Transform InputPoint => inputPoint;
    public Transform OutputPoint => outputPoint;
    public float Speed => speed;

    public ConveyorSegment NextSegment
    {
        get => nextSegment;
        set => nextSegment = value;
    }

    // Returns a position along this conveyor.
    // t = 0 is the input, t = 1 is the output.
    public Vector3 GetPosition(float t)
    {
        t = Mathf.Clamp01(t);

        return Vector3.Lerp(
            inputPoint.position,
            outputPoint.position,
            t
        );
    }

    public Vector3 GetDirection()
    {
        return (outputPoint.position - inputPoint.position).normalized;
    }

    public float GetLength()
    {
        return Vector3.Distance(
            inputPoint.position,
            outputPoint.position
        );
    }

    private void OnDrawGizmos()
    {
        if (inputPoint == null || outputPoint == null)
            return;

        Gizmos.DrawSphere(inputPoint.position, 0.08f);
        Gizmos.DrawSphere(outputPoint.position, 0.08f);

        Gizmos.DrawLine(
            inputPoint.position,
            outputPoint.position
        );
    }
}