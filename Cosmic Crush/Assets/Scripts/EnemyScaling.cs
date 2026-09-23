using UnityEngine;

public class EnemyScaling : MonoBehaviour
{
    private float minScale = 0.5f;
    private float maxScale = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Pick a random uniform scale once, when the enemy spawns
        float size = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(size, size, size);
    }
}
