using UnityEngine;

public class EnemyScaling : MonoBehaviour
{
    [SerializeField] private float minScale = 0.1f;
    [SerializeField] private float maxScale = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Pick a random uniform scale once, when the enemy spawns
        float size = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(size, size, size);
    }
}
