using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// For better display, set Template Content height same as Template Item height
/// </summary>
[RequireComponent(typeof(EventTrigger))]
public class DropdownViewportSizeByChildren : MonoBehaviour
{
    [Header("Child Component")]
    [SerializeField] TMP_Dropdown _dropdown;
    [SerializeField] RectTransform _templateContent;
    [SerializeField] RectTransform _templateItem;

    [Header("Viewport size limit")]

    [SerializeField] bool _makeTemplateContentAndItemSameHeight = false;
    [SerializeField] bool _limitSize = false;
    [SerializeField] int _maxChildShowAtATime = 5;

    private EventTrigger _eventTrigger;

    private void Start()
    {
        if (_makeTemplateContentAndItemSameHeight)
        {
            _templateContent.sizeDelta = new Vector2(
                    _templateContent.sizeDelta.x,
                    _templateItem.sizeDelta.y
                );
        }
        SetViewportSize();
        _eventTrigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new()
        {
            eventID = EventTriggerType.PointerClick
        };
        entry.callback.AddListener((eventData) => { SetViewportSize(); });
        _eventTrigger.triggers.Add(entry);
    }

    public void SetViewportSize()
    {
        StartCoroutine(SetViewPortSizeCoroutine());
    }

    private IEnumerator SetViewPortSizeCoroutine()
    {
        GameObject dropdownListObj = GameObject.Find("Dropdown List");
        if (dropdownListObj != null)
        {
            dropdownListObj.TryGetComponent<RectTransform>(out var dropdownList);
            if (dropdownList != null)
            {
                int itemNumber = _dropdown.options.Count;
                if (_limitSize == true)
                {
                    if (itemNumber > _maxChildShowAtATime) itemNumber = _maxChildShowAtATime;
                }

                Debug.Log(_limitSize + " | " + itemNumber + " | " + _templateItem.rect.height + " | " + itemNumber * _templateItem.rect.height);
                Vector2 newSize = new(
                    _templateContent.sizeDelta.x,
                    itemNumber * _templateItem.rect.height
                    );
                dropdownList.sizeDelta = newSize;
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
                StartCoroutine(SetViewPortSizeCoroutine());

            }
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(SetViewPortSizeCoroutine());

        }
    }
}
