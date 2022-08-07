using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class handles all bullet collisions with other objects and updates the player's score depending
/// on which enemy object was destroyed by a bullet.
/// </summary>
public class BulletCollisions : MonoBehaviour
{
    /// <summary>
    /// A reference to GameManager used to update the player's score.
    /// </summary>
    public GameManager GameMgr { get; set; }

    /// <summary>
    /// Points for destroying a low-level enemy.
    /// </summary>
    public int LowPoints { get; set; }

    /// <summary>
    /// Points for destroying a mid-level enemy.
    /// </summary>
    public int MediumPoints { get; set; }

    /// <summary>
    /// Points for destroying a high-level enemy.
    /// </summary>
    public int HighPoints { get; set; }

    // Tags for enemy game objects in the Unity Editor
    private const string ASTEROID_TAG = "Asteroid";
    private const string SLOWALIEN_TAG = "SlowAlien";
    private const string FASTALIEN_TAG = "FastAlien";

    /// <summary>
    /// Initializes class members.
    /// </summary>
    private void Awake()
    {
        GameMgr = GameObject.Find("Game Manager").GetComponent<GameManager>();
        LowPoints = 100;
        MediumPoints = 250;
        HighPoints = 425;
    }

    /// <summary>
    /// Handles bullet collisions with other game objects. Destroys the other object if it's an enemy
    /// and gets its tag to pass along to NewScore().
    /// </summary>
    /// <param name="other">Collider object of the game object a bullet collides with.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (IsEnemy(other))
        {
            Destroy(other.gameObject);
            NewScore(other.tag);
        }
    }

    /// <summary>
    /// Is the object a bullet collides with an enemy?
    /// </summary>
    /// <param name="other">The Collider of a game object a bullet collides with.</param>
    /// <returns></returns>
    private bool IsEnemy(Collider other)
    {
        return other.gameObject.CompareTag(ASTEROID_TAG) ||
            other.gameObject.CompareTag(FASTALIEN_TAG) ||
            other.gameObject.CompareTag(SLOWALIEN_TAG);
    }

    /// <summary>
    /// Updates the score based on which enemy was destroyed.
    /// </summary>
    /// <param name="tag">The tag of the game object that was destroyed.</param>
    private void NewScore(string tag)
    {
        if (tag == ASTEROID_TAG)
            GameMgr.UpdateScore(LowPoints);
        if (tag == SLOWALIEN_TAG)
            GameMgr.UpdateScore(MediumPoints);
        if (tag == FASTALIEN_TAG)
            GameMgr.UpdateScore(HighPoints);
    }
}

