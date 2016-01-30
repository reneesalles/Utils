using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class LevelManager : MonoBehaviour {

    public string nextLevelName = "03_Level_01";
    public LoadSceneMode loadMode = LoadSceneMode.Single;

    public bool isDoneLoading = false;
    public float progress = 0.0f;

    public bool didPlayerInput = false;

    private Animator thisAnimator;

	void Awake() {
		if(PlayerPrefs.HasKey(EnumUtils.NEXTLEVEL.ToString())) {
            nextLevelName = PlayerPrefs.GetString(EnumUtils.NEXTLEVEL.ToString());
        }
        if(PlayerPrefs.HasKey(EnumUtils.LOADLEVELMODE.ToString())) {
            loadMode = PlayerPrefs.GetInt(EnumUtils.LOADLEVELMODE.ToString()) == 1 ? LoadSceneMode.Additive : LoadSceneMode.Single;
        }

        thisAnimator = GetComponent<Animator>();
    }

    void Start() {
        StartCoroutine(CallLoadLevel());
    }

    void Update() {
        if(isDoneLoading) {
            // implementar uma logica onde o jogador confirma que quer de fato passar o level adiante após
            //o término do loading (com input do mouse / teclado / gamepad / touch)

            // exemplo input teclado:
            if(Input.GetKeyUp(KeyCode.Return)) {
                didPlayerInput = true;
                Debug.Log("!");
            }
        }
    }

    IEnumerator CallLoadLevel() {
        AsyncOperation ao = AsyncLoadLevel(nextLevelName, loadMode);
        ao.allowSceneActivation = false;

        while(!isDoneLoading) {
            progress = ao.progress;
            thisAnimator.SetFloat("Blend", progress);

            isDoneLoading = progress >= 0.9f;

            yield return 0;
        }

        yield return new WaitWhile(() => !didPlayerInput);

        ao.allowSceneActivation = true;

        yield return ao;
    }

    public static void LoadLevel(string levelName) {
        SceneManager.LoadScene(levelName);
    }
    public static void LoadLevel(int levelIndex) {
        SceneManager.LoadScene(levelIndex);
    }

    public static void LoadLevel(string levelName, LoadSceneMode loadMode) {
        SceneManager.LoadScene("99_LoadingScreen");

        PlayerPrefs.SetString(EnumUtils.NEXTLEVEL.ToString(), levelName);
        PlayerPrefs.SetInt(EnumUtils.LOADLEVELMODE.ToString(), loadMode == LoadSceneMode.Additive ? 1 : 0);

        PlayerPrefs.Save();
    }

    public static AsyncOperation AsyncLoadLevel(string levelName, LoadSceneMode loadMode = LoadSceneMode.Single) {
        return SceneManager.LoadSceneAsync(levelName, loadMode);
    }
    public static AsyncOperation AsyncLoadLevel(int levelIndex, LoadSceneMode loadMode = LoadSceneMode.Single) {
        return SceneManager.LoadSceneAsync(levelIndex, loadMode);
    }
}