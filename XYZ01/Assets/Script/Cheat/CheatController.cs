using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Events;

public class CheatController : MonoBehaviour
{
    [SerializeField] float _inputTimeToLive; //����� ����� ������ ������
    [SerializeField] CheatItem[] _cheats; //������ �����

    string _currentInput;
    float _inputTime;

    private void Awake()
    {
        //�������� �� ������� ����� ������ � ����������
        Keyboard.current.onTextInput += OnTextInput;
    }

    private void OnDestroy()
    {
        //���������� �� ������� �� ����� � ����������
        Keyboard.current.onTextInput -= OnTextInput;
    }

    void OnTextInput(char inputChar)
    {
        //����� ���� ����� ��������� � ������
        _currentInput += inputChar;
        //����� ������� �� ����� ������
        _inputTime = _inputTimeToLive;
        FindAnyCheats();
    }

    void FindAnyCheats()
    {
        //������� �������� 
        foreach(var cheatItem in _cheats)
        {
            if(_currentInput.Contains(cheatItem.Name))
            {
                //����� event
                cheatItem.Action.Invoke();
                //����� 
                _currentInput = string.Empty;
            }
        }
    }

    private void Update()
    {
        //������ ������
        if (_inputTime < 0)
        {
            _currentInput = string.Empty;
        }
        else
        {
            _inputTime -= Time.deltaTime;
        }
    }
}


//���� ������ ������ [SerializeField]
[Serializable]
public class CheatItem
{
    public string Name;
    public UnityEvent Action;
}
