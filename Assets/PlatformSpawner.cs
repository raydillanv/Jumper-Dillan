using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class PlatformSpawner : MonoBehaviour
{
    public float minWidth = 1f, maxWidth = 5f, platformHeight = 0.5f;
    public float spawnInterval = 2f, horizontalRange = 6f;
    
    public Transform altitudeTransform, player;

    private float nextSpawnY;
    private List<GameObject> platforms = new();

    void Start()
    {
        nextSpawnY = player.position.y;
        for (int i = 0; i < 10; i++) SpawnPlatform();
        
    }

    void Update()
    {
        while (nextSpawnY < altitudeTransform.position.y + 20f) SpawnPlatform();
        for (int i = platforms.Count - 1; i >= 0; i--)
            if (platforms[i] == null || platforms[i].transform.position.y < altitudeTransform.position.y - 15f)
            { Destroy(platforms[i]); platforms.RemoveAt(i); } // get rid of platforms far below the camera
    }

    void SpawnPlatform()
    {
        GameObject p = new GameObject("Platform") { tag = "Ground" }; // make sure it has ground tag so it checks for jumping
        p.transform.SetParent(transform);
        p.transform.position = new Vector3(Random.Range(-horizontalRange, horizontalRange), nextSpawnY, 0f);
        p.transform.localScale = new Vector3(Random.Range(minWidth, maxWidth), platformHeight, 1f);

        var sr = p.AddComponent<SpriteRenderer>();
        sr.sprite = GetSquareSprite();
        sr.color = new Color(0.3f, 0.6f, 1f);

        p.AddComponent<BoxCollider2D>().usedByEffector = true;
        var e = p.AddComponent<PlatformEffector2D>(); // add effect so go through the bottom of a platform
        e.useOneWay = e.useOneWayGrouping = true;
        e.surfaceArc = 180f;

        platforms.Add(p);
        nextSpawnY += spawnInterval;
    }

    Sprite GetSquareSprite()
    {
        var tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.white);
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, 0, 1, 1), Vector2.one * 0.5f, 1f);
    }
}