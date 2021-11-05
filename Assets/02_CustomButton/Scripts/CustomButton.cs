using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _duration;
    
    public event Action Tap;
    public event Action LongPress;
    private Coroutine _countDownCoroutine;

    private CustomButtonModel _buttonModel;

    private void Awake()
    {
        _buttonModel = new CustomButtonModel(_duration);
        LongPress += () => print("long!!!");
        Tap += () => print("tap!");

        _buttonModel.Tapped += Tap;
        _buttonModel.LongPressed += LongPress;
        _buttonModel.Pressed += () => _animator.SetTrigger("Pressed");
        _buttonModel.Released += () => _animator.SetTrigger("Released");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _countDownCoroutine = StartCoroutine(CountDownLongPress(_duration));
        _buttonModel.Down(Time.time);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(_countDownCoroutine);
        _buttonModel.Up();
    }

    private IEnumerator CountDownLongPress(float duration)
    {
        yield return new WaitForSeconds(duration);
        _buttonModel.LongPress(Time.time);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(_countDownCoroutine);
        _buttonModel.Exit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _buttonModel.Enter();
    }
}
