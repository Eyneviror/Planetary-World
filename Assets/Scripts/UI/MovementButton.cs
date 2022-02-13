using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MovementButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action<bool> OnPressed;
    public void OnPointerDown(PointerEventData eventData)
    {
        OnPressed?.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPressed?.Invoke(false);
    }
}

