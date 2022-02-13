using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class PlayerInputMobile: MonoBehaviour
{
    [SerializeField] private Rocket rocket;
    [SerializeField] private MovementButton leftBtn;
    [SerializeField] private MovementButton rightBtn;

    [SerializeField] private float aceleration;
    [SerializeField] private float acelerationStop;
    private bool canMove;
    private float sensetivy;
    private float direction;

    

    private void OnEnable()
    {
        leftBtn.OnPressed += LeftBtnPressedHandler;
        rightBtn.OnPressed += RightBtnPressedHandler;
    }
    private void OnDisable()
    {
        leftBtn.OnPressed -= LeftBtnPressedHandler;
        rightBtn.OnPressed -= RightBtnPressedHandler;
    }

    private void Update()
    {
        sensetivy = Mathf.Clamp(sensetivy, -1, 1);
        if (canMove)
        {
            sensetivy += Time.deltaTime * direction * aceleration;
            rocket.RotateRocket(sensetivy);
            
        }
        else
        {
            // sensetivy = Mathf.Lerp(sensetivy, 0, 1);
            sensetivy = Mathf.MoveTowards(sensetivy, 0, Time.deltaTime * acelerationStop);
            rocket.RotateRocket(sensetivy);
        }
    }
    private void LeftBtnPressedHandler(bool isPressed)
    {
        canMove = isPressed;
        direction = -1;
    }

    private void RightBtnPressedHandler(bool isPressed)
    {
        canMove = isPressed;
        direction = 1;
    }
}

