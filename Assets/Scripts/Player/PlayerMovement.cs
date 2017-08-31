using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    private Rigidbody rigidBody;
    private Animator animator;
    private float maxRayDistance = 1000f;
    private int floorMask;

    private void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
        rigidBody.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        var horizontalMovement = Input.GetAxisRaw("Horizontal");
        var verticalMovement = Input.GetAxisRaw("Vertical");

        Move(horizontalMovement, verticalMovement);
        Animate(horizontalMovement, verticalMovement);
    }

    private void Animate(float horizontalMovement, float verticalMovement)
    {
        bool isMoving = horizontalMovement != 0 || verticalMovement != 0;
        animator.SetBool("Moving", isMoving);
        Turn();
    }

    private void Turn()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(mouseRay, out hitInfo, maxRayDistance, floorMask)) {
            Quaternion newRotation = Quaternion.LookRotation(hitInfo.point - transform.position);
            rigidBody.MoveRotation(newRotation);
        }
    }

    private void Move(float horizontalMovement, float verticalMovement)
    {
        Vector3 movement = new Vector3(horizontalMovement, 0, verticalMovement);
        movement = movement.normalized * speed * Time.deltaTime;
        rigidBody.MovePosition(transform.position + movement);
    }
}
