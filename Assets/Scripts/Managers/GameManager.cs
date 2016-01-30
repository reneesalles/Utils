using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private static GameManager instance;
    private EnumUtils.EScreenState currentScreen;

	public static GameManager Instance {
		get {
			if(instance == null) {
				if(!GameObject.Find("GameManager"))
					instance = new GameObject("GameManager").AddComponent<GameManager>();
				else
					instance = GameObject.Find("GameManager").GetComponent<GameManager>();
			}
			return instance;
		}
	}

    public EnumUtils.EScreenState CurrentScreen {
        get {
            return currentScreen;
        }
        set {
            currentScreen = value;
        }
    }

	void Awake() {
		if((instance) && (instance.GetInstanceID() != GetInstanceID()))
			DestroyImmediate(gameObject);
		else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}
}