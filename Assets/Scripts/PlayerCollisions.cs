using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class handles all player collisions with other objects in the game.
/// </summary>
public class PlayerCollisions : MonoBehaviour
{
    /// <summary>
    /// A reference to the GameManager used for altering game state.
    /// </summary>
    public GameManager GameMgr { get; set; }

    /// <summary>
    /// A reference to the PlayerController used for checking powerup state.
    /// </summary>
    public PlayerController Player { get; set; }

    // Tags and names of objects in the Unity Editor
    private const string ASTEROID_TAG = "Asteroid";
    private const string SLOWALIEN_TAG = "SlowAlien";
    private const string FASTALIEN_TAG = "FastAlien";
    private const string POWERUP_TAG = "Powerup";
    private const string GAME_MANAGER = "Game Manager";
    private const string PLAYER_OBJ = "Player";

    /// <summary>
    /// Initializes class members.
    /// </summary>
    private void Awake()
    {
        GameMgr = GameObject.Find(GAME_MANAGER).GetComponent<GameManager>();
        Player = GameObject.Find(PLAYER_OBJ).GetComponent<PlayerController>();
    }

    /// <summary>
    /// Handles the collision with a powerup.
    /// </summary>
    /// <param name="other">The Collider object of the game object a player collides with.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(POWERUP_TAG))
        {
            Destroy(other.gameObject);
            Player.HasPowerup = true;
            StartCoroutine(PowerupCountdown());
        }
    }

    /// <summary>
    /// Sets Player.HasPowerup to false once the PowerupDuration is reached.
    /// </summary>
    /// <returns></returns>
    private IEnumerator PowerupCountdown()
    {
        yield return new WaitForSeconds(Player.PowerupDuration);
        Player.HasPowerup = false;
    }

    /// <summary>
    /// Handles player collisions with any other game object besides a powerup.
    /// </summary>
    /// <param name="collision">The collision object that's created when the player touches another
    /// RigidBody or Collider.</param>
    private void OnCollisionEnter(Collision collision)
    {
        CheckEnemyTag(collision);
    }

    /// <summary>
    /// Destroys the player and ends the game if player collides with an enemy.
    /// </summary>
    /// <param name="collision">The collision object that's created when the player touches another
    /// RigidBody or Collider.</param>
    private void CheckEnemyTag(Collision collision)
    {
        if (collision.gameObject.CompareTag(ASTEROID_TAG) ||
            collision.gameObject.CompareTag(FASTALIEN_TAG) ||
            collision.gameObject.CompareTag(SLOWALIEN_TAG))
        {
            Destroy(gameObject);
            GameMgr.GameOver();
        }
    }
}
