using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableScript : MonoBehaviour
{
    public Consumable consumable;

	// Start is called before the first frame update
	public void Use()
	{
		consumable.Use();
		if(gameObject != null)
        {
			if(gameObject.GetComponent<AgaveObject>() != null)
            {
				gameObject.GetComponent<AgaveObject>().RemoveFromSpawner();
			}

			gameObject.SetActive(false);
		}
	}
}
