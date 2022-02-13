using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMovement : MonoBehaviour
{

	public event Action OnGameReadyToStart;

	[SerializeField] private float moveDist;
	[SerializeField] private float lerpSpeed;

	[SerializeField] private Vector3 menuCameraPos;
	[SerializeField] private float menuOrthoSize;
	[SerializeField] private float gameOrthoSize;

	private  Camera camera;
	private bool shakingCam;

    private void Start()
    {
		camera = Camera.main;
    }
    public void Init()
    {
		SetMenuView();

		GlobalEvents.Subscribe<PlanetaryWorld.Events.OnTakedDamage>(OnTakedDamgedHandler);
		GlobalEvents.Subscribe<PlanetaryWorld.Events.OnBulletHit>(OnBulletHitHandler);
	}
	public void SetMenuView()
	{
		Camera.main.transform.position = menuCameraPos;
		Camera.main.orthographicSize = menuOrthoSize;
	}

	//Transitions the camera out to see the whole game view.
	public void TransitionToGameView()
	{
		StartCoroutine(RedyRoutine());
	}
	public void Move()
	{
		transform.position = Vector3.Lerp(transform.position, Rocket.r.rocketSprite.transform.position * moveDist, lerpSpeed * Time.deltaTime);
		transform.position = new Vector3(transform.position.x, transform.position.y, -10);
	}
	private void Shake(float duration, float amount, float intensity)
	{
		if (!shakingCam)
			StartCoroutine(ShakeCam(duration, amount, intensity));
	}

    private IEnumerator ShakeCam(float dur, float amount, float intensity)
	{
		float t = dur;
		Vector3 originalPos = camera.transform.localPosition;
		Vector3 targetPos = Vector3.zero;
		shakingCam = true;

		while (t > 0.0f)
		{
			if (targetPos == Vector3.zero)
			{
				targetPos = originalPos + (UnityEngine.Random.insideUnitSphere * amount);
			}

			camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, targetPos, intensity * Time.deltaTime);

			if (Vector3.Distance(camera.transform.localPosition, targetPos) < 0.02f)
			{
				targetPos = Vector3.zero;
			}

			t -= Time.deltaTime;
			yield return null;
		}

		camera.transform.localPosition = originalPos;
		shakingCam = false;
	}
	private IEnumerator RedyRoutine()
	{
		//Move the camera to the center of the planet.
		while (camera.transform.position.x < 0.0f)
		{
			camera.transform.position = Vector3.MoveTowards(camera.transform.position, new Vector3(0, 0, -10), 8 * Time.deltaTime);
			yield return null;
		}

		yield return new WaitForSeconds(0.3f);

		//Zoom out to see the game space.
		while (camera.orthographicSize < gameOrthoSize)
		{
			camera.orthographicSize = Mathf.MoveTowards(camera.orthographicSize, gameOrthoSize, 20 * Time.deltaTime);
			yield return null;
		}

		OnGameReadyToStart?.Invoke();
	}
    private void OnDisable()
    {
		GlobalEvents.Unsubscribe<PlanetaryWorld.Events.OnTakedDamage>(OnTakedDamgedHandler);
		GlobalEvents.Unsubscribe<PlanetaryWorld.Events.OnBulletHit>(OnBulletHitHandler);
	}
	private void OnBulletHitHandler(PlanetaryWorld.Events.OnBulletHit ev)
    {
		Shake(0.1f, 0.25f, 30.0f);
	}

	private void OnTakedDamgedHandler(PlanetaryWorld.Events.OnTakedDamage ev)
    {
		Shake(0.3f, 0.5f, 50.0f);
	}
}
