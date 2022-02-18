using UnityEngine;
using UnityEngine.InputSystem;
using PixelCrew.Creatures;

public class InputActiveHero : MonoBehaviour
{
    [SerializeField]
    private Hero _hero;

    public void OnHorizontalMovement(InputAction.CallbackContext context)
    {
        //������������ ������
        var direction = context.ReadValue<Vector2>();
        _hero.SetDirection(direction);
    }


    public void OnInteract(InputAction.CallbackContext context)
    {
        //canceled - ���� ��������� �������
        if (context.canceled)
        {
            _hero.Ineract();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _hero.Attack();
        }
    }
}
