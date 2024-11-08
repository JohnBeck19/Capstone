using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulIcon : MonoBehaviour
{
    public Image image;          // Reference to the UI Image component
    public Sprite[] frames;      // Array to hold the frames from the sprite sheet
    public float framesPerSecond = 10f;

    private int currentFrame;
    private float timer;

    void Start()
    {
        if (frames.Length > 0)
        {
            image.sprite = frames[0];
        }
    }

    void Update()
    {
        if (frames.Length == 0) return;

        // Update timer
        timer += Time.deltaTime;
        if (timer >= 1f / framesPerSecond)
        {
            // Move to the next frame
            timer -= 1f / framesPerSecond;
            currentFrame = (currentFrame + 1) % frames.Length;
            image.sprite = frames[currentFrame];
        }
    }
}
