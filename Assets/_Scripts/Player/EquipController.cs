using UnityEngine;

public class EquipController : MonoBehaviour 
{
    [SerializeField]
    private float _throwRange;
    [SerializeField]
    private Transform _mouse;

    //TODO 
    //* 1 - EARTH
    //* 2 - WATER
    //* 3 - FIRE
    //* 4 - AIR
    //* 5 - HEAL
    [SerializeField]
    private Potion[] _potions = new Potion[5]; 

    private InputManager _inputs;
    private int _currentSlotEquipped = 1;
    private int currentSlotEquipped
    {
        get => _currentSlotEquipped;
        set => _currentSlotEquipped = Mathf.Clamp(value, 1, 5);
    }

    private Potion _activePotion;
    private void Awake()
    {
        _inputs = FindObjectOfType<InputManager>();
    }

    private void Start()
    {
        foreach(var potion in _potions)
        {
            potion.gameObject.SetActive(false);
        }
        GetPotion();
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

    void OnDrawGizmos()
    {
        var direction =  _mouse.position- transform.position;
        direction.Normalize();
        Gizmos.DrawLine(transform.position, _mouse.position);
    }

    private void GetPotion()
    {
        _potions[currentSlotEquipped - 1].gameObject.SetActive(true);
        if (currentSlotEquipped == 5)
        {
            _activePotion = _potions[currentSlotEquipped - 1].gameObject.GetComponent<HealingPotion>();
            
        }
        else 
        {
            _activePotion = _potions[currentSlotEquipped - 1].gameObject.GetComponent<DamagePotion>();
        }
    }

    //* temp
    private void RemovePotion()
    {
        _potions[currentSlotEquipped - 1].gameObject.SetActive(false);
    }

#region -== INPUTS ==-
    private void ScrollWheelInput(float value)
    {
        if (_inputs == null) return;

        if (value > 0)
        {
            RemovePotion();
            currentSlotEquipped = _currentSlotEquipped == 5 ? currentSlotEquipped = 1 : ++currentSlotEquipped;
            GetPotion();
        } 
        else if (value < 0) 
        {
            RemovePotion();
            currentSlotEquipped = _currentSlotEquipped == 1 ? currentSlotEquipped = 5 : --currentSlotEquipped;
            GetPotion();
        }
        else 
        {
            
#if UNITY_EDITOR
            Debug.Log($"Current slot equipped value: {currentSlotEquipped}");
#endif
        }
    }

    private void EquipPotionInput(float value, int slotNumber)
    {
        if (_inputs == null || _currentSlotEquipped == slotNumber) return;

        RemovePotion();
        currentSlotEquipped = slotNumber;
        GetPotion();
    }

    private void ThrowInput(float value)
    {
        if (_inputs == null || value == 0) return; 
        
        _activePotion.onThrowDelegate?.Invoke(_mouse.position, _throwRange);
    }


#endregion
}