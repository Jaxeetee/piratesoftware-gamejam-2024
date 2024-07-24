using UnityEngine;

public class EquipController : MonoBehaviour 
{
    [SerializeField]
    private float _throwRange;

    //TODO 
    [SerializeField]
    private Potion[] _potions; 

    private InputManager _inputs;
    private int _currentSlotEquipped = 1;

    private int currentSlotEquipped
    {
        get => _currentSlotEquipped;
        set => _currentSlotEquipped = Mathf.Clamp(value, 1, 5);
    }


    private void Awake()
    {
        _inputs = FindObjectOfType<InputManager>();
    }

    private void OnEnable()
    {
        _inputs.onMouseScrolled += ScrollWheelInput;
        _inputs.onEquip += EquipPotionInput;
        _inputs.onMouseClicked += ThrowInput;
    }

    private void OnDisable()
    {
        _inputs.onMouseScrolled -= ScrollWheelInput;
        _inputs.onEquip -= EquipPotionInput;
        _inputs.onMouseClicked -= ThrowInput;
    }


#region -== INPUTS ==-
    private void ScrollWheelInput(float value)
    {
        if (_inputs == null) return;

        if (value == 120)
        {
            currentSlotEquipped = _currentSlotEquipped == 5 ? currentSlotEquipped = 1 : ++currentSlotEquipped;
        } 
        else if (value == -120) 
        {
            currentSlotEquipped = _currentSlotEquipped == 1 ? currentSlotEquipped = 5 : --currentSlotEquipped;
        }
        else 
        {
            Debug.Log($"Current slow equipped value: {currentSlotEquipped}");
        }
    }

    private void EquipPotionInput(float value, int slotNumber)
    {
        if (_inputs == null) return;

        if (_currentSlotEquipped == slotNumber) return;
        currentSlotEquipped = slotNumber;
    }

    private void ThrowInput(float value)
    {
        if (_inputs == null) return; 


    }


#endregion
}