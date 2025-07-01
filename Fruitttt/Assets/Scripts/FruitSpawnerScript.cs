using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawnerScript : MonoBehaviour
{
    public GameObject[] fruitPrefabs;
    public GameObject bombPrefab;
    public float launchForce = 14f;
    public float spawnRangeX = 2f;
    public float launchForceVariation = 2f;
    public float bombChance = 0.2f;

    void Start()
    {
        InvokeRepeating("SpawnFruit", 1f, 2f);
    }

    void Update()
    {

    }

    void SpawnFruit() // This method now spawns either a fruit OR a bomb
    {
        // Calculate a random X position within the spawn range
        float randomXPos = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(randomXPos, transform.position.y, transform.position.z);

        GameObject objectToSpawn; // This will hold either our fruit or our bomb

        // Decide if we spawn a fruit or a bomb
        if (Random.value < bombChance) // Random.value gives a number between 0.0 and 1.0
        {
            objectToSpawn = bombPrefab;
        }
        else // It's a fruit, so pick a random fruit from our list!
        {
            // Randomly pick one fruit prefab from the fruitPrefabs array
            objectToSpawn = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)]; // <--- THIS LINE uses the PLURAL name!
        }

        // Instantiate the chosen object
        GameObject newObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        // Get its Rigidbody component (both fruits and bombs have one)
        Rigidbody objectRb = newObject.GetComponent<Rigidbody>();

        // Calculate random launch force (vertical + slight variation)
        float currentLaunchForce = launchForce + Random.Range(-launchForceVariation, launchForceVariation);

        // Calculate random horizontal launch force
        float randomXForce = Random.Range(-launchForceVariation, launchForceVariation);

        // Apply upward and horizontal force
        Vector3 totalLaunchForce = new Vector3(randomXForce, currentLaunchForce, 0);

        objectRb.AddForce(totalLaunchForce, ForceMode.Impulse);
    }
}