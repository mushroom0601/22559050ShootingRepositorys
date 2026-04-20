using UnityEngine;

public class FollowWorldUI : MonoBehaviour
{
    public Transform target;
    public Vector3 worldOffset = new Vector3(0f, 1.2f, 0f);
    public Camera targetCamera;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void LateUpdate()
    {
        if (target == null || targetCamera == null)
            return;

        Vector3 worldPos = target.position + worldOffset;
        Vector3 screenPos = targetCamera.WorldToScreenPoint(worldPos);

        // 카메라 뒤로 가면 숨기기만 하고, 오브젝트는 비활성화하지 않음
        if (screenPos.z < 0f)
        {
            canvasGroup.alpha = 0f;
            return;
        }

        canvasGroup.alpha = 1f;
        rectTransform.position = screenPos;
    }
}