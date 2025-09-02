using UnityEngine;


[CreateAssetMenu(fileName = "Delivery", menuName = "ScriptableObjects/Delivery", order = 1)]
public class QuestDeliverySCO : ScriptableObject
{
    public string title;
    public string description;
    public GameObject[] objects;
}
