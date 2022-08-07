using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls the movement of bullets fired by the player.
/// </summary>
public class BulletController : MonoBehaviour
{
    /// <summary>
    /// The speed at which bullets travel.
    /// </summary>
    public float Speed { get; set; }

    /// <summary>
    /// The maximum distance in the Z direction bullets are allowed to travel.
    /// </summary>
    public int ZRange { get; set; }

    /// <summary>
    /// Initializes class members.
    /// </summary>
    void Start()
    {
        Speed = 50;
        ZRange = 20;
    }

    /// <summary>
    /// Moves bullets at a constant speed in one direction, and destroys bullets once they reach ZRange.
    /// </summary>
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
        if (transform.position.z > ZRange)
            Destroy(gameObject);
    }
}
