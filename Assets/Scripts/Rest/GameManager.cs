using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Transform pauseMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (pauseMenu.gameObject.activeInHierarchy) { 

                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1;

            } else {

                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0;

            }

        }

    }

    public void ResumeGame() {

        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;

    }

    public void RestartGame() {

        Time.timeScale = 1;
        SceneManager.LoadScene(1);

    }

    public void ToMainMneu() {

        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }

}
