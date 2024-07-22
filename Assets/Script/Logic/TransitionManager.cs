using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{

    private CanvasGroup fadeCanvasGroup;
    public bool isFade = false;

    private void OnEnable()
    {
        EventHandler.Button_HomeToMap += OnButton_HomeToMap;
        EventHandler.Button_MaptoHome += OnButton_MapToHome;
        EventHandler.Button_ToENDGame += OnButton_ToEndGame;
        EventHandler.Button_MaptoPark += OnButton_MapToPark;
    }

    private void OnDisable()
    {
        EventHandler.Button_HomeToMap -= OnButton_HomeToMap;
        EventHandler.Button_MaptoHome -= OnButton_MapToHome;
        EventHandler.Button_ToENDGame -= OnButton_ToEndGame;
        EventHandler.Button_MaptoPark -= OnButton_MapToPark;
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

        yield return TransitionFadeCanvas(1, 1f);

        EventHandler.CallBeforeSceneLoad();
        //��ж�����еĳ���
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        //���ز�����Ŀ�곡��
        yield return LoadSceneSetActive(sceneName);

        EventHandler.CallAfterSceneLoad();

        yield return new WaitForSeconds(0.01f);

        EventHandler.CallAfterSceneLoadMove();

        yield return TransitionFadeCanvas(0, 0.05f);
    }

    IEnumerator TransitionFadeCanvas(float targetAlpha, float transitionFadeDuration)
    {
        isFade = true;
        fadeCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / transitionFadeDuration;

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            //yield return new WaitForSeconds(0.01f);
            yield return null;
        }
        isFade = false;
        fadeCanvasGroup.blocksRaycasts = false;

        yield return new WaitForSeconds(1.5f);
    }


    //��ť����ת��
    private void OnButton_HomeToMap()
    {
        StartCoroutine(Transition("Map"));
    }

    private void OnButton_MapToHome()
    {
        StartCoroutine(Transition("Home"));
    }

    private void OnButton_MapToPark()
    {
        StartCoroutine(Transition("SafetyArea"));
    }

    private void OnButton_ToEndGame()
    {
        StartCoroutine(Transition("EndScene"));
    }

}
