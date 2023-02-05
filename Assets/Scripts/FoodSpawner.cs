using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public float planeWidth = 9f;
    public float planeHeight = 9f;
    public int numberOfFoods = 10;
    public float spawnInterval = 0.1f;
    //spawn 10 pieces of food in beginning, then spawn food based on variable spawnþterval declared above. 
    private void Start()
    {
        for (int i = 0; i < 10; i++)
            SpawnFood();
        InvokeRepeating("SpawnFood", 0, spawnInterval);
    }
    //in order to make it interesting, spawn food at randomly selected places.
    private void SpawnFood()
    {
        int currentFoodCount = GameObject.FindGameObjectsWithTag("Food").Length;

        if (currentFoodCount < numberOfFoods)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-planeWidth / 2, planeWidth / 2), 0.04f, Random.Range(-planeHeight / 2, planeHeight / 2));
            Quaternion spawnRotation = Quaternion.identity;
            GameObject food = Instantiate(foodPrefab, spawnPosition, spawnRotation);
        }
    }
}
