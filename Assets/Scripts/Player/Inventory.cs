using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Inventory : MonoBehaviour
{
    private const int MAX_SIZE = 20;
    private SignalBus m_signalBus;
    private HashSet<InventoryItem> m_items = new(MAX_SIZE);

    public HashSet<InventoryItem> Items => m_items;

    [Inject]
    private void Constructor(SignalBus signalBus)
    {
        m_signalBus = signalBus;
    }

    private void OnEnable()
    {
        m_signalBus.Subscribe<OnInventoryItemCollectedSignal>(AddItem);
    }

    private void OnDisable()
    {
        m_signalBus.Unsubscribe<OnInventoryItemCollectedSignal>(AddItem);
    }

    public void AddItem(OnInventoryItemCollectedSignal signal)
    {
        foreach (var item in m_items)
        {
            if (item.ItemData == signal.ItemData)
            {
                item.Quantity += signal.Quantity;
                return;
            }
        }
        m_items.Add(new InventoryItem(signal.ItemData, signal.Quantity));
    }

    public InventoryItem GetItem(InventoryItemSO itemData)
    {
        foreach (var item in m_items)
        {
            if (item.ItemData == itemData)
            {
                return item;
            }
        }
        return null;
    }

    public bool HasItem(InventoryItemSO itemData, int amount = 1)
    {
        foreach (var item in m_items)
        {
            if (item.ItemData.ItemID == itemData.ItemID && item.Quantity >= amount)
            {
                return true;
            }
        }
        return false;
    }

    public void ReduceItem(InventoryItemSO itemData, int amount)
    {
        foreach (var item in m_items)
        {
            if (item.ItemData == itemData && item.Quantity >= amount)
            {
                item.Quantity -= amount;
                return;
            }
        }
    }

    #region Test

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("inventory size: " + m_items.Count);
            foreach (var item in m_items)
            {
                Debug.Log(item.ItemData + " " + item.Quantity);
            }
        }
    }

    #endregion

    #region Nested Class

    [Serializable]
    public class InventoryItem
    {
        public InventoryItemSO ItemData;
        public int Quantity;

        public InventoryItem(InventoryItemSO ýtemData, int quantity)
        {
            ItemData = ýtemData;
            Quantity = quantity;
        }
    }

    #endregion
}
