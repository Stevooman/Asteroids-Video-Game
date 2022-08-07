using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class controls all of the movement of the asteroids.
/// </summary>
public class AsteroidController : MonoBehaviour
{
    /// <summary>
    /// The maximum movement speed of an asteroid.
    /// </summary>
    public float MaxSpeed { get; set; }

    /// <summary>
    /// The movement trajectory of an asteroid along the X axis.
    /// </summary>
    public float RandomXDirection { get; set; }

    /// <summary>
    /// The movement trajectory of an asteroid along the Z axis.
    /// </summary>
    public float RandomZDirection { get; set; }

    /// <summary>
    /// The force applied to an asteroid when it's spawned.
    /// </summary>
    public Vector3 StartingForce { get; set; }

    /// <summary>
    /// A reference to the RigidBody of an asteroid object.
    /// </summary>
    public Rigidbody AsteroidRB { get; set; }

    /// <summary>
    /// Initializes class members.
    /// </summary>
    private void Awake()
    {
        MaxSpeed = 1.0f;
        RandomXDirection = Random.Range(-4, 4);
        RandomZDirection = Random.Range(-2, 2);
        AsteroidRB = GetComponent<Rigidbody>();
        StartingForce = new Vector3(RandomXDirection, 0, RandomZDirection);

        // If asteroid spawns with no movement, add movement.
        if (RandomXDirection == 0 && RandomZDirection == 0)
            RandomXDirection += 2;
    }

    /// <summary>
    /// Gets the asteroid's RigidBody and applies the StartingForce to it.
    /// </summary>
    void Start()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(StartingForce, ForceMode.Impulse);
    }

    /// <summary>
    /// Limits the movement speed of an asteroid.
    /// </summary>
    private void FixedUpdate()
    {
        if (AsteroidRB.velocity.magnitude > MaxSpeed)
        {
            AsteroidRB.velocity = Vector3.ClampMagnitude(AsteroidRB.velocity, MaxSpeed);
        }
    }
}

