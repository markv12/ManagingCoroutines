using UnityEngine;
using UnityEngine.UI;

public class DemoUIManager : MonoBehaviour {
    public GameObject modalObject;
    public RectTransform modalTransform;
    public Button openButton;
    public Button closeButton;
    public float animationDuration;

    public Vector2 modalHidePos;
    public Vector2 modalShowPos;

    private bool modalOpen = false;
    private Coroutine fadeRoutine;

    void Start() {
        openButton.onClick.AddListener(delegate { OpenModal(); });
        closeButton.onClick.AddListener(delegate { CloseModal(); });
    }

    private void OpenModal() {
        if (!modalOpen) {
            modalOpen = true;
            closeButton.gameObject.SetActive(true);
            openButton.gameObject.SetActive(false);
            this.EnsureCoroutineStopped(ref fadeRoutine);
            modalObject.SetActive(true);
            Vector2 startPos = modalTransform.anchoredPosition;
            Vector2 endPos = modalShowPos;
            fadeRoutine = this.CreateAnimationRoutine(
                animationDuration,
                delegate (float progress) {
                    float easedProgress = Easing.easeInOutSine(0, 1, progress);
                    Vector2 pos = Vector2.Lerp(startPos, endPos, easedProgress);
                    modalTransform.anchoredPosition = pos;
                }
            );
        }
    }

    private void CloseModal() {
        if (modalOpen) {
            modalOpen = false;
            closeButton.gameObject.SetActive(false);
            openButton.gameObject.SetActive(true);
            this.EnsureCoroutineStopped(ref fadeRoutine);
            Vector2 startPos = modalTransform.anchoredPosition;
            Vector2 endPos = modalHidePos;
            fadeRoutine = this.CreateAnimationRoutine(
                animationDuration,
                delegate (float progress) {
                    float easedProgress = Easing.easeInOutSine(0, 1, progress);
                    Vector2 pos = Vector2.Lerp(startPos, endPos, easedProgress);
                    modalTransform.anchoredPosition = pos;
                },
                delegate { modalObject.SetActive(false); }
            );
        }
    }
}
