using System.Collections;
using UnityEngine;

public class PokeballThrow : MonoBehaviour
{
    [Header("目標與特效")]
    public Transform pokemon;               // 拖入 Glaceon_ColladaMax
    public ParticleSystem captureEffect;    // 拖入 CaptureEffect

    [Header("拋出設定")]
    public float startDelay = 1f;           // 按 Play 後等幾秒才拋
    public float flightTime = 1.5f;         // 飛行時間
    public float arcHeight = 1.5f;          // 拋物線高度
    public float spinSpeed = 720f;          // 旋轉速度（度/秒）
    public float hitHeightOffset = 0.4f;    // 命中點比寶可夢腳底高多少

    [Header("收服設定")]
    public float shrinkTime = 0.6f;         // 寶可夢縮小時間

    void Start()
    {
        StartCoroutine(ThrowRoutine());
    }

    IEnumerator ThrowRoutine()
    {
        yield return new WaitForSeconds(startDelay);

        Vector3 start = transform.position;
        Vector3 end = pokemon.position + Vector3.up * hitHeightOffset;

        // 1. 由左至右拋出（拋物線）
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / flightTime;
            Vector3 pos = Vector3.Lerp(start, end, t);
            pos.y += Mathf.Sin(t * Mathf.PI) * arcHeight;
            transform.position = pos;
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = end;

        // 2. 命中：播放收服特效
        if (captureEffect != null)
        {
            captureEffect.transform.position = end;
            captureEffect.Play();
        }

        // 3. 寶可夢縮小消失（被吸進球裡）
        Vector3 originalScale = pokemon.localScale;
        float s = 0f;
        while (s < 1f)
        {
            s += Time.deltaTime / shrinkTime;
            pokemon.localScale = Vector3.Lerp(originalScale, Vector3.zero, s);
            yield return null;
        }
        pokemon.gameObject.SetActive(false);
    }
}