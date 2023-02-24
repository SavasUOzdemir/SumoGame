using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeSpawner : MonoBehaviour
{
    public GameObject dudePrefab;
    public float planeWidth = 9f;
    public float planeHeight = 9f;
    public int numberOfDudes = 9;
    [SerializeField] GameObject spawnParent;

    private void Start()
    {
        for (int i = 0; i < numberOfDudes; i++)
            SpawnDude();
    }
    //almost the same as foodspawn, logic is the same. difference is instead of repeated invoking instantiation occurs only once. 
    private void SpawnDude()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-planeWidth / 2, planeWidth / 2), 0.04f, Random.Range(-planeHeight / 2, planeHeight / 2));
        Quaternion spawnRotation = Quaternion.identity;
        GameObject food = Instantiate(dudePrefab, spawnPosition, spawnRotation);
        food.transform.parent = spawnParent.transform;
    }
}
