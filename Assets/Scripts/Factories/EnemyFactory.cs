using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class EnemyFactory
{
    private EnemyFactoryConfig config;
    public EnemyFactory(EnemyFactoryConfig cfg)
    {
        config = cfg;
    }


    public SpawnData Get(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Sniper:
                return new SpawnData()
                {
                    EnemyObject = config.sniper,
                    count = 1
                };
                break;
            case  EnemyType.Suicider:
                return new SpawnData()
                {
                    EnemyObject = config.suicider,
                    count = 4
                };
                break;
            case  EnemyType.Destroyer:
                return new SpawnData()
                {
                    EnemyObject = config.destroyer,
                    count = 1
                };
                break;
        }
        return SpawnData.Empty;
    }
    public SpawnData GetRandom()
    {
        Array values = Enum.GetValues(typeof(EnemyType));
        System.Random random = new System.Random();
        EnemyType randomEnemyType = (EnemyType)values.GetValue(random.Next(values.Length));
        return Get(randomEnemyType);
    }
}