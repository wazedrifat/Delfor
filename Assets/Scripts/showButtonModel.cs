using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showButtonModel : MonoBehaviour
{
    private Transform objectToShow;
    private Action<Transform> clickAction;

    

    public void Initialize(Transform objectToShow,Action<Transform>clickAction)
    {
        this.objectToShow = objectToShow;
        this.clickAction = clickAction;
        GetComponentInChildren<Text>().text = objectToShow.gameObject.name;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(HandleButtonClick);
    }

    private void HandleButtonClick()
    {
        clickAction(objectToShow);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
