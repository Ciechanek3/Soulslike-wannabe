using UnityEditor;

[CustomPropertyDrawer(typeof(StrategyAttribute))]
public class StrategySelectorDrawer : TypeSelectorDrawer<IStrategy>
{
}
