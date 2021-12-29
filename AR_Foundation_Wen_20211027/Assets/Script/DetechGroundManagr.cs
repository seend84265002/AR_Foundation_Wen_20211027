using UnityEngine.XR.ARFoundation;      //  �ޥ� Foundation API
using UnityEngine.XR.ARSubsystems;      //  �ޥ� Subsystems API;
using UnityEngine;
using System.Collections.Generic;       //�ޥ� ���X API �]�t�M�� List

namespace Wen.ARFoundation
{
    /// <summary>
    /// �˴��a�O�I���޲z��
    /// �I���a�O��B�z���ʦ欰
    /// �ͦ�����P������m
    /// </summary>
    public class DetechGroundManagr : MonoBehaviour
    {
        [Header("�I����n�ͦ�������")]
        public GameObject goSpawn;
        [Header("AR �g�u�޲z��"), Tooltip("���޲z���n��b��v�� Origin ����W")]
        public ARRaycastManager arRaycastManager;
        [Header("�ͦ�����n���V����v������")]
        public Transform traCamera;
        [Header("�ͦ����󭱦V���t��"), Range(0, 100)]
        public float speedLookAt = 3.5f;
        private Transform traSpawm;
        private List<ARRaycastHit> hits = new List<ARRaycastHit>();      //  �M����� = �s�W ���� �M�檫��

        /// <summary>
        /// �ƹ�����PĲ��
        /// </summary>
        private bool inputMouseLeft { get => Input.GetKeyDown(KeyCode.Mouse0); }
        private void Update()
        {
            ClickAndDetechGround();
            SpawnLookAtCamera();
        }
        /// <summary>
        /// �I���ˬd�a�O
        /// 1. �����O�_�����w����
        /// 2. �N�I���y�Ь���
        /// 3. �g�u�ˬd
        /// 4. ����
        /// </summary>
        private void ClickAndDetechGround()
        {
            if (inputMouseLeft)                                                             //�p�G�@���U���w����
            {
                Vector2 positionMouse = Input.mousePosition;                                //���o�@�I���y��
                //print("�I���y��: " + positionMouse);                                          ���շƹ������m�y��
                //Ray ray = Camera.main.ScreenPointToRay(positionMouse);                     //�N �I���y��  �ର�g�u
                if (arRaycastManager.Raycast(positionMouse, hits, TrackableType.PlaneWithinPolygon))  // �p�G �g�u����a�O
                {
                    Vector3 positionHit = hits[0].pose.position;                            //�����I���y�� �é�b�M�椺
                    if(traSpawm == null)
                    {
                        
                        traSpawm = Instantiate(goSpawn, positionHit, Quaternion.identity).transform;   //�N����ͦ��b�I�쪺�y�ФW
                        traSpawm.localScale = Vector3.one * 0.5f;
                    }
                    else
                    {
                        traSpawm.position = positionHit;                                        //�_�h ���ͦ��L���� �N��s�y��
                    }               
                };
            }

        }
        private void SpawnLookAtCamera()
        {
            if (traSpawm == null) return;                                                                   //�p�G �ͦ����󬰪ŭȸ��X    
            Quaternion angle = Quaternion.LookRotation(traCamera.position - traSpawm.position);             //�����V�q
            traSpawm.rotation = Quaternion.Lerp(traSpawm.rotation, angle, Time.deltaTime * speedLookAt);    //���׮t��    
            Vector3 angleOrigial = traSpawm.localEulerAngles;                                               //��������
            angleOrigial.x = 0;                                                                             // �ᵲX 
            angleOrigial.z = 0;                                                                             //  �ᵲZ                 //  
            traSpawm.localEulerAngles = angleOrigial;                                                       // ��s����   
        } 

    }

   

}
