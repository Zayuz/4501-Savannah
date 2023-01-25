using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Renderer tileRenderer;
    public GameObject tile;
    public Color trueColor = Color.white;
    public static int playerTurn = 1;

    void Update()
    {
        if (tile == getClickedObject(out RaycastHit hit)) {
            if (trueColor == Color.white)
            {
                if (playerTurn == 0)
                {
                    tileRenderer.material.color = Color.yellow;
                }
                else if (playerTurn == 1)
                {
                    tileRenderer.material.color = Color.blue;
                }
                Invoke("ResetColor", 0.05F);
            }
            if (Input.GetMouseButtonDown(0))
            { 
                if (ValidMove())
                {
                    if (playerTurn == 0)
                    {
                        tileRenderer.material.color = Color.green;
                    }
                    else
                    {
                        tileRenderer.material.color = Color.cyan;
                    }
                    changeTurn();
                    trueColor = tileRenderer.material.color;
                }
                else
                {
                    tileRenderer.material.color = Color.red;
                    Invoke("ResetColor", 0.5F);
                }
            }
        }
    }

    bool ValidMove()
    {
        if (trueColor != Color.white)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void ResetColor()
    {
        tileRenderer.material.color = trueColor;
    }

    void changeTurn()
    {
        playerTurn = ((playerTurn + 1) % 2);
    }

    int getTurn()
    {
        return playerTurn;
    }

    GameObject getClickedObject(out RaycastHit hit) {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        if(Physics.Raycast (ray.origin, ray.direction * 10, out hit))
        {
            if (!isPointerOverUIObject()) { target = hit.collider.gameObject; }
        }
        return target;
        bool isPointerOverUIObject()
        {
            PointerEventData ped = new PointerEventData(EventSystem.current);
            ped.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(ped, results);
            return results.Count > 0;
        }
    }
}
