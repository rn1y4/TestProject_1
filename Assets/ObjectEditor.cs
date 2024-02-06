using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEditor : MonoBehaviour
{
    private Camera maincamera;
    public GameObject selectedObject; //選択中のオブジェクト
    public Material selectedMaterial; //選択中のオブジェクトに適用するマテリアル
    public Material defaultMaterial; //選択解除時に適用するマテリアル

    private bool isSelecting = false; //選択モード制御
    // Start is called before the first frame update
    void Start()
    {
        //スペースキーで選択モード切り替え
        if (Input.GetKeyDown(KeyCode.Space))
        {  
            isSelecting = !isSelecting;
        }

        //選択モードで左クリックが押されたときにオブジェクトを選択する
        if (isSelecting && Input.GetMouseButton(0))
        {
            Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                //Rayがあたったオブジェクトを選択する
                if(selectedObject != null)
                {
                    //選択されているオブジェクトがある場合は選択を解除する
                    selectedObject.GetComponent<Renderer>().material = defaultMaterial;
                }
                selectedObject = hit.collider.gameObject;
                selectedObject.GetComponent<Renderer>().material = selectedMaterial;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
