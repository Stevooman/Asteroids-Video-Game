using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class controls the player's movement, invisible wall boundaries and bullets fired.
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Initialized to the bullet object in Unity Editor.
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// Does the player currently have a powerup?
    /// </summary>
    public bool HasPowerup { get; set; }

    /// <summary>
    /// The movement speed of the player.
    /// </summary>
    public float Speed { get; set; }

    /// <summary>
    /// The up and down movement of the player.
    /// </summary>
    public float VerticalInput { get; set; }

    /// <summary>
    /// The left and right movement of the player.
    /// </summary>
    public float HorizontalInput { get; set; }

    /// <summary>
    /// The top and bottom movement boundary.
    /// </summary>
    public float ZRangeLimit { get; set; }

    /// <summary>
    /// The left and right movement boundary.
    /// </summary>
    public float XRangeLimit { get; set; }

    public float PowerupDuration { get; set; }

    /// <summary>
    /// A reference to the player's RigidBody component.
    /// </summary>
    public Rigidbody PlayerRigidbody { get; set; }

    /// <summary>
    /// The starting point for the player's left bullet that is fired when powerup is enabled.
    /// </summary>
    public Vector3 LeftBulletPosition { get; set; }

    /// <summary>
    /// The starting point for the player's right bullet that's fired when powerup is enabled.
    /// </summary>
    public Vector3 RightBulletPosition { get; set; }

    // Names of the vertical and horizontal axes in Unity Editor
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    /// <summary>
    /// The tag of the powerup object in Unity Editor.
    /// </summary>
    private const string POWERUP_TAG = "Powerup";

    /// <summary>
    /// Initializes class members.
    /// </summary>
    private void Awake()
    {
        HasPowerup = false;
        Speed = 15;
        ZRangeLimit = 9.7f;
        XRangeLimit = 22.0f;
        PowerupDuration = 7.0f;
        PlayerRigidbody = GetComponent<Rigidbody>();
        LeftBulletPosition = new Vector3(-0.5f, 0, 0);
        RightBulletPosition = new Vector3(0.5f, 0, 0);
    }

    void Update()
    {
        DetectPlayerInput();
        ConstrainPlayerPosition();
        ShootBullet();
    }

    /// <summary>
    /// Detects player input and translates button presses into movement on the screen.
    /// </summary>
    private void DetectPlayerInput()
    {
        HorizontalInput = Input.GetAxis(HORIZONTAL_AXIS);
        VerticalInput = Input.GetAxis(VERTICAL_AXIS);
        transform.Translate(Vector3.right * HorizontalInput * Time.deltaTime * Speed);
        transform.Translate(Vector3.forward * VerticalInput * Time.deltaTime * Speed);
    }

    /// <summary>
    /// Controls the left, right, top and bottom boundaries for player movement.
    /// </summary>
    private void ConstrainPlayerPosition()
    {
        if (transform.position.z > ZRangeLimit)
            transform.position = new Vector3(transform.position.x, transform.position.y, ZRangeLimit);
        if (transform.position.z < -ZRangeLimit)
            transform.position = new Vector3(transform.position.x, transform.position.y, -ZRangeLimit);
        if (transform.position.x < -XRangeLimit)
            transform.position = new Vector3(-XRangeLimit, transform.position.y, transform.position.z);
        if (transform.position.x > XRangeLimit)
            transform.position = new Vector3(XRangeLimit, transform.position.y, transform.position.z);
    }

    /// <summary>
    /// Instantiates a bullet object when Mouse0 button is clicked and adds two extra bullets
    /// when the player has the powerup.
    /// </summary>
    private void ShootBullet()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
        if (Input.GetKeyDown(KeyCode.Mouse0) && HasPowerup)
        {
            Instantiate(bulletPrefab, transform.position + LeftBulletPosition, bulletPrefab.transform.rotation);
            Instantiate(bulletPrefab, transform.position + RightBulletPosition, bulletPrefab.transform.rotation);
        }
    }
}

