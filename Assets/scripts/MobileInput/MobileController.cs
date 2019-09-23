using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image JoystickBg;
    [SerializeField]
    private Image Joystick;
    [HideInInspector]
    public Vector2 inputvector; //получение координат вектора


    private void Start()
    {
        JoystickBg = GetComponent<Image>();
        Joystick = transform.GetChild(0).GetComponent<Image>();
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputvector = Vector2.zero;
        Joystick.rectTransform.anchoredPosition = Vector2.zero;
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(JoystickBg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / JoystickBg.rectTransform.sizeDelta.x);// получение координат касания на джостике
            pos.y = (pos.y / JoystickBg.rectTransform.sizeDelta.y);// получение координат касания на джостике


            inputvector = new Vector2(pos.x * 2 - 0, pos.y * 2 - 0);﻿//установка точных координат из касания
            inputvector = (inputvector.magnitude > 1.0f) ? inputvector.normalized : inputvector;

            Joystick.rectTransform.anchoredPosition = new Vector2(inputvector.x * (JoystickBg.rectTransform.sizeDelta.x / 3), inputvector.y * (JoystickBg.rectTransform.sizeDelta.y / 3));
        }
    }
    public float Horizontal()
    {
        if (inputvector.x != 0) return inputvector.x;
        else return Input.GetAxis("Horizontal");

    }
    public float Vertical()
    {
        if (inputvector.y != 0) return inputvector.y;
        else return Input.GetAxis("Vertical");
    }
}
