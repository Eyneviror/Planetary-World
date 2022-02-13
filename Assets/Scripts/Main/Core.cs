using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Core : MonoBehaviour
{
	private float gameTime;
	private float gameTimeHighscore;

	[SerializeField] private bool gameActive;
	[Header("Main Components")]
	[SerializeField] private Turret turret;
	[SerializeField] private CameraMovement cameraMovement;
	[SerializeField] private UI ui;
	[SerializeField] private Rocket rocket;
	[SerializeField] private Planet planet;

	[Header("Spawners")]
	[SerializeField] private EnemySpawner enemySpawner;
	[SerializeField] private PickupSpawner pickupSpawner;

	[Header("Factory cfg")]
	[SerializeField] EnemyFactoryConfig enemyFactoryConfig;

	private SaveLoader saveLoader;


	private Context coreStateMashine;
	//Factories
	EnemyFactory enemyFactory;

	//handlers
	private IHandlerEvent pickupHandler;

	private void Awake()
	{
		//Инициализация
		saveLoader = new SaveLoader();
		DamageType.Init();
		enemyFactory = new EnemyFactory(enemyFactoryConfig);
		pickupHandler = new PickupHandlerEvents(turret, planet, rocket);
		ui.Init(planet.Health);
		cameraMovement.Init();
		enemySpawner.Init(enemyFactory);
		pickupSpawner.Init();
		gameTimeHighscore = saveLoader.LoadHighScore();
		coreStateMashine = new Context();
		coreStateMashine.TransitionTo(new MenuState());
	}
    private void LateUpdate()
    {
		coreStateMashine.LateUpdateState();
    }

    private void Update()
	{
		coreStateMashine.UpdateState();
	}
	public void StartGame()
	{
		coreStateMashine.TransitionTo(
			new GameActiveState(rocket, ui, gameTimeHighscore, cameraMovement, enemySpawner, planet));
	}
	public void EndGame()
	{
		UpdateBestRecord();
		saveLoader.SaveHighScore(gameTimeHighscore);
		coreStateMashine.TransitionTo(new GameOverState());
	}
    private void OnEnable()
    {
		ui.OnPlay += cameraMovement.TransitionToGameView;
		cameraMovement.OnGameReadyToStart += StartGame;
		planet.OnHPChanged += ui.SetPlanetHealthBarValue;

		GlobalEvents.Subscribe<PlanetaryWorld.Events.OnDefeatObjectNotHeath>(OnDefeatObjectNotHeathHandler);
		pickupHandler.Subscribe();
	}
    private void OnDisable()
    {
		ui.OnPlay -= cameraMovement.TransitionToGameView;
		cameraMovement.OnGameReadyToStart -= StartGame;
		planet.OnHPChanged -= ui.SetPlanetHealthBarValue;


		GlobalEvents.Unsubscribe<PlanetaryWorld.Events.OnDefeatObjectNotHeath>(OnDefeatObjectNotHeathHandler);
		pickupHandler.Unsubsribe();
	}
	private void OnDefeatObjectNotHeathHandler(PlanetaryWorld.Events.OnDefeatObjectNotHeath ev)
    {
		EndGame();
    }
    private void UpdateBestRecord()
    {
		if (gameTime>gameTimeHighscore)
        {
			gameTimeHighscore = gameTime;
        }
    }
}
