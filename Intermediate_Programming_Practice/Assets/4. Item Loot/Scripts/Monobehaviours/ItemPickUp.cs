using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemPickUps_SO itemDefinition;

    public CharacterStats m_charStats;
    CharacterInventory m_charInventory;

    GameObject m_foundStats;

    #region Constructors
    public ItemPickUp()
    {
        m_charInventory = CharacterInventory.instance;
    }
    #endregion

    private void Start()
    {
        if (m_charStats != null)
        {
            m_foundStats = GameObject.FindGameObjectWithTag("Player");
            m_charStats = m_foundStats.GetComponent<CharacterStats>();
        }
    }

    void StoreItemInInventory()
    {
        m_charInventory.StoreItem(this);
    }

    public void UseItem()
    {
        switch (itemDefinition.itemType)
        {
            case ItemTypeDefinitions.HEALTH:
                {
                    m_charStats.ApplyHealth(itemDefinition.itemAmount);
                }
                break;
            case ItemTypeDefinitions.MANA:
                {
                    m_charStats.ApplyMana(itemDefinition.itemAmount);
                }
                break;
            case ItemTypeDefinitions.WEALTH:
                {
                    m_charStats.GiveWealth(itemDefinition.itemAmount);
                }
                break;
            case ItemTypeDefinitions.WEAPON:
                {
                    m_charStats.ChangeWeapon(this);
                }
                break;
            case ItemTypeDefinitions.ARMOR:
                {
                    m_charStats.ChangeArmor(this);
                }
                break;
            case ItemTypeDefinitions.BUFF:
                {
                }
                break;

            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Player")
        {
            if (itemDefinition.isStorable)
            {
                StoreItemInInventory();
            }
            else
            {
                UseItem();
            }
        }
    }
}
