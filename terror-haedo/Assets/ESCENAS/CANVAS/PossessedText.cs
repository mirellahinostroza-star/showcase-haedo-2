using System.Collections;
using TMPro;
using UnityEngine;

public class PossessedText : MonoBehaviour
{
    TMP_Text textMesh;
    string originalText;

    [Header("Timing")]
    public float calmTime = 2.5f;
    public float glitchTime = 0.8f;

    [Header("Intensity")]
    [Range(0,1)] public float mildCorruption = 0.15f;
    [Range(0,1)] public float heavyCorruption = 0.45f;

    string glitchChars = "█▓▒░@#$%&¿?¡!<>/\\+=-*";

    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
        originalText = textMesh.text;

        StartCoroutine(GlitchLoop());
    }

    IEnumerator GlitchLoop()
    {
        while (true)
        {
            // TEXTO NORMAL (se puede leer)
            textMesh.text = originalText;
            yield return new WaitForSeconds(Random.Range(calmTime * 0.7f, calmTime * 1.3f));

            // POSESIÓN LEVE
            yield return StartCoroutine(GlitchPhase(mildCorruption));

            // pausa breve legible
            textMesh.text = originalText;
            yield return new WaitForSeconds(0.2f);

            // POSESIÓN FUERTE
            yield return StartCoroutine(GlitchPhase(heavyCorruption));
        }
    }

    IEnumerator GlitchPhase(float corruption)
    {
        float timer = 0f;

        while (timer < glitchTime)
        {
            textMesh.text = CorruptText(originalText, corruption);
            yield return new WaitForSeconds(Random.Range(0.03f, 0.09f));
            timer += Time.deltaTime;
        }

        textMesh.text = originalText;
    }

    string CorruptText(string input, float corruption)
    {
        char[] result = input.ToCharArray();

        for (int i = 0; i < result.Length; i++)
        {
            if (result[i] == ' ') continue;

            if (Random.value < corruption)
            {
                result[i] = glitchChars[Random.Range(0, glitchChars.Length)];
            }
        }

        return new string(result);
    }
}

