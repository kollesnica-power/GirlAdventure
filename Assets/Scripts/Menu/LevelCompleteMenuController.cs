using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompleteMenuController : MenuController {

    [SerializeField]
    private Text scoreText;

	// Use this for initialization
	void Start () {

        scoreText.text = "Your score: " +  SceneProperties.score.ToString();

    }
}
