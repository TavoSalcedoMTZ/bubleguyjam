using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;       
    public float damping = 5f;        
    private Vector3 velocity;       
    private Vector3 inputDir;        

    private void Update()
    {
        HandleMovementInput();
        MovePlayer();
    }

    private void HandleMovementInput()
    {
        inputDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            inputDir += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputDir += Vector3.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputDir += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputDir += Vector3.right;
        }
        inputDir = inputDir.normalized;
    }

    private void MovePlayer()
    {
       
        if (inputDir != Vector3.zero)
        {
            velocity = Vector3.Lerp(velocity, inputDir * moveSpeed, Time.deltaTime * damping);
        }
        else
        {
        
            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * damping);
        }

       
        transform.position += velocity * Time.deltaTime;
    }
}
