using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShipShield : MonoBehaviour , ICanTakeDamage
{
	public float rotateSpeed;
	public SpriteRenderer sr;

	void Update ()
	{
		transform.eulerAngles += new Vector3(0, 0, rotateSpeed * Time.deltaTime);
	}


    public void TakeDamage(Type t)
    {
		StartCoroutine(VisibaleEffects.StunRoutine(sr));
    }
}
