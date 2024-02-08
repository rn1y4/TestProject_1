using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectEditor : MonoBehaviour
{
    private Camera maincamera;
    public GameObject selectedObject; //選択中のオブジェクト
    public Text selectedObjectNameText; // 選択中のオブジェクトの名前を表示するテキスト
    public Text modeText; // 選択モードを表示するテキスト

    // Start is called before the first frame update
    void Start()
    {
        maincamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //スペースキーで選択モード切り替え
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ObjectEditMode();
        }

        // 選択モードで左クリックが押されたときにオブジェクトを選択する
        if (EditModeManager.instance.isSelecting && Input.GetMouseButtonDown(0))
        {
            SelectObjectWithRaycast();
        }
    }

    void ObjectEditMode()
    {
        EditModeManager.instance.ObjectEditMode();
        Debug.Log("EditModeManager.instance.isSelecting: " + EditModeManager.instance.isSelecting); // デバッグ: 選択モードの状態をログに出力

        // 選択モードの表示を更新
        if (modeText != null)
        {
            modeText.text = EditModeManager.instance.isSelecting ? "Mode: Selecting" : "Mode: Normal";
            Debug.Log("Updated modeText: " + modeText.text); // デバッグ: テキストの内容をログに出力
        }
        else
        {
            Debug.LogError("modeText is not set");
        }
    }

    void SelectObjectWithRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            int itemLayerMask = 1 << LayerMask.NameToLayer("Item");
            if (Physics.Raycast(ray, out hit, 1000, itemLayerMask)) //レイヤマスクでitemレイヤのオブジェクトのみにレイを飛ばす
            {
                // デバッグ: レイキャストの視覚化
                Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 1.0f);

                // デバッグ: レイキャストがヒットした場合のログ
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name);

                // デバッグ: ヒットしたオブジェクトのプレハブ名とレイヤー名
                Debug.Log("Prefab name: " + hit.collider.gameObject.name);
                Debug.Log("Layer name: " + LayerMask.LayerToName(hit.collider.gameObject.layer));

                if (hit.collider != null)
                {
                    //Rayがあたったオブジェクトを選択する
                    selectedObject = hit.collider.gameObject;

                    // 選択中のオブジェクトの名前をテキストで表示
                    if (selectedObjectNameText != null)
                    {
                        selectedObjectNameText.text = "Selected: " + selectedObject.name;
                        Debug.Log("Updated selectedObjectNameText: " + selectedObjectNameText.text);
                    }
                    else
                    {
                        Debug.LogError("selectedObjectNameText is not set");
                    }
                }
            }
            else
            {
                // デバッグ: レイキャストがヒットしなかった場合のログ
                Debug.Log("Raycast did not hit any object");
            }
        }
    }

    //x方向のオブジェクト移動
    public void MoveObjectX(bool positive)
    {
        if (EditModeManager.instance.isSelecting && selectedObject != null)
        {
            float direction = positive ? 1f : -1f;
            selectedObject.transform.Translate(Vector3.right * direction, Space.World);
        }
    }

    //z方向のオブジェクト移動
    public void MoveObjectZ(bool positive)
    {
        if (EditModeManager.instance.isSelecting && selectedObject != null)
        {
            float direction = positive ? 1f : -1f;
            selectedObject.transform.Translate(Vector3.forward * direction, Space.World);
        }
    }
    public void OnRightButtonDown()
    {
        MoveObjectX(true);
    }

    public void OnLeftButtonDown()
    {
        MoveObjectX(false);
    }

    public void OnUpButtonDown()
    {
        MoveObjectZ(true);
    }

    public void OnDownButtonDown()
    {
        MoveObjectZ(false);
    }
}
