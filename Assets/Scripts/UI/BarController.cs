using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour {

    [SerializeField]
    private Image[] resourseArray;

    private int currentValue;

    public int CurrentValue {
        get {
            return currentValue;
        }

        set {
            SetValue(value);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        

    }

    public void SetValue(int value) {

        currentValue = value;

        foreach (Image image in resourseArray) {
            image.enabled = false;
        }

        for (int i = 0; i < currentValue; i++) {
            resourseArray[i].enabled = true;
        }

    }

}
