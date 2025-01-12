using UnityEditor;

[CustomPropertyDrawer(typeof(EnemyAttribute))]
public class EnemySelectorDrawer : TypeSelectorDrawer<IEnemy>
{
}
