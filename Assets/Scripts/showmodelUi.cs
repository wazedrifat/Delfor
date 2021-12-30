using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showmodelUi : MonoBehaviour
{
    [SerializeField] private showButtonModel buttonPrefab;


    void Start()
    {
        var models = FindObjectOfType<showCaseModel>().GetModels();
        foreach(var model in models)
        {
            createButtonForModel(model);
        }
    }


    private void createButtonForModel(Transform model)
    {
        var button = Instantiate(buttonPrefab);
        button.transform.SetParent(this.transform);
        button.transform.localScale = Vector3.one;
        button.transform.localRotation = Quaternion.identity;

        var controller = FindObjectOfType<showCaseModel>();

        button.Initialize(model, controller.enableModel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
