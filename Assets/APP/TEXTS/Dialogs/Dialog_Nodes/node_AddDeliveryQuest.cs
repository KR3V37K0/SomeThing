using UnityEngine;

public class node_AddDeliveryQuest: Base
{
    [SerializeField] QuestDeliverySCO quest;
    [SerializeField] string table;
    public override void Execute()
    {
        DeliveryManager.AddQuest(quest, table);
        EventBus.act_Next_Speach?.Invoke();
        base.Execute();
    }
}
