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
        //ͨ���������ּ��س���
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

    //���س��������伤��
    IEnumerator LoadSceneSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        //��õ�ǰ�Ѿ����صĳ���
        Scene newScene = SceneManager.GetSceneByName(sceneName);

        //����
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
        //��ж�����еĳ���
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        //���ز�����Ŀ�곡��
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


    //��ť����ת��
    private void OnButton_HomeToMap()  //ȥ��ͼ
    {
        StartCoroutine(Transition("Map"));
    }

    private void OnButton_MapToHome() //ȥ����
    {
        StartCoroutine(Transition("Home"));
    }

    private void OnButton_MapToPark()  //ȥ���й㳡
    {
        StartCoroutine(Transition("SafetyArea"));
    }

    private void OnButton_MaptoRB()   //ȥ���л���
    {
        StartCoroutine(Transition("ResearchBase"));
    }

    private void OnButton_MaptoWarY()   //ȥս�һĵ�
    {
        StartCoroutine(Transition("WarYield"));
    }

    private void OnButton_ToIntroduceScene()  //ȥ����Scene
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
