using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentControl : MonoBehaviour
{
    private List<ScriptableObject> equipments;
    private List<Item> items;
    
    // Start is called before the first frame update
    void Start()
    {
        equipments = new List<ScriptableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ContainEquipment(ScriptableObject item)
    {
        return equipments.Contains(item);
    }

    public void AddEquipments(ScriptableObject item)
    {
        equipments.Add(item);
    }

    public bool ContainItem(string name)
    {
        foreach (Item item in items)
            if (item.name == name)
                return true;

        return false;
    }

    public void RemoveItem(string name)
    {
        for (int i = 0; i < items.Count; i++)
            if (items[i].name == name)
            {
                items.RemoveAt(i);
                return;
            }
    }
}
