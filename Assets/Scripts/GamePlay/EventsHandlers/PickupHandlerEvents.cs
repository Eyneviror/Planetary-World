using PlanetaryWorld.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class PickupHandlerEvents : IHandlerEvent
{
    private Turret turret;
    private Planet planet;
    private Rocket rocket;

    public PickupHandlerEvents(Turret t, Planet p, Rocket r)
    {
        turret = t;
        planet = p;
        rocket = r;
    }
    public void Subscribe()
    {
        GlobalEvents.Subscribe<PlanetaryWorld.Events.OnPickupAplayed>(OnPickupAplayed);
    }
    public void Unsubsribe()
    {
        GlobalEvents.Unsubscribe<PlanetaryWorld.Events.OnPickupAplayed>(OnPickupAplayed);
    }

    public void OnPickupAplayed(PlanetaryWorld.Events.OnPickupAplayed ev)
    {
        switch (ev.type)
        {
            case PickupType.Turret:
                SetTurret();
                break; 
            case PickupType.SpeedFire:
                rocket.ActivateSpeedFire();
                break;
            case PickupType.PlanetShield:
                SetTempPlanetShield();
                break;
        }
    }
    private void SetTurret()
    {
        if (!turret.isActiveAndEnabled)
        {
            turret.gameObject.SetActive(true);
            turret.transform.eulerAngles = Vector3.zero;
            turret.Init(true);

        }
    }
    private void SetTempPlanetShield()
    {
        planet.CreateShield();
    }


}