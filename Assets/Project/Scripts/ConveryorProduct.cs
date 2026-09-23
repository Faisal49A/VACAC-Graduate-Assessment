using UnityEngine;

public class ConveyorProduct : MonoBehaviour
{
    [SerializeField] private ConveyorSegment currentConveyor;

    private float distanceTravelled;

    private void Start()
    {
        if (currentConveyor != null)
        {
            transform.position = currentConveyor.InputPoint.position;
        }
    }

    private void Update()
    {
        if (currentConveyor == null)
            return;

        float conveyorLength = currentConveyor.GetLength();

        if (conveyorLength <= 0f)
            return;

        distanceTravelled += currentConveyor.Speed * Time.deltaTime;

        float progress = distanceTravelled / conveyorLength;

        transform.position = currentConveyor.GetPosition(progress);

        Vector3 direction = currentConveyor.GetDirection();

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        if (progress >= 1f)
        {
            MoveToNextConveyor();
        }
    }

    private void MoveToNextConveyor()
    {
        if (currentConveyor.NextSegment == null)
        {
            // Wait at the end until another conveyor is connected.
            distanceTravelled = currentConveyor.GetLength();
            return;
        }

        currentConveyor = currentConveyor.NextSegment;
        distanceTravelled = 0f;

        transform.position = currentConveyor.InputPoint.position;
    }

    public void SetConveyor(ConveyorSegment conveyor)
    {
        currentConveyor = conveyor;
        distanceTravelled = 0f;

        if (currentConveyor != null)
        {
            transform.position = currentConveyor.InputPoint.position;
        }
    }
}