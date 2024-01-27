using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uploadscore : MonoBehaviour 
{
    public InputField username;
    public void OnEnable()
    {
        HighScores.UploadScore(username.text,PlayerPrefs.GetInt("highscore"));
    }
}
