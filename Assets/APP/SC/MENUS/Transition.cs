using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using PrimeTween;

public class Transition : MonoBehaviour
{
    [SerializeField] GameObject canvas_fade;
    [SerializeField] Material mat_fade;
    [SerializeField] float duration_fade = 0.5f;
    public static Transition Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        canvas_fade.SetActive(false);
    }
    public async Task FadeIn()
    {
        canvas_fade.SetActive(true);
        await Tween.MaterialAlpha(mat_fade, 1f, duration_fade);
        return;
    }
    public async Task FadeOut()
    {
        canvas_fade.SetActive(true);
        await Task.Delay(600);
        await Tween.MaterialAlpha(mat_fade, 0f, duration_fade);
        canvas_fade.SetActive(false);
        return;
    }
}
