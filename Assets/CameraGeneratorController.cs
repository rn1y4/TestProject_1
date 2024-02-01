using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraGeneratorController: MonoBehaviour
{
    private Camera maincamera;
    public GameObject generateTarget;�@//��������^�[�Q�b�g�̃v���n�u
    public ToggleGroup toggleGroup; //ToggletGroup�ւ̎Q��
    private bool canGenerate = true; //�I�u�W�F�N�g�����\�����䂷��t���O

    // Start is called before the first frame update
    void Start()
    {
        maincamera = GetComponent<Camera>();
        toggleGroup.gameObject.SetActive(false); //������Ԃł�ToggleGroupe���A�N�e�B�u�ɂ���
    }

    // Update is called once per frame
    void Update()
    {
        //�E�N���b�N�Ő����@�\�𖳌������AToggleGroupe��\������
        if (Input.GetMouseButtonDown(1))
        {
            canGenerate =�@!canGenerate;
            toggleGroup.gameObject.SetActive(!toggleGroup.gameObject.activeSelf);
        }

        //�����t���O��false�̏ꍇ�A�����ŏ������I������
        if (!canGenerate) return;

        //���N���b�N�ŃI�u�W�F�N�g�𐶐�����
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);
            //Debug.Log(ray.origin);
            //Debug.Log(ray.direction);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                GameObject go = Instantiate(generateTarget);
                go.transform.position = hit.point;
                Debug.Log(hit.point);
                Debug.Log(hit.transform.name);               
            }
        }


    }
}
