using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Drag : MonoBehaviour
{
    /*
     A Component that can help with draggable objects,

     Can be used through EventTrigger + 'Drag' as an event type,
     Needs a specified Canvas in order to work properly

     */

    public Sprite hex_gray;
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DragHandler(BaseEventData data)
    {
        // make on top while dragging and after

        GameManager.instance.movingCandidate = gameObject;
        GameObject realParent = gameObject.transform.parent.gameObject;
        GameObject dummyParentFolder = GameObject.FindGameObjectWithTag("dummy_parent_folder");
        gameObject.transform.SetParent(dummyParentFolder.transform);
        gameObject.transform.SetParent(realParent.transform);


        PointerEventData pointerEventData = (PointerEventData)data;

        Vector3 position;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            (RectTransform)canvas.transform,
            pointerEventData.position,
            canvas.worldCamera,
            out position
        );

//        highlightNearestMatching();
        transform.position = position;
    }

    private void highlightNearestMatching()
    {
        float margin = 0.2f;
        foreach (GameObject spot in GameManager.instance.spots)
        {
            if (GameManager.instance.movingCandidate == null)
                return;

            Sprite found = null;
            foreach (GameObject c_hex in Helper.getChildsFor(GameManager.instance.movingCandidate))
            {
                float distance = Vector2.Distance(spot.transform.position, c_hex.transform.position);
                if((distance < margin))
                {
                    found = c_hex.GetComponent<Image>().sprite;
                }
            }
            if (found != null)
            {
                spot.GetComponent<Image>().sprite = found;
            }
            else
            {
                spot.GetComponent<Image>().sprite = hex_gray;
            }
        }

    }

}
