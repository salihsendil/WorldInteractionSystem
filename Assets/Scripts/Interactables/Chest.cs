using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Chest : BaseInteractableHold
{
    private DiContainer m_diContainer;
    [SerializeField] private List<InventoryItemSO> m_items = new();
    [SerializeField] private float m_spawnRadiusOffset = 1f;
    [SerializeField] private float m_spawnRadius = 2f;

    [Inject]
    private void Constructor(DiContainer diContainer)
    {
        m_diContainer = diContainer;
    }

    protected override void InteractComplete()
    {
        base.InteractComplete();
        foreach (var item in m_items)
        {
            float randomXPos = Random.Range(-m_spawnRadius, m_spawnRadius);
            float randomZPos = Random.Range(-m_spawnRadius, m_spawnRadius);
            Vector3 randomPos = new Vector3(randomXPos + m_spawnRadiusOffset, 0f, randomZPos + m_spawnRadiusOffset);
            GameObject gameObject = m_diContainer.InstantiatePrefab(item.ItemPrefab, randomPos + transform.position, Quaternion.identity, transform);
        }
    }
}
