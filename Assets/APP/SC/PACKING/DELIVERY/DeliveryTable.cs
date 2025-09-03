using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryTable : MonoBehaviour, IInteractable
{
    [SerializeField] string table;
    List<QuestDeliveryJson> quests;
    public async void OnInteract()
    {
        quests = await DeliveryManager.GetQuestTo(table);
        Debug.Log("i " + table + " have " + quests.Count + " quests");
    }
}
