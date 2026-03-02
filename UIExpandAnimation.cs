using UnityEngine;
using System.Collections;
using UnityEngine.Events;

/// <summary>
/// 通用UI缩放展开动画脚本
/// 支持从Scale(0,0,0)平滑过渡到自定义大小，可指定动画时长
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class UIExpandAnimation : MonoBehaviour
{
    [Header("动画配置")]
    [Tooltip("动画目标缩放大小（最终展开的尺寸）")]
    public Vector3 targetScale = Vector3.one; // 默认1倍大小
    [Tooltip("动画总时长（秒）")]
    public float animationDuration = 0.3f; // 默认0.3秒完成
    [Tooltip("是否启用缓动效果（让动画更自然）")]
    public bool useSmoothEase = true;

    public UnityEvent OnExpandFinish;

    private RectTransform _rectTransform;
    private Coroutine _expandCoroutine; // 记录当前动画协程，防止重复执行

    private void Awake()
    {
        // 获取UI的RectTransform组件（UI元素的核心组件）
        _rectTransform = GetComponent<RectTransform>();
        // 初始化：默认将UI隐藏（缩放为0）
        _rectTransform.localScale = Vector3.zero;
    }

    private void OnEnable()
    {
        if (_rectTransform != null)
            PlayExpandAnimation();
    }

    /// <summary>
    /// 外部调用：播放展开动画
    /// </summary>
    public void PlayExpandAnimation()
    {
        // 如果已有动画在执行，先停止，避免叠加
        if (_expandCoroutine != null)
        {
            StopCoroutine(_expandCoroutine);
        }
        // 重置初始状态（缩放为0）
        _rectTransform.localScale = Vector3.zero;
        // 启动动画协程
        _expandCoroutine = StartCoroutine(ExpandCoroutine());
    }

    /// <summary>
    /// 展开动画核心协程
    /// </summary>
    private IEnumerator ExpandCoroutine()
    {
        float elapsedTime = 0f; // 已流逝的时间
        Vector3 startScale = Vector3.zero; // 起始缩放（0）

        while (elapsedTime < animationDuration)
        {
            // 计算进度（0~1）
            float t = elapsedTime / animationDuration;

            // 如果启用缓动，用Mathf.SmoothStep实现平滑的缓入缓出，否则线性插值
            float progress = useSmoothEase ? Mathf.SmoothStep(0f, 1f, t) : t;

            // 插值计算当前缩放值
            _rectTransform.localScale = Vector3.Lerp(startScale, targetScale, progress);

            // 累加时间（Time.deltaTime保证帧率无关）
            elapsedTime += Time.deltaTime;

            //等待下一帧
            yield return null;
        }

        // 动画结束后强制设置为目标值，避免精度误差
        _rectTransform.localScale = targetScale;

        // 重置协程标记
        _expandCoroutine = null;

        // 动画完成回调（可扩展）
        OnExpandAnimationComplete();
    }

    /// <summary>
    /// 展开动画完成回调（可重写或绑定）
    /// </summary>
    protected virtual void OnExpandAnimationComplete()
    {
        Debug.Log($"{gameObject.name} 展开动画完成！");
        OnExpandFinish?.Invoke();
    }
}