using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[CreateAssetMenu(fileName = "new EnemyFactory Cfg",menuName = "Game/Enemy Factory cfg")]
public class EnemyFactoryConfig: ScriptableObject
{
    public Sniper sniper;
    public Suicider suicider;
}

