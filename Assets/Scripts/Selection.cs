using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Selection : MonoBehaviour
{

    public GameObject selectedObject;

    private BuildingSystem buildingSystem;

    public GameObject popUpUI;


    // Start is called before the first frame update
    void Start()
    {
        buildingSystem = GameObject.Find("BuildingSystem").GetComponent<BuildingSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.CompareTag("Object"))
                {
                    Select(hit.collider.gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && selectedObject != null)
        {
            Deselect();
        }

    }

    private void Select(GameObject obj)
    {
        if (obj == selectedObject)
        {
            return;
        }
        if(selectedObject != null)
        {
            Deselect();
        }
        Outline outline = obj.GetComponent<Outline>();
        if (outline == null) obj.AddComponent<Outline>();
        else outline.enabled = true;
        selectedObject = obj;
        popUpUI.SetActive(true);
    }

    private void Deselect()
    {
        selectedObject.GetComponent<Outline>().enabled = false;
        popUpUI.SetActive(false);
        selectedObject = null;
    }

    public void Move()
    {
        buildingSystem.pendingObjects = selectedObject;
    }

    public void Delete()
    {
        GameObject objToDelete = selectedObject;
        Deselect();
        Destroy(objToDelete);
    }
}
