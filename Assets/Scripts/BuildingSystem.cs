using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSystem : MonoBehaviour
{
    public GameObject[] objects;

    public GameObject pendingObjects;

    private Vector3 pos;

    private RaycastHit hit;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Toggle gridToggle;

    public float gridSize;

    bool gridOn = true;

    public float rotateAmount;



    // Method to set the building position from the mouse
    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }
    }


    // Method to select the object in the game
    public void SelectObject(int index)
    {
        pendingObjects = Instantiate(objects[index], pos, transform.rotation);
    }


    // Update is called once per frame
    void Update()
    {
        if(pendingObjects != null)
        {

            if (gridOn)
            {
                pendingObjects.transform.position = new Vector3(RoundToNearestGrid(pos.x), RoundToNearestGrid(pos.y), RoundToNearestGrid(pos.z));
            }
            else
            {
                pendingObjects.transform.position = pos;
            }


            if(Input.GetMouseButtonDown(0))
            {
                PlaceObject();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                RotateRight();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                RotateLeft();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                RotateUp();
            }

            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                RotateDown();
            }
        }
    }

    public void PlaceObject()
    {
        pendingObjects = null;
    }

    public void ToggleGrid()
    {
        if (gridToggle.isOn)
        {
            gridOn = true;
        }
        else
        {
            gridOn = false;
        }
    }

    //Method to snap the object to the grid
    float RoundToNearestGrid(float pos)
    {
        // Changeable grid system
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if(xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }
        return pos;
    }

    //Rotate right
    void RotateRight()
    {
        pendingObjects.transform.Rotate(Vector3.up, rotateAmount);
    }

    //Rotate left
    void RotateLeft()
    {
        pendingObjects.transform.Rotate(Vector3.down, rotateAmount);
    }

    //Rotate towards camera
    void RotateUp()
    {
        pendingObjects.transform.Rotate(Vector3.left, rotateAmount);
    }

    //Rotate away from camera
    void RotateDown()
    {
        pendingObjects.transform.Rotate(Vector3.right, rotateAmount);
    }
}

