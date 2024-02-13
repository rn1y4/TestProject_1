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
        //右クリックでEditModeを切り替え
        if (Input.GetMouseButtonDown(1))
        {
            EditModeManager.instance.ToggleWithRightClick();
            // Movingフラグを下す
            if (EditModeManager.instance.isMoving)
            {
                EditModeManager.instance.SetMoving(false);
            }
        }

        //スペースキーでEditModeを切り替え
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EditModeManager.instance.ToggleWithSpace();
            // Movingフラグを下す
            if (EditModeManager.instance.isMoving)
            {
                EditModeManager.instance.SetMoving(false);
            }
        }

        // 現在のEditModeに基づいてテキストを表示
        modeText.text = "Mode: " + EditModeManager.instance.currentMode.ToString();

        // 選択モードで左クリックが押されたときにオブジェクトを選択する
        if (EditModeManager.instance.currentMode == EditModeManager.EditMode.Selecting && Input.GetMouseButtonDown(0))
        {
            SelectObjectWithRaycast();
        }
    }

    public void SelectObjectWithRaycast()
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
                    EditModeManager.instance.SetMoving(true); // Movingフラグを立てる


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
                else
                {
                    // デバッグ: レイキャストがヒットしなかった場合のログ
                    Debug.Log("Raycast did not hit any object");
                }
            }
        }
    }


        //x方向のオブジェクト移動
        public void MoveObjectX(bool positive)
        {
            // Movingフラグが下がっている場合、ここで処理をスキップ
            if (!EditModeManager.instance.isMoving) return;

            if (selectedObject != null)
            {
                float direction = positive ? 1f : -1f;
                selectedObject.transform.Translate(Vector3.right * direction, Space.World);
            }
        }

        //z方向のオブジェクト移動
        public void MoveObjectZ(bool positive)
        {
            // Movingフラグが下がっている場合、ここで処理をスキップ
            if (!EditModeManager.instance.isMoving) return;

            if (selectedObject != null)
            {
                float direction = positive ? 1f : -1f;
                selectedObject.transform.Translate(Vector3.forward * direction, Space.World);
            }
        }


        /*
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
    }*/
}
