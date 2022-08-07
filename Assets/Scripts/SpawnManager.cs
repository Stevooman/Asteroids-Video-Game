using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class spawns and respawns each type of enemy as well as a powerup after the end of each enemy wave,
/// provided that certain thresholds are met (e.g. a set number of asteroids must be  destroyed for a new 
/// powerup to spawn). This class controls when and how many slow and fast aliens (which are the more 
/// difficult enemies) spawn and increments the number of those spawns each wave to increase game difficulty.
/// </summary>
public class SpawnManager : MonoBehaviour
{
    // GameObject's initialized in Unity Editor
    public GameObject asteroidPrefab;
    public GameObject slowAlienPrefab;
    public GameObject fastAlienPrefab;
    public GameObject powerupPrefab;

    /// <summary>
    /// A reference to the GameManager object used to check if the game is currently active.
    /// </summary>
    public GameManager GameMgr { get; set; }

    /// <summary>
    /// The total number of asteroids that have been destroyed.
    /// </summary>
    public int AsteroidsDestroyed { get; set; }

    /// <summary>
    /// Enemy spawn position in the X direction.
    /// </summary>
    public float SpawnRangeX { get; set; }

    /// <summary>
    /// Enemy spawn position in the Z direction.
    /// </summary>
    public float SpawnRangeZ { get; set; }

    /// <summary>
    /// Powerup spawn position in the Z direction.
    /// </summary>
    public float ZSpawnForPowerup { get; set; }

    /// <summary>
    /// Number of asteroids that must be destroyed in order for a powerup to spawn.
    /// </summary>
    public int ThresholdForPowerupSpawn { get; set; }

    /// <summary>
    /// Number of slow aliens that must be spawned in order for a fast alien to spawn.
    /// </summary>
    public int ThresholdForFastAlienSpawn { get; set; }

    /// <summary>
    /// The number of slow aliens that have been spawned.
    /// </summary>
    public int SlowAlienCounter { get; set; }

    /// <summary>
    /// The number of fast aliens that have been spawned.
    /// </summary>
    public int FastAlienCounter { get; set; }

    /// <summary>
    /// The total number of enemy waves that have been defeated.
    /// </summary>
    public int WaveCount { get; set; }

    private const float GROUNDLEVEL_Y_POSITION = 0.64f;

    /// <summary>
    /// The number of asteroids that must be destroyed in order for slow aliens to start spawning.
    /// </summary>
    private const int SLOW_ALIEN_THRESHOLD = 3;

    /// <summary>
    /// The number of asteroids that must be destroyed in order for a new powerup to spawn.
    /// </summary>
    private const int POWERUP_COUNTER = 15;

    /// <summary>
    /// The number of asteroids to spawn at each new wave.
    /// </summary>
    private int asteroidsToSpawn;

    private const string ASTEROID = "Asteroid";

    /// <summary>
    /// Initializes class properties and member variables.
    /// </summary>
    private void Awake()
    {
        GameMgr = GameObject.Find("Game Manager").GetComponent<GameManager>();
        AsteroidsDestroyed = 0;
        SpawnRangeX = 21;
        SpawnRangeZ = 9.7f;
        ZSpawnForPowerup = 10;
        ThresholdForPowerupSpawn = 15;
        ThresholdForFastAlienSpawn = 5;
        SlowAlienCounter = 1;
        FastAlienCounter = 1;
        WaveCount = 0;
        asteroidsToSpawn = 0;
    }

    /// <summary>
    /// Called every frame. Updates class properties and spawns correct number of enemies when all asteroids
    /// in a wave are destroyed.
    /// </summary>
    void Update()
    {
        if (!GameMgr.IsGameActive)
            return;
        if (ZeroAsteroids())
        {
            WaveCount++;
            AsteroidsDestroyed += asteroidsToSpawn;     // Keep track of total asteroids destroyed
            SpawnAsteroids(ref asteroidsToSpawn);

            if (SlowAlienThresholdReached())
            {
                for (int i = 0; i < SlowAlienCounter; i++)
                {
                    SpawnSlowAlien();
                }
                SlowAlienCounter++;
            }

            if (FastAlienThresholdReached())
            {
                for (int j = 0; j < FastAlienCounter; j++)
                {
                    SpawnFastAlien();
                }
                FastAlienCounter++;
            }

            if (PowerupThresholdReached())
            {
                SpawnPowerup();
                ThresholdForPowerupSpawn += AsteroidsDestroyed - ThresholdForPowerupSpawn;
                ThresholdForPowerupSpawn += POWERUP_COUNTER;    // At least POWERUP_COUNTER asteroids must be
            }                                                   // destroyed for another powerup to spawn
        }
    }

    /// <summary>
    /// Have all of the asteroids in a wave been destroyed?
    /// </summary>
    /// <returns>true if all of the asteroids in a wave have been destroyed. Otherwise returns false.</returns>
    private bool ZeroAsteroids()
    {
        if (GameObject.FindGameObjectsWithTag(ASTEROID).Length == 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Have enough asteroids and waves of enemies been destroyed to spawn a slow alien?
    /// </summary>
    /// <returns>true if enough asteroids and waves have been defeated to spawn a slow alien.
    /// Otherwise returns false.</returns>
    private bool SlowAlienThresholdReached()
    {
        return AsteroidsDestroyed >= SLOW_ALIEN_THRESHOLD &&
                    WaveCount >= SLOW_ALIEN_THRESHOLD;
    }

    /// <summary>
    /// Spawns the correct number of asteroids for each wave by incrementing the number of asteroids
    /// that were destroyed in the prior wave.
    /// </summary>
    /// <param name="enemies">The number of asteroids that were destroyed in the prior wave.</param>
    private void SpawnAsteroids(ref int enemies)
    {
        enemies++;
        for (int i = 0; i < enemies; i++)
        {
            Instantiate(asteroidPrefab, GenerateAsteroidPosition(),
            asteroidPrefab.transform.rotation);
        }
    }

    /// <summary>
    /// Generates a random spawn position for an asteroid.
    /// </summary>
    /// <returns>A Vector3 position for an asteroid to spawn.</returns>
    private Vector3 GenerateAsteroidPosition()
    {
        return new Vector3(Random.Range(-SpawnRangeX, SpawnRangeX),
            GROUNDLEVEL_Y_POSITION, SpawnRangeZ);
    }

    /// <summary>
    /// Spawns a slow alien in a random position. 
    /// </summary>
    private void SpawnSlowAlien()
    {
        Instantiate(slowAlienPrefab, GenerateSlowAlienPosition(),
            slowAlienPrefab.transform.rotation);
    }

    /// <summary>
    /// Generates a random spawn position for a slow alien.
    /// </summary>
    /// <returns>A Vector3 position for a slow alien to spawn.</returns>
    private Vector3 GenerateSlowAlienPosition()
    {
        return new Vector3(Random.Range(-SpawnRangeX, SpawnRangeX),
            GROUNDLEVEL_Y_POSITION, SpawnRangeZ);
    }

    /// <summary>
    /// Have enough slow aliens been spawned to begin spawning fast aliens?
    /// </summary>
    /// <returns>true if enough slow aliens have been spawned to begin spawning fast aliens.
    /// Otherwise returns false.</returns>
    private bool FastAlienThresholdReached()
    {
        return SlowAlienCounter >= ThresholdForFastAlienSpawn;
    }

    /// <summary>
    /// Spawns a fast alien in a random position.
    /// </summary>
    private void SpawnFastAlien()
    {
        Instantiate(fastAlienPrefab, GenerateFastAlienPosition(),
            fastAlienPrefab.transform.rotation);
    }

    /// <summary>
    /// Generates a random spawn position for a fast alien.
    /// </summary>
    /// <returns>A Vector3 position for a fast alien to spawn.</returns>
    private Vector3 GenerateFastAlienPosition()
    {
        return new Vector3(Random.Range(-SpawnRangeX, SpawnRangeX),
            GROUNDLEVEL_Y_POSITION, SpawnRangeZ);
    }

    /// <summary>
    /// Have enough asteroids been destroyed to spawn a powerup?
    /// </summary>
    /// <returns>true if enough asteroids have been destroyed to spawn a powerup. Otherwise returns false.</returns>
    private bool PowerupThresholdReached()
    {
        return AsteroidsDestroyed >= ThresholdForPowerupSpawn;
    }

    /// <summary>
    /// Spawns a powerup in a random position.
    /// </summary>
    private void SpawnPowerup()
    {
        Instantiate(powerupPrefab, GeneratePowerupPosition(),
            powerupPrefab.transform.rotation);
    }

    /// <summary>
    /// Generates a random spawn position for a powerup.
    /// </summary>
    /// <returns>A Vector3 position for a powerup to spawn.</returns>
    private Vector3 GeneratePowerupPosition()
    {
        return new Vector3(Random.Range(-SpawnRangeX, SpawnRangeX),
            GROUNDLEVEL_Y_POSITION, Random.Range(-ZSpawnForPowerup, ZSpawnForPowerup));
    }
}

