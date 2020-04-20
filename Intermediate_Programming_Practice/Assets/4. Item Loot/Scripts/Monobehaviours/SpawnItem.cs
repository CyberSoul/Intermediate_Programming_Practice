using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour, ISpawns
{
    public ItemPickUps_SO[] m_itemDefinitions;

    private int m_whichToSpawn = 0;
    private int m_totalSpawnWeight = 0;
    private int m_chosen = 0;

    public Rigidbody m_itemSpawned { get; set; }
    public Renderer m_itemMaterial { get; set; }
    public ItemPickUp m_itemType { get; set; }

    void Start()
    {
        foreach (ItemPickUps_SO ip in m_itemDefinitions)
        {
            m_totalSpawnWeight += ip.spawnChanceWeight;
        }
    }

    public void CreateSpawn()
    {
        foreach (ItemPickUps_SO ip in m_itemDefinitions)
        {
            m_whichToSpawn += ip.spawnChanceWeight;
            if (m_whichToSpawn >= m_chosen)
            {
                m_itemSpawned = Instantiate(ip.itemSpawnObject, transform.position, Quaternion.identity);
                m_itemMaterial = m_itemSpawned.GetComponent<Renderer>();
                m_itemMaterial.material = ip.itemMaterial;

                m_itemType = m_itemSpawned.GetComponent<ItemPickUp>();
                m_itemType.itemDefinition = ip;
                break;
            }
        }

    }
}
