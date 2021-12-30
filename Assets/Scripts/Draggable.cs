using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
  public Transform parentReturnTo = null;

    
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
          this.transform.position = eventData.position;
       // transform.position = Input.mousePosition;
    }

    
        
    public void OnEndDrag(PointerEventData eventData)
    {
        //transform.localPosition = Vector3.zero;
        this.transform.SetParent(parentReturnTo);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
