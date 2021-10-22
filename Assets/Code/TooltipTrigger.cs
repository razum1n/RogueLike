using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string content;
    public string header;
    public float delay = 1.5f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine("TriggerTooltip");
        TooltipSystem.Show(content, header);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
        StopCoroutine("TriggerTooltip");
    }

    IEnumerator TriggerTooltip()
    {
        yield return new WaitForSeconds(delay);
        TooltipSystem.Show(content, header);

    }
}
