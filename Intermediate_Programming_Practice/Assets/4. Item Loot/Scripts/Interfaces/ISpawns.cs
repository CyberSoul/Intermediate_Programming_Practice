using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawns
{
    Rigidbody m_itemSpawned { get; set; }

    Renderer m_itemMaterial { get; set; }

    ItemPickUp m_itemType { get; set; }

    void CreateSpawn();
}
