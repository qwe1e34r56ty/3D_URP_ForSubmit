using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public HashSet<Enemy> detectedEnemies = new();
    public float radius = 5f;

    private void Awake()
    {
        SphereCollider col = gameObject.AddComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = radius;
    }

    private void Update()
    {
        detectedEnemies.RemoveWhere(enemy => enemy == null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            detectedEnemies.Add(enemy);
        }
    }
}