using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/ Item")]
public class Item : ScriptableObject
{
    public float value = 5f;
    public string name = "Item";
}
