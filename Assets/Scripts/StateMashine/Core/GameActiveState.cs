using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameActiveState : IState
{
    public GameActiveState(Rocket r,UI gameUi,float hightScore,CameraMovement movement,EnemySpawner es,Planet p)
    {
        rocket = r;
        ui = gameUi;
        gameTime = 0;
        gameTimeHighscore = hightScore;
        cameraMovement = movement;
        enemySpawner = es;
        planet = p;
    }

    private float gameTime;
    private float gameTimeHighscore;

    private Rocket rocket;
    private UI ui;
    private CameraMovement cameraMovement;
    private EnemySpawner enemySpawner;
    private Planet planet;
    private bool objectsCanMove;


    public void Enter(Context context)
    {
        gameTime = 0.0f;
        rocket.canMove = true;
        ui.SetGameUI(true);
        objectsCanMove = true;
        GlobalEvents.Rise(new PlanetaryWorld.Events.OnStartGame(objectsCanMove));
    }
    public void Update(Context context)
    {
        gameTime += Time.deltaTime;
        enemySpawner.UpdateTimeSpawn();
        ui.SetTimeElapsed(gameTime);
    }
    public void LateUpdate(Context context)
    {
        cameraMovement.Move();
    }
    public void Exit(Context context)
    {
        rocket.canMove = false;
        ui.SetGameUI(true);
        ui.SetGameOverUI(gameTimeHighscore, gameTime);
        rocket.rocketSprite.gameObject.SetActive(false);
        planet.gameObject.SetActive(false);
        objectsCanMove = false;
        GlobalEvents.Rise(new PlanetaryWorld.Events.OnEndGame(objectsCanMove));
    }

    
}

