using UnityEngine;

[CreateAssetMenu(fileName = "New InventoryItemSO", menuName = "Scriptable Objects/New Inventory Item SO")]
public class InventoryItemSO : ScriptableObject
{
    public string ItemID;
    public GameObject ItemPrefab;
}
