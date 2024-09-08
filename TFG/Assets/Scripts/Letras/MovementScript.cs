using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script of each falling letter
/// </summary>
public class MovementScript : MonoBehaviour
{
    /// <summary>
    /// Value of speed of the letter. <see cref="LetterManagerScript"/> LetterManagerScript </see> manages it.
    /// </summary>
    public static float speed = 0.1f; 

    /// <summary>
    /// Each frame the letter falls a certain speed.
    /// </summary>
    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}
