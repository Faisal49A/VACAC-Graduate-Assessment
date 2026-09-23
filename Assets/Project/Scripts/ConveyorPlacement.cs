using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ConveyorPlacement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera mainCamera;

    [Header("Conveyor Prefabs")]
    [SerializeField] private GameObject longConveyorPrefab;
    [SerializeField] private GameObject shortConveyorPrefab;
    [SerializeField] private GameObject inclineConveyorPrefab;
    [SerializeField] private GameObject declineConveyorPrefab;

    [Header("Placement")]
    [SerializeField] private float rotationStep = 90f;

    [Header("Snapping")]
    [SerializeField] private float snapDistance = 0.5f;

    [Header("UI")]
    [SerializeField] private TMP_Text selectedText;

    private GameObject selectedPrefab;
    private GameObject previewObject;
    private ConveyorSegment previewSegment;

    private float currentRotation;
    private ConveyorSegment snapTarget;
    private float previewHeightOffset;

    private void Start()
    {
        // Long conveyor selected by default.
        SelectConveyor(longConveyorPrefab);
    }

    private void Update()
    {
        HandleSelection();

        if (previewObject == null)
            return;

        UpdatePreviewPosition();
        HandleRotation();
        TrySnapPreview();

        // Only place when clicking outside the UI.
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
        {
            PlaceConveyor();
        }

        // Cancel placement with Escape.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelPreview();
        }
    }

    private void HandleSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectLongConveyor();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectShortConveyor();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectInclineConveyor();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectDeclineConveyor();
        }
    }

    public void SelectLongConveyor()
    {
        SelectConveyor(longConveyorPrefab);

        if (selectedText != null)
            selectedText.text = "Selected: Long Conveyor";
    }

    public void SelectShortConveyor()
    {
        SelectConveyor(shortConveyorPrefab);

        if (selectedText != null)
            selectedText.text = "Selected: Short Conveyor";
    }

    public void SelectInclineConveyor()
    {
        SelectConveyor(inclineConveyorPrefab);

        if (selectedText != null)
            selectedText.text = "Selected: Incline Conveyor";
    }

    public void SelectDeclineConveyor()
    {
        SelectConveyor(declineConveyorPrefab);

        if (selectedText != null)
            selectedText.text = "Selected: Decline Conveyor";
    }

    private void SelectConveyor(GameObject prefab)
    {
        if (prefab == null)
            return;

        selectedPrefab = prefab;

        if (previewObject != null)
        {
            Destroy(previewObject);
        }

        CreatePreview();
    }

    private void CreatePreview()
    {
        if (selectedPrefab == null)
            return;

        previewObject = Instantiate(selectedPrefab);
        previewObject.name = "Conveyor_Preview";

        // Remember the prefab's intended height.
        previewHeightOffset = selectedPrefab.transform.position.y;

        previewSegment =
            previewObject.GetComponent<ConveyorSegment>();

        if (previewSegment != null)
        {
            previewSegment.enabled = false;
        }
    }

    private void UpdatePreviewPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        Plane buildPlane = new Plane(Vector3.up, Vector3.zero);

        if (buildPlane.Raycast(ray, out float distance))
        {
            Vector3 point = ray.GetPoint(distance);

            previewObject.transform.position =
                new Vector3(
                    point.x,
                    previewHeightOffset,
                    point.z
                );

            previewObject.transform.rotation =
                Quaternion.Euler(
                    0f,
                    currentRotation,
                    0f
                );
        }

        snapTarget = null;
    }

    private void HandleRotation()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentRotation += rotationStep;

            if (currentRotation >= 360f)
            {
                currentRotation = 0f;
            }
        }
    }

    private void TrySnapPreview()
    {
        if (previewSegment == null)
            return;

        ConveyorSegment[] conveyors =
            FindObjectsOfType<ConveyorSegment>();

        float closestDistance = snapDistance;
        ConveyorSegment closestConveyor = null;

        foreach (ConveyorSegment conveyor in conveyors)
        {
            if (conveyor == previewSegment)
                continue;

            Vector3 previewInput =
                previewSegment.InputPoint.position;

            Vector3 conveyorOutput =
                conveyor.OutputPoint.position;

            // Ignore height when deciding whether we're close enough to snap.
            previewInput.y = 0f;
            conveyorOutput.y = 0f;

            float distance = Vector3.Distance(
                previewInput,
                conveyorOutput
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestConveyor = conveyor;
            }
        }

        if (closestConveyor == null)
            return;

        SnapToConveyor(closestConveyor);
        snapTarget = closestConveyor;
    }

    private void SnapToConveyor(ConveyorSegment target)
    {
        Vector3 targetDirection = target.GetDirection();
        targetDirection.y = 0f;

        Vector3 previewDirection =
            previewSegment.OutputPoint.position -
            previewSegment.InputPoint.position;

        previewDirection.y = 0f;

        if (targetDirection.sqrMagnitude > 0.001f &&
            previewDirection.sqrMagnitude > 0.001f)
        {
            targetDirection.Normalize();
            previewDirection.Normalize();

            Quaternion alignment =
                Quaternion.FromToRotation(
                    previewDirection,
                    targetDirection
                );

            previewObject.transform.rotation =
                alignment * previewObject.transform.rotation;
        }

        Vector3 offset =
            target.OutputPoint.position -
            previewSegment.InputPoint.position;

        previewObject.transform.position += offset;
    }

    private void PlaceConveyor()
    {
        if (selectedPrefab == null)
            return;

        GameObject placed =
            Instantiate(
                selectedPrefab,
                previewObject.transform.position,
                previewObject.transform.rotation
            );

        placed.name = selectedPrefab.name + "_Placed";

        ConveyorSegment placedSegment =
            placed.GetComponent<ConveyorSegment>();

        if (snapTarget != null &&
            placedSegment != null)
        {
            snapTarget.NextSegment = placedSegment;
        }
    }

    private void CancelPreview()
    {
        if (previewObject != null)
        {
            Destroy(previewObject);
        }

        previewObject = null;
        previewSegment = null;
        snapTarget = null;
    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current != null &&
               EventSystem.current.IsPointerOverGameObject();
    }
}