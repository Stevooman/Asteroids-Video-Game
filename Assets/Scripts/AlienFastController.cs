using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class controls all of the movement behavior of the fast alien enemy.
/// </summary>
public class AlienFastController : MonoBehaviour
{
    /// <summary>
    /// The speed at which the fast alien initially travels when spawned.
    /// </summary>
    public float Speed { get; set; }

    /// <summary>
    /// The max speed of the fast alien.
    /// </summary>
    public float MaxSpeed { get; set; }

    /// <summary>
    /// The direction the fast alien travels.
    /// </summary>
    public Vector3 MoveDirection { get; set; }

    public GameObject Player { get; set; }

    /// <summary>
    /// The fast alien's RigidBody.
    /// </summary>
    public Rigidbody EnemyRb { get; set; }

    /// <summary>
    /// Name of the Player object in the Unity Editor.
    /// </summary>
    private const string PLAYER_OBJECT = "Player";

    /// <summary>
    /// Initializes class members.
    /// </summary>
    void Start()
    {
        Speed = 3.0f;
        MaxSpeed = 7.0f;
        Player = GameObject.Find(PLAYER_OBJECT);
        EnemyRb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Moves the fast alien constantly towards the player's current position and limits its speed.
    /// </summary>
    void Update()
    {
        if (Player != null)
        {
            MoveDirection = (Player.transform.position - transform.position).normalized;
            EnemyRb.AddForce(MoveDirection * Speed);
            if (EnemyRb.velocity.magnitude > MaxSpeed)
            {
                EnemyRb.velocity = Vector3.ClampMagnitude(EnemyRb.velocity, MaxSpeed);
            }
        }
    }
}

