using UnityEngine.Events;

public class EventManager
{
    public static UnityEvent OnFoodEat = new UnityEvent();

    public static void SendFoodEat()
    {
        OnFoodEat?.Invoke();
    }
}