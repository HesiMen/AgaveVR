using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public CraftingComponent activatedComponentOne;
    public CraftingComponent activatedComponentTwo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateComponent(CraftingComponent component)
    {
        if (activatedComponentOne == null)
        {
            activatedComponentOne = component;
        }
        else if (activatedComponentTwo == null)
        {
            activatedComponentTwo = component;
        }
    }
    public void DeactivateComponent(CraftingComponent component)
    {
        if (activatedComponentOne == component)
        {
            activatedComponentOne = null;
        }
        else if (activatedComponentTwo == component)
        {
            activatedComponentTwo = null;
        }
    }

    public void CraftItem(CraftingComponent componentOne, CraftingComponent componentTwo, CraftableItem item)
    {
        DeactivateComponent(componentOne);
        DeactivateComponent(componentTwo);

        GameObject gameObjectOne = componentOne.gameObject;
        GameObject gameObjectTwo = componentTwo.gameObject;
        Vector3 newPos = gameObjectOne.transform.position;

        var craftedPrefab = Resources.Load("Crafting/" + item.name);
        GameObject craftedItem = Instantiate(craftedPrefab) as GameObject;
        craftedItem.gameObject.transform.position = new Vector3(newPos.x, newPos.y, newPos.z);
        
        gameObjectOne.GetComponent<XROffsetGrabInteractable>().colliders.Clear();
        Destroy(gameObjectOne);
        gameObjectTwo.GetComponent<XROffsetGrabInteractable>().colliders.Clear();
        Destroy(gameObjectTwo);
    }
}
