using UnityEngine;
using UnityEngine.UI;

public class SceneryMotion : MonoBehaviour
{
    [SerializeField] private Vector2 motion;
    private RawImage image;

    private void Awake()
    {
        image = GetComponent<RawImage>();
    }

    private void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + motion * Time.deltaTime, image.uvRect.size);
    }

}
