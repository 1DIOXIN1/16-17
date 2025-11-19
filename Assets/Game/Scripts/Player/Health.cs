using UnityEngine;

public class Health
{
    private int _maxHealth;

    public Health(int _startHealth)
    {
        _maxHealth = _startHealth;
        CurrentHealth = _maxHealth;
    }

    public int CurrentHealth {get; private set;}

    public void Heal(int value)
    {
        if(value < 0)
        {
            Debug.Log("Неправильное значение хила");
            return;
        }

        CurrentHealth += value;
        Debug.Log("Текущее хп: " + CurrentHealth);
    }

    public void TakeDamage(int value)
    {
        if(value < 0)
        {
            Debug.Log("Неправильное значение урона");
            return;
        }
        
        CurrentHealth -= value;
        Debug.Log("Текущее хп: " + CurrentHealth);
    }
}
