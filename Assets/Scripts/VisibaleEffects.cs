using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class VisibaleEffects : MonoBehaviour
{
	public static IEnumerator StunRoutine(SpriteRenderer sr)
	{
		if (sr.color != Color.white)
		{
			Color defaultColour = sr.color;
			sr.color = Color.white;

			yield return new WaitForSeconds(0.05f);

			if (sr != null)
				sr.color = defaultColour;
		}
	}

	//StunEffect
}

