using UnityEngine;
using TMPro;

public class AltitudeHandler : MonoBehaviour
{
    public GameObject Player, deathBar;
    public TMP_Text altitudeText;
    public TMP_Text maxAltitudeText;

    public float smoothSpeed = 5f;
    private float maxAltitude = PlayerPrefs.GetFloat("MaxAltitude", 0f);
    private float startY;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
