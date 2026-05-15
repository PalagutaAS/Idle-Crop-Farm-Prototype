using UnityEngine;
using DG.Tweening;

public class ScreenLoading : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 0.5f;
    
    private CanvasGroup _canvas;

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
        DontDestroyOnLoad(this);
    }

    [ContextMenu("SHOW")]
    public void Show()
    {
        gameObject.SetActive(true);
        _canvas.alpha = 1;
    }

    [ContextMenu("HIDE")]
    public void Hide()
    {
        _canvas.DOKill();
        
        _canvas.DOFade(0, _fadeDuration)
            .OnComplete(() => gameObject.SetActive(false));
    }
}