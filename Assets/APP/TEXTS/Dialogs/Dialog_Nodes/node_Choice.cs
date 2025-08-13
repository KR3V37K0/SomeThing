using System.Linq;
using UnityEngine;

public class node_Choice : Base
{
    [SerializeField] string[] choices;

    public override void Execute()
    {
        EventBus.act_Choice?.Invoke(choices);
        base.Execute();
    }

    private void UpdatePortsSafe()
    {
        // ������ ������� ������
        var currentPorts = DynamicOutputs.Select(p => p.fieldName).ToList();
        var desiredPorts = Enumerable.Range(0, choices.Length)
            .Select(i => $"Answer {i}")
            .ToList();

        // ������� ������
        foreach (var port in currentPorts.Except(desiredPorts).ToList())
        {
            RemoveDynamicPort(port);
        }

        // ��������� �����������
        foreach (var port in desiredPorts.Except(currentPorts))
        {
            AddDynamicOutput(typeof(bool),
                ConnectionType.Override,
                TypeConstraint.Inherited,
                port);
        }
    }

    protected override void Init()
    {
        base.Init();
        UpdatePortsSafe();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        UnityEditor.EditorApplication.delayCall += () =>
        {
            if (this != null) // ����� �� ������ ��� �������� ����
                UpdatePortsSafe();
        };
    }
#endif
}
