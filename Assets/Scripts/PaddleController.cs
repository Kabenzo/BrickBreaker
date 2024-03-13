using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    //Stats
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxBounceAngle = 75f;

    [Header("Experimental")]
    [SerializeField] private bool bounceRefection;

    //Components
    private Rigidbody2D rbody;
    private BoxCollider2D col;

    //Input
    private Vector2 input;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
    }

    private void HandleInput()
    {
        input.x = Input.GetAxis("Horizontal");
    }

    private void HandleMovement()
    {
        rbody.AddForce(input * moveSpeed);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bounceRefection)
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();

            if (ball != null)
            {
                Vector3 paddlePosiion = transform.position;
                Vector2 contactPoint = collision.GetContact(0).point;

                float offset = paddlePosiion.x - contactPoint.x;
                //Half the width of the paddle (so we can create a scale from -100% to +100% for the collision point on the paddle)
                float width = col.bounds.size.x / 2;

                float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rbody.velocity);
                //Percentage (offset/width) but cannot exceed maxBounceAngle
                float bounceAngle = (offset / width) * maxBounceAngle;
                float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);

                Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
                ball.rbody.velocity = rotation * Vector2.up * ball.rbody.velocity.magnitude;
            }
        }
    }
}
