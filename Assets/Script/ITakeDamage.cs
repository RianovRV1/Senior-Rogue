using UnityEngine;
/// <summary>
/// take damage interface
/// </summary>

public interface ITakeDamage
{
    void TakeDamage(int damage, GameObject instigator);
}

