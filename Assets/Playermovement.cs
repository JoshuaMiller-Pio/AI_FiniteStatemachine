using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player movement
    public float rotationSpeed = 100f; // Speed of the player rotation

    private Rigidbody rb; // Reference to the player's Rigidbody component

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Getting the Rigidbody component attached to the player
    }

    // Update is called once per frame
    void Update()
    {
        // Getting input from the player
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Rotating the player
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);

        // Calculating the movement direction
        Vector3 movement = transform.forward * verticalInput;

        // Applying movement to the player
        rb.velocity = movement * moveSpeed;
    }
}