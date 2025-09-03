using UnityEngine;


[CreateAssetMenu(fileName = "Delivery", menuName = "ScriptableObjects/Delivery", order = 1)]
public class QuestDeliverySCO : ScriptableObject
{
    public string title;
    public string description;
    public GameObject[] objects;

}

public class QuestDeliveryJson
{
    public string title;
    public string description;
    public GameObject[] objects;
    public QuestDeliveryJson(string title, string description, GameObject[] objects)
    {
        this.title = title;
        this.description = description;
        this.objects = objects;
    }
}
