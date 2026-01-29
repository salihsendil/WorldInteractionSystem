public class OnInventoryItemCollectedSignal
{
    public InventoryItemSO ItemData;
    public int Quantity;

    public OnInventoryItemCollectedSignal(InventoryItemSO ıtemData, int quantity)
    {
        ItemData = ıtemData;
        Quantity = quantity;
    }
}