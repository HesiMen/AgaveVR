using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SurvivalSystem : MonoBehaviour
{
    #region Singleton
    public static SurvivalSystem instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SurvivalSystem>();
            }
            return _instance;
        }
    }
    static SurvivalSystem _instance;

    void Awake()
    {
        _instance = this;
    }

    #endregion

    // Stats Display
    public TextMeshPro temperatureDisplay;
    public TextMeshPro hungerDisplay;
    public TextMeshPro healthDisplay;

    public NormalizedIntensitySO normalizedIntensity;
    // Health hidden from player, but necessary for survival purposes
    private float baseHealth = 80.0f;
    private float maxHealth = 100.0f;
    public float currentHealth;

    private float baseTemperature = 50.0f;
    private float maxTemperature = 90f;
    public float currentTemperature;

    private float baseHunger = 80.0f;
    private float maxHunger = 90f;
    public float currentHunger;

    // Loss occurs each interval
    public float survivalInterval = 60.0f;
    private bool healthCoroutineRunning = false;
    Coroutine healthCoroutine;
    Coroutine hungerCoroutine;
    Coroutine temperatureCoroutine;

    // Hunger and health interval loss are consistent, temperature interval depends on weather conditions
    public float healthIntervalLoss = -20.0f;
    public float hungerIntervalLoss = -8.0f;
    public float temperatureIntervalLoss = 0.0f;

    // Initialize system
    public Shapes.Disc hungerBar;
    public Shapes.Disc temperatureBar;

    void Start()
    {
        currentTemperature = baseTemperature;
        currentHunger = baseHunger;
        currentHealth = baseHealth;

        temperatureDisplay.text = ((int)currentTemperature).ToString();
        hungerDisplay.text = ((int)currentHunger).ToString();
        healthDisplay.text = ((int)currentHealth).ToString();

        // Begin tracking survival statistics
        hungerCoroutine = StartCoroutine(HungerLoss(survivalInterval));
        temperatureCoroutine = StartCoroutine(TemperatureLoss(survivalInterval));

    }
    public void UpdateHealth(float healthDelta)
    {
        currentHealth += healthDelta;
        currentHealth = Mathf.Clamp(currentHealth, 0.0f, maxHealth);

      //  Debug.Log("Player health changed by " + healthDelta + ".");
    }

    public void UpdateHunger(float hungerDelta)
    {
        currentHunger += hungerDelta;
        currentHunger = Mathf.Clamp(currentHunger, 0.0f, maxHunger);
        hungerBar.AngRadiansEnd = currentHunger * Mathf.Deg2Rad;
        //Debug.Log("Player hunger changed by " + hungerDelta + ".");
    }

    public void UpdateTemperature(float temperatureDelta)
    {
        currentTemperature += temperatureDelta;
        currentTemperature = Mathf.Clamp(currentTemperature, 0.0f, maxTemperature);
        temperatureBar.AngRadiansEnd = currentTemperature * Mathf.Deg2Rad;
        // Debug.Log("Player body temperature changed by " + temperatureDelta + ".");

    }

    void Update()
    {
        // Check weather status each frame to update temperature interval loss
        temperatureIntervalLoss = -2f * normalizedIntensity.normalizedIntensity; //-4.0f;

        temperatureDisplay.text = ((int)currentTemperature).ToString();
        hungerDisplay.text = ((int)currentHunger).ToString();
        healthDisplay.text = ((int)currentHealth).ToString();

        // Check if health needs to drop
        if ((currentHunger == 0 || currentTemperature == 0 || currentTemperature == 100) && healthCoroutineRunning == false)
        {
            healthCoroutine = StartCoroutine(HealthLoss(survivalInterval));
            healthCoroutineRunning = true;
            Debug.Log("Starting Player health loss.");
        }

        // Check if we can stop dropping health

        if (currentHunger > 0 && currentTemperature > 0 && currentTemperature < 100 && healthCoroutineRunning == true)
        {
            StopCoroutine(healthCoroutine);
            healthCoroutineRunning = false;
            Debug.Log("Stopping Player health loss.");
        }

    }

    IEnumerator HungerLoss(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            UpdateHunger(hungerIntervalLoss);
        }
    }

    IEnumerator TemperatureLoss(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            UpdateTemperature(temperatureIntervalLoss);
        }
    }

    IEnumerator HealthLoss(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            UpdateHealth(healthIntervalLoss);
        }
    }

}
