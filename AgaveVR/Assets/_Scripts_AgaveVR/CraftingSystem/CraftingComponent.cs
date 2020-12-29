using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CraftingComponent : MonoBehaviour
{
    public CraftingManager manager;
    [SerializeField]
    private List<CraftableItem> craftableItems;
    [SerializeField]
    private string ComponentNameString;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Crafting Manager").GetComponent<CraftingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        manager.ActivateComponent(this);
    }

    public void Deactivate()
    {
        manager.DeactivateComponent(this);
    }

    public void AttemptCraft()
    {
        // Loop through list of craftable items and check if this + the other component are both activated
        if (manager.activatedComponentOne == this || manager.activatedComponentTwo == this)
        {
            foreach (CraftableItem item in craftableItems)
            {
                if(this.ComponentNameString == item.componentOne.ComponentNameString || this.ComponentNameString == item.componentTwo.ComponentNameString)
                {
                    if (manager.activatedComponentOne.ComponentNameString == item.componentOne.ComponentNameString &&
                        manager.activatedComponentTwo.ComponentNameString == item.componentTwo.ComponentNameString)
                    {
                        manager.CraftItem(manager.activatedComponentOne, manager.activatedComponentTwo, item);
                    }
                    else if (manager.activatedComponentOne.ComponentNameString == item.componentTwo.ComponentNameString && 
                        manager.activatedComponentTwo.ComponentNameString == item.componentOne.ComponentNameString)
                    {
                        manager.CraftItem(manager.activatedComponentOne, manager.activatedComponentTwo, item);
                    }
                }
            }
        }
    }

}
