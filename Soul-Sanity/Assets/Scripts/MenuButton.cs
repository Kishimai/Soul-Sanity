using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MenuButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    TMP_Text text;
    public string Containing;

    private void Start() {
        text = GetComponentInChildren<TMP_Text>();
    }
    
    public void OnPointerDown(PointerEventData eventData){
        text.text = $"[{Containing}]";
    }

    public void OnPointerUp(PointerEventData eventData){
        text.text = $"[ {Containing} ]";
    }
}
