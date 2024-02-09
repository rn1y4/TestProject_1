using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraGeneratorController : MonoBehaviour
{
    private Camera maincamera;
    public GameObject generateTarget; //生成するターゲットのプレハブ
    public Transform generatedObjectsParent; //生成したオブジェクトを格納する親オブジェクト
    public ToggleGroup toggleGroup; //ToggletGroupへの参照
    public CameraRotateController cameraController; //CameraRotateControllerへの参照
    public Vector3 objectSize = Vector3.one; // 生成するオブジェクトのサイズ
    private bool canGenerate = true; //オブジェクト生成可能か制御するフラグ

    // Start is called before the first frame update
    void Start()
    {
        maincamera = GetComponent<Camera>();
        toggleGroup.gameObject.SetActive(false); //初期状態ではToggleGroupeを非アクティブにする
    }

    //右クリックが押されたときの処理
    void CheckRightClick()
    {
        //生成機能とToggleGroupeを表示を切り替える
        if (Input.GetMouseButtonDown(1))
        {
            // TogglingモードのときにはToggleを表示、それ以外のときには非表示にする
            toggleGroup.gameObject.SetActive(EditModeManager.instance.currentMode == EditModeManager.EditMode.Toggling);
            EditModeManager.instance.ToggleWithRightClick();
            cameraController.BackUpMousePosition(); //ToggleGroupの表示状態が切り替わる時にマウス位置を保存
        }
    }

    //左クリックが押されたときの処理
    void CheckLeftClick()
    {
        //生成するオブジェクトが何も選択されてない場合、ここで処理を終了する
        if (generateTarget == null) return;

        // Toggleが表示されている場合、ここで処理を終了する
        if (toggleGroup.gameObject.activeSelf) return;



        //レイヤがGroundのオブジェクト上のみにオブジェクトを生成する
        if (Input.GetMouseButtonDown(0) && EditModeManager.instance.currentMode == EditModeManager.EditMode.Generating)
        {
            Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            int groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
            if (Physics.Raycast(ray, out hit, 1000, groundLayerMask)) //レイヤマスクでGroundレイヤのオブジェクトのみにレイを飛ばす
            {
                int itemLayerMask = 1 << LayerMask.NameToLayer("Item"); // "Item"レイヤーのマスク
                // 生成予定の位置に既に他のオブジェクトが存在しないことを確認
                if (!Physics.CheckBox(hit.point, objectSize * 0.5f, Quaternion.identity, itemLayerMask))
                {
                    // 他のオブジェクトが存在しない場合のみオブジェクトを生成
                    GameObject go = Instantiate(generateTarget, generatedObjectsParent); //生成したオブジェクトを特定の親オブジェクトの子オブジェクトにする
                    go.name = generateTarget.name;

                    go.transform.position = hit.point;
                    Debug.Log(hit.point);
                    Debug.Log(hit.transform.name);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //右クリックでEditModeを切り替え
        CheckRightClick();

        ObjectEditor.CheckSpaseKeyDown();

        // 生成モードで左クリックが押されたときにオブジェクトを生成する
        if (EditModeManager.instance.currentMode == EditModeManager.EditMode.Generating)
        {
            CheckLeftClick();
        }
    }
}