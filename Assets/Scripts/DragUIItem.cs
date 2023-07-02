using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DragUIItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject PrefabToInstantiate;
    [SerializeField] private RectTransform UIDragElement;
    [SerializeField] private RectTransform Canvas;
    [SerializeField] private Transform floorTrans;
    [SerializeField] private TMP_InputField inputText;

    private float floorY;
    private Vector2 mOriginalLocalPointerPosition;
    private Vector3 mOriginalPanelLocalPosition;
    private Vector2 mOriginalPosition;

    void Start()
    {
        floorY = floorTrans.position.y;
        mOriginalPosition = UIDragElement.localPosition;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        mOriginalPanelLocalPosition = UIDragElement.localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            Canvas,
            data.position,
            data.pressEventCamera,
            out mOriginalLocalPointerPosition);
    }

    public void OnDrag(PointerEventData data)
    {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
          Canvas,
          data.position,
          data.pressEventCamera,
          out localPointerPosition))
        {
            Vector3 offsetToOriginal =
              localPointerPosition -
              mOriginalLocalPointerPosition;
            UIDragElement.localPosition =
              mOriginalPanelLocalPosition +
              offsetToOriginal;
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        StartCoroutine(
            Coroutine_MoveUIElement(
                UIDragElement,
                mOriginalPosition,
            0.5f));

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(
          Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000.0f))
        {
            Vector3 worldPoint = hit.point;
            worldPoint.y = floorY + PrefabToInstantiate.transform.position.y;
            CreateObject(worldPoint);
        }
    }

    public IEnumerator Coroutine_MoveUIElement(
    RectTransform rect,
    Vector2 targetPosition,
    float duration = 0.1f)
    {
        float elapsedTime = 0;
        Vector2 startingPos = rect.localPosition;
        while (elapsedTime < duration)
        {
            rect.localPosition =
              Vector2.Lerp(
                startingPos,
                targetPosition,
                (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        rect.localPosition = targetPosition;
    }

    public void CreateObject(Vector3 position)
    {
        if (PrefabToInstantiate == null)
        {
            Debug.Log("No prefab to instantiate");
            return;
        }
        GameObject obj = Instantiate( PrefabToInstantiate, position, Quaternion.identity);
        if (inputText.text != null)
        {
            obj.GetComponentInChildren<TextMeshPro>().text = inputText.text;
        }
    }
}
