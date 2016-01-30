using UnityEngine;
using System.Collections;

public abstract class Screen : MonoBehaviour {

    public abstract void InitScreen();
    public abstract void ChangeScreen();
    public abstract void KillScreen();
}