using UnityEngine;
using UnityEngine.Events;

public static class IsoPetEvents 
{
    public static ButtonEvent GiveValueToPet = new ButtonEvent();
    
}


public class ButtonEvent : UnityEvent<ButtonEventData> { }

public class ButtonEventData 
{
    public Animator buttonAnimator; 
    public PetInfo petTarget;
    public Actions action;

    public ButtonEventData(Animator _buttonAnimator, PetInfo _petTarget, Actions _action)
    {
        this.buttonAnimator = _buttonAnimator;
        this.petTarget = _petTarget;
        this.action = _action;
    }

}
