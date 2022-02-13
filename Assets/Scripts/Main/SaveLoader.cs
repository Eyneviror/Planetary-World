using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class SaveLoader
{
    public void SaveHighScore(float scoreTime)
    {
        PlayerPrefs.SetFloat("Highscore", scoreTime);
    }
    public float LoadHighScore()
    {
        return PlayerPrefs.GetFloat("Highscore", 0);
    }
}