using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{

    private void Awake()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        StartCoroutine(LoadSceneandSetActive("StartScene"));
    }

    IEnumerator LoadSceneandSetActive(string sceneName)
    {
        //通过场景名字加载场景
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);
    }

    private CanvasGroup fadeCanvasGroup;
    //public bool isFade = false;

    private void OnEnable()
    {
        EventHandler.Button_HomeToMap += OnButton_HomeToMap;
        EventHandler.Button_MaptoHome += OnButton_MapToHome;
        EventHandler.Button_ToENDGame += OnButton_ToEndGame;
        EventHandler.Button_MaptoPark += OnButton_MapToPark;
        EventHandler.Button_MaptoRB += OnButton_MaptoRB;
        EventHandler.Button_ToWarY += OnButton_MaptoWarY;
        EventHandler.Button_ToIntroduceScene += OnButton_ToIntroduceScene;
        EventHandler.Button_ToChurch += OnButton_ToChurch;
        EventHandler.Button_ToStart += OnButton_ToStart;

    }

    private void OnDisable()
    {
        EventHandler.Button_HomeToMap -= OnButton_HomeToMap;
        EventHandler.Button_MaptoHome -= OnButton_MapToHome;
        EventHandler.Button_ToENDGame -= OnButton_ToEndGame;
        EventHandler.Button_MaptoPark -= OnButton_MapToPark;
        EventHandler.Button_MaptoRB -= OnButton_MaptoRB;
        EventHandler.Button_ToWarY -= OnButton_MaptoWarY;
        EventHandler.Button_ToIntroduceScene -= OnButton_ToIntroduceScene;
        EventHandler.Button_ToChurch -= OnButton_ToChurch;
        EventHandler.Button_ToStart -= OnButton_ToStart;
    }

    //加载场景并将其激活
    IEnumerator LoadSceneSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        //获得当前已经加载的场景
        Scene newScene = SceneManager.GetSceneByName(sceneName);

        //激活
        SceneManager.SetActiveScene(newScene);
    }

    private void Start()
    {
        //StartCoroutine(LoadSceneSetActive())

        fadeCanvasGroup = GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<CanvasGroup>();
    }

    IEnumerator Transition(string sceneName)
    {
        fadeCanvasGroup.blocksRaycasts = true;
        yield return TransitionFadeCanvas(1, 1f);

        EventHandler.CallBeforeSceneLoad();
        //先卸载现有的场景
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        //加载并激活目标场景
        yield return LoadSceneSetActive(sceneName);

        EventHandler.CallAfterSceneLoad();

        yield return new WaitForSeconds(0.05f);

        EventHandler.CallAfterSceneLoadMove();

        yield return TransitionFadeCanvas(0, 0.05f);
        fadeCanvasGroup.blocksRaycasts = false;
    }

    IEnumerator TransitionFadeCanvas(float targetAlpha, float transitionFadeDuration)
    {
        //isFade = true;
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / transitionFadeDuration;
        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            //yield return new WaitForSeconds(0.01f);
            yield return null;
        }
        //isFade = false;
        if (targetAlpha > 0.6)
        {
            yield return new WaitForSeconds(1.5f);
        }
        
    }


    //按钮场景转换
    private void OnButton_HomeToMap()  //去地图
    {
        StartCoroutine(Transition("Map"));
    }

    private void OnButton_MapToHome() //去家里
    {
        StartCoroutine(Transition("Home"));
    }

    private void OnButton_MapToPark()  //去城市广场
    {
        StartCoroutine(Transition("SafetyArea"));
    }

    private void OnButton_MaptoRB()   //去科研基地
    {
        StartCoroutine(Transition("ResearchBase"));
    }

    private void OnButton_MaptoWarY()   //去战乱荒地
    {
        StartCoroutine(Transition("WarYield"));
    }

    private void OnButton_ToIntroduceScene()  //去导入Scene
    {
        StartCoroutine(Transition("IntroduceScene"));
    }

    private void OnButton_ToChurch()
    {
        StartCoroutine(Transition("Church"));
    }


    private void OnButton_ToEndGame()
    {
        StartCoroutine(Transition("EndScene"));
    }

    private void OnButton_ToStart()
    {
        StartCoroutine(Transition("StartScene"));
    }

}
