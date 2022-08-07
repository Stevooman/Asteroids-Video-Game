using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class controls all of the slow alien's movement.
/// </summary>
public class AlienSlowController : MonoBehaviour
{
    /// <summary>
    /// The speed at which a slow alien moves.
    /// </summary>
    public float Speed { get; set; }

    /// <summary>
    /// The max movement speed for a slow alien.
    /// </summary>
    public float MaxSpeed { get; set; }

    /// <summary>
    /// The direction which a slow alien travels.
    /// </summary>
    public Vector3 MoveDirection { get; set; }

    /// <summary>
    /// A reference to the player object.
    /// </summary>
    public GameObject Player { get; set; }

    /// <summary>
    /// A reference to the slow alien's RigidBody.
    /// </summary>
    public Rigidbody SlowAlienRB { get; set; }

    /// <summary>
    /// The name of the player object in Unity Editor.
    /// </summary>
    private const string PLAYER_OBJECT = "Player";

    /// <summary>
    /// Initializes class members.
    /// </summary>
    private void Awake()
    {
        Speed = 0.5f;
        MaxSpeed = 2.5f;
    }

    /// <summary>
    /// Finds the player object and gets the slow alien's RigidBody.
    /// </summary>
    void Start()
    {
        Player = GameObject.Find(PLAYER_OBJECT);
        SlowAlienRB = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Moves the slow alien constantly towards the player's current position and limits its speed.
    /// </summary>
    void Update()
    {
        if (Player != null)
        {
            MoveDirection = (Player.transform.position - transform.position).normalized;
            SlowAlienRB.AddForce(MoveDirection * Speed);
            if (SlowAlienRB.velocity.magnitude > MaxSpeed)
            {
                SlowAlienRB.velocity = Vector3.ClampMagnitude(SlowAlienRB.velocity, MaxSpeed);
            }
        }
    }
}

