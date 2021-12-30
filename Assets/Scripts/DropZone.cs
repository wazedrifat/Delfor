using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    List<string> names = new List<string>();
    public GameObject golem;
    public GameObject hippo;
    public GameObject dragon;
    private GameObject[] nodes;
    //\public bool isDrop;

    public GameObject vas;

    private void Start()
    {
        nodes = vas.GetComponent<canVas>().nodes;
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + "Drop:" + gameObject.name);

        if(eventData.pointerDrag.name == "GolemCard")
        {
            eventData.pointerDrag.SetActive(false);
            golemSpawn();
        }

        if (eventData.pointerDrag.name == "HippoCard")
        {
            eventData.pointerDrag.SetActive(false);
            hippoSpawn();
        }
        if(eventData.pointerDrag.name == "DragonCard")
        {
            eventData.pointerDrag.SetActive(false);
            dragonSpawn();
        }



        if (gameObject.name == "executionArea")
        {
          //  print(eventData.pointerDrag.name);
            names.Add(eventData.pointerDrag.name);
        }


        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if(d != null)
        {
            d.parentReturnTo = this.transform;
        }

        
        
    }

    public void golemSpawn()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hit,1000))
        {
            golem.SetActive(true);
            golem.transform.position = hit.point;
            
        }
    }

    public void dragonSpawn()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            dragon.SetActive(true);
            dragon.transform.position = hit.point;
        }
    }


    public void hippoSpawn()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            hippo.SetActive(true);
            hippo.transform.position = hit.point;
            float d = float.MaxValue;
            int j = 0;
             for(int i =0;i<nodes.Length;i++)
            {
               if(Vector3.Distance(hippo.transform.position,nodes[i].transform.position) < d)
                {
                    d = Vector3.Distance(hippo.transform.position, nodes[i].transform.position);
                    j = i;
                }
            }
            hippo.GetComponent<playerScript>().curPosition = j;
            hippo.GetComponent<playerScript>().nextPosition = j;
        }
        hippo.GetComponent<playerScript>().isDrop = true;
        print("Drop");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void showNames()
    {
        foreach (string str in names)
        {
          //  print(str);


        }

      //  if (names[0] == "Card" && names[1] == "Card2" && names[2] == "Card3")
      //  {
      //      cube.GetComponent<rotate>().enabled = true;
       //     print("execution");

     //   }

    }


}
