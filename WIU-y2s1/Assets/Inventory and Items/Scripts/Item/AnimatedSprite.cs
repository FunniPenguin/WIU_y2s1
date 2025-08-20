using System.Collections;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    private SpriteRenderer sr;
    private Sprite[] frames;
    private float frameRate;
    private int currentFrame;
    private float timer;
    private float lifetime = 0.2f;
    private float fadeDuration = 0.5f;
    private float floatSpeed = 3f;

    public void SetUp(Sprite[] animationFrames, float fps, float lifetime = 0.2f, float fadeDuration = 0.5f, float floatSpeed = 3f)
    {
        sr = GetComponent<SpriteRenderer>();
        frames = animationFrames;
        frameRate = fps;
        currentFrame = 0;
        timer = 0;


        this.lifetime = lifetime;
        this.fadeDuration = fadeDuration;
        this.floatSpeed = floatSpeed;
        if (frames.Length > 0) { sr.sprite = frames[0]; }

        StartCoroutine(FadeOutAndFloatRoutine());
    }

    void Update()
    {
        if (frames == null || frames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= 1f / frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % frames.Length;
            sr.sprite = frames[currentFrame];
        }
    }

    private IEnumerator FadeOutAndFloatRoutine()
    {
        yield return new WaitForSeconds(lifetime);

        float elapsedTime = 0f;
        Color originalColour = sr.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            sr.color = new Color(originalColour.r, originalColour.g, originalColour.b, alpha);
            transform.position += floatSpeed * Time.deltaTime * Vector3.up;
            yield return null;
        }

        Destroy(gameObject);
    }
}
