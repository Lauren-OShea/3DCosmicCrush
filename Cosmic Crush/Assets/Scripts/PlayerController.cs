using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    // Rigidbody of the player.
    private Rigidbody rb;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    private float minScale = 0.5f;
    private float maxScale = 3.0f;

    // Speed at which the player moves.
    public float speed = 0;

    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);
    }

    // Added Update() because FixedUpdate() was unreliably detecting key changes.
    void Update()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard != null && (keyboard.wKey.wasPressedThisFrame || keyboard.aKey.wasPressedThisFrame
                                 || keyboard.sKey.wasPressedThisFrame || keyboard.dKey.wasPressedThisFrame))
        {
            ChangeSize(-0.05f);
        }
    }

    // Grows or shrinks the player, keeping its scale between minScale and maxScale.
    // Mass changes by the same amount the scale actually changed, so it also stops once the scale hits a limit.
    void ChangeSize(float amount)
    {
        float currentScale = transform.localScale.x;
        float newScale = Mathf.Clamp(currentScale + amount, minScale, maxScale);

        transform.localScale = new Vector3(newScale, newScale, newScale);
        rb.mass += newScale - currentScale;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Vector3 enemyScale = other.transform.lossyScale;
            Vector3 playerScale = transform.lossyScale;

            float enemySize = enemyScale.x * enemyScale.y * enemyScale.z;
            float playerSize = playerScale.x * playerScale.y * playerScale.z;

            if (enemySize < playerSize)
            {
                other.gameObject.SetActive(false);

                ChangeSize(0.1f);
            }
        }
    }
}
