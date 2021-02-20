using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Custom/Consumable")]
public class Consumable : ScriptableObject
{

	new public string name = "New Consumable";

	public int hungerGain;
	public int temperatureGain;

	// Called when the item is activated
	public virtual void Use()
	{
		// Heal the player
		// SurvivalSystem survivalSystem = SurvivalSystem.instance.playerStats;

		SurvivalSystem.instance.UpdateHunger(hungerGain);
		SurvivalSystem.instance.UpdateTemperature(temperatureGain);

		Debug.Log(name + " consumed.");
	}
}
