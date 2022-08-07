using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator GameOverOccursOnAsteroidCollision()
    {
        GameObject obj = MonoBehaviour.Instantiate(Resources.Load<GameObject>("My Prefabs/Asteroid"));
        GameManager game = obj.GetComponent<GameManager>();

        
        yield return null;
    }
}
