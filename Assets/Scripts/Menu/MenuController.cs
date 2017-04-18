﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public void LoadScene(int sceneNumber) {
        SceneManager.LoadScene(sceneNumber);
    }

    public void ExitApplication() {
        Application.Quit();
    }

}
