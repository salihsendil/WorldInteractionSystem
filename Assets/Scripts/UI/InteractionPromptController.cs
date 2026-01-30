using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;

public class InteractionPromptController : MonoBehaviour
{
    [SerializeField] private TMP_Text tmpText;
    [SerializeField] private float fadeDuration = 0.5f;

    private void Start()
    {
        SetAlpha(0f);
    }

    public async UniTaskVoid ShowMessage(string message)
    {
        tmpText.text = message;

        SetAlpha(1f);
        tmpText.gameObject.SetActive(true);

        await UniTask.Delay(1000);

        await FadeOut();

        tmpText.gameObject.SetActive(false);
    }

    private async UniTask FadeOut()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            SetAlpha(alpha);
            await UniTask.Yield();
        }

        SetAlpha(0f);
    }

    private void SetAlpha(float alpha)
    {
        var color = tmpText.color;
        color.a = alpha;
        tmpText.color = color;
    }
}
