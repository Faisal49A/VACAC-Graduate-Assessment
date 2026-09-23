using UnityEngine;

public class ProductSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] productPrefabs;
    [SerializeField] private ConveyorSegment startingConveyor;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 2f;

    private float timer;

    private void Start()
    {
        timer = spawnInterval;
    }

    private void Update()
    {
        if (productPrefabs == null ||
            productPrefabs.Length == 0 ||
            startingConveyor == null)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnProduct();
            timer = 0f;
        }
    }

    private void SpawnProduct()
    {
        // Pick a random product prefab.
        int randomIndex =
            Random.Range(0, productPrefabs.Length);

        GameObject selectedPrefab =
            productPrefabs[randomIndex];

        if (selectedPrefab == null)
            return;

        GameObject productObject =
            Instantiate(selectedPrefab);

        ConveyorProduct product =
            productObject.GetComponent<ConveyorProduct>();

        if (product != null)
        {
            product.SetConveyor(startingConveyor);
        }
        else
        {
            Debug.LogWarning(
                selectedPrefab.name +
                " does not contain ConveyorProduct."
            );
        }
    }
}