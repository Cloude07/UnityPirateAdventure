using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Events;

public class CheatController : MonoBehaviour
{
    [SerializeField] float _inputTimeToLive; //время жизни записи кнопки
    [SerializeField] CheatItem[] _cheats; //список читов

    string _currentInput;
    float _inputTime;

    private void Awake()
    {
        //потписка на события ввода текста с клавиатуры
        Keyboard.current.onTextInput += OnTextInput;
    }

    private void OnDestroy()
    {
        //отписались на события от ввода с клавиатуры
        Keyboard.current.onTextInput -= OnTextInput;
    }

    void OnTextInput(char inputChar)
    {
        //Любой ввод знака сохраняем в строку
        _currentInput += inputChar;
        //сброс времяни до ввода строки
        _inputTime = _inputTimeToLive;
        FindAnyCheats();
    }

    void FindAnyCheats()
    {
        //перебор элемента 
        foreach(var cheatItem in _cheats)
        {
            if(_currentInput.Contains(cheatItem.Name))
            {
                //вызов event
                cheatItem.Action.Invoke();
                //сброс 
                _currentInput = string.Empty;
            }
        }
    }

    private void Update()
    {
        //таймер сброса
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


//дает классу делать [SerializeField]
[Serializable]
public class CheatItem
{
    public string Name;
    public UnityEvent Action;
}
