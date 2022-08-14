using System.Collections.Generic;

/// <summary>
/// Generic implementation of the Pool
/// </summary>
/// <typeparam name="T"></typeparam>
public class ServicePool<T> : GenericSingleton<ServicePool<T>> where T : class
{
    private List<PooledItem> pooledItems = new List<PooledItem>();

    public T GetItem(CharacterType characterType)
    {
        if(pooledItems.Count > 0)
        {
            PooledItem item = pooledItems.Find(it => (it.IsUsed == false) && (it.characterType == characterType));
            if(item != null)
            {
                item.IsUsed = true;
                return item.Item;
            }
        }
        return CreateNewItem(characterType);
    }

    private T CreateNewItem(CharacterType characterType)
    {
        PooledItem newItem = new PooledItem();
        newItem.Item = CreateItem();
        newItem.IsUsed = true;
        newItem.characterType = characterType;
        pooledItems.Add(newItem);
        return newItem.Item;
    }

    // override in the child classes (enemy controller, arrow controller and fireball controller)
    protected virtual T CreateItem()
    {
        return (null);
    }

    public void ReturnToPool(T item, CharacterType characterType)
    {
        PooledItem returnedItem = pooledItems.Find(it => (it.Item.Equals(item)) && (it.characterType == characterType));
        if(returnedItem != null)
        {
            returnedItem.IsUsed = false;
        }
    }

    public class PooledItem
    {
        public T Item;
        public bool IsUsed;
        public CharacterType characterType;
    }
}
