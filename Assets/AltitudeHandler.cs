using UnityEngine;
using TMPro;

public class AltitudeHandler : MonoBehaviour
{
    public Transform player;
    public GameObject deathBar;
    public TMP_Text altitudeText, maxAltitudeText;
    
    public float smoothSpeed = 5f;

    private float maxAltitude, startY;
    private bool hasLifted;

    void Start()
    {
        startY = transform.position.y;
        maxAltitude = PlayerPrefs.GetFloat("MaxAltitude", 0f);
        if (deathBar) deathBar.SetActive(false);
    }

    void Update()
    {
        float alt = player.position.y - startY;

        // camera moves upward using math lerp so its not jarring and player doesnt go out of frame
        if (player.position.y > transform.position.y)
            transform.position = new Vector3(transform.position.x,
                Mathf.Lerp(transform.position.y, player.position.y, smoothSpeed * Time.deltaTime),
                transform.position.z);


        if (!hasLifted && transform.position.y > startY + 0.1f)
        {
            hasLifted = true;
            if (deathBar) deathBar.SetActive(true);
        }
        
        if (alt > maxAltitude) PlayerPrefs.SetFloat("MaxAltitude", maxAltitude = alt);

        if (altitudeText) altitudeText.text = $"Alt: {alt:F1}m"; // current position of jumper
        if (maxAltitudeText) maxAltitudeText.text = $"Best: {maxAltitude:F1}m"; // highest point theyve been this session
    }

    public float GetCurrentAltitude() => player.position.y - startY;
}