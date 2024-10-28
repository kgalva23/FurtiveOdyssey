using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPosition : MonoBehaviour
{
    [Header("Helpers")]
    [SerializeField] Transform swordSpawnTransform;

    [Header("Prefabs")]
    [SerializeField] GameObject swordPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newSword = Instantiate(swordPrefab, swordSpawnTransform.position, Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
