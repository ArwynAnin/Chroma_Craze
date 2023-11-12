using System.Collections;
using UnityEngine;

public class HintPrompt : MonoBehaviour
{
    [SerializeField] private GameObject flag;
    [SerializeField] private float hintDelay;

    private void Update()
    {
        if (flag.gameObject.activeSelf) return;
        StartCoroutine(VanishHint());
    }

    private IEnumerator VanishHint()
    {
        yield return new WaitForSeconds(hintDelay);
        this.gameObject.SetActive(false);
    }
}
