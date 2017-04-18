using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stat {

    [SerializeField]
    private BarController bar;
    [SerializeField]
    private int maxValue;
    [SerializeField]
    private int currentValue;

    public int CurrentValue {
        get {
            return currentValue;
        }

        set {
            if (value >= 0 && value <= maxValue) {
                currentValue = value;
                bar.CurrentValue = currentValue; 
            }
        }
    }

    public void Init() {
        CurrentValue = currentValue;
    }

}
