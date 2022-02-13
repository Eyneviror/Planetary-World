using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
	[SerializeField] protected int health;
	[SerializeField] protected float moveSpeed;
	[SerializeField] protected int damage;
	[SerializeField] protected bool stunned;
	[SerializeField] protected ParticleSystem deathParticleEffect;
	[SerializeField] protected SpriteRenderer sr;

	protected bool canMove;
	
	public virtual void Init()
    {
		moveSpeed *= UnityEngine.Random.Range(0.9f, 1.1f);
		canMove = true;
		GlobalEvents.Subscribe<PlanetaryWorld.Events.OnStartGame>(OnStartGameHandler);
		GlobalEvents.Subscribe<PlanetaryWorld.Events.OnEndGame>(OnEndGameHandler);
	}
	protected void LookAtPlanet ()
	{
		Vector3 dir = transform.position.normalized;
		float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
		transform.eulerAngles = new Vector3(0, 0, ang);
	}
	public virtual void Die ()
	{
		
	}

	//Called by the blue ship. Duplicates the ship.
/*	void Duplicate ()
	{
		GameObject e1 = Instantiate(duplicatePrefab.gameObject, transform.position + (transform.up * -2), Quaternion.identity);
		e1.GetComponent<Enemy>().Init();
		GameObject e2 = Instantiate(duplicatePrefab.gameObject, transform.position + (transform.right * 2), Quaternion.identity);
		e2.GetComponent<Enemy>().Init();
		GameObject e3 = Instantiate(duplicatePrefab.gameObject, transform.position + (transform.right * -2), Quaternion.identity);
		e3.GetComponent<Enemy>().Init();
	}*/
	protected IEnumerator stunnedRoutine()
	{
		stunned = true;
		yield return new WaitForSeconds(0.02f);
		stunned = false;
	}

	protected void OnStartGameHandler(PlanetaryWorld.Events.OnStartGame ev)
    {
		canMove = ev.ObjectsCanMove;
    }
	protected void OnEndGameHandler(PlanetaryWorld.Events.OnEndGame ev)
	{
		canMove = ev.ObjectsCanMove;
	}
}
