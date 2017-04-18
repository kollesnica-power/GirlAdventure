using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

    [SerializeField]
    private Texture2D fadeTexture;
    [SerializeField]
    private float alpha;
    [SerializeField]
    private float fadeSpeed;

    private const int drawDepth = -1000;
    private int fadeDirection = -1;

    private void OnGUI() {

        alpha += fadeDirection * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp(alpha, 0.0f, 0.8f);

        GUI.color = new Color(0, 0, 0, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);

    }

    public void FadeScreen(int direction) {
        fadeDirection = direction;
    }
}
