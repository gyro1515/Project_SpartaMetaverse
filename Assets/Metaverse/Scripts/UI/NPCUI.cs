using FlappyPlaneSession;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MetaverseSession
{
    public class NPCUI : MonoBehaviour
    {
        [SerializeField] Canvas npcInterCanvas;
        [SerializeField] Canvas npcTalkCanvas;

        /*private void Update()
        {
            if (npcCanvas == null || !npcCanvas.gameObject.activeSelf) return;
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log($"상호작용");
            }
        }*/
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Debug.Log($"Enter: {collision.name}");

            npcInterCanvas.gameObject.SetActive(true);
        }
        /* private void OnTriggerStay2D(Collider2D collision)
         {
             //Debug.Log($"Stay: {collision.name}");
             if(Input.GetKeyDown(KeyCode.E))
             {
                 npcCanvas.gameObject.SetActive(false);
             }
         }*/
        private void OnTriggerExit2D(Collider2D collision)
        {
            //Debug.Log($"Exit: {collision.name}");

            npcInterCanvas.gameObject.SetActive(false);
            npcTalkCanvas.gameObject.SetActive(false);
            UIManager.instance.CharSelUI.gameObject.SetActive(false);

        }
        void OnInteraction(InputValue inputValue)
        {
            // inputValue.isPressed를 안하면 키 다운, 키 업 두 번 호출 됨
            if (inputValue.isPressed)
            {
                if (npcTalkCanvas != null && npcTalkCanvas.gameObject.activeSelf)
                {
                    //Debug.Log($"커스텀 UI 온");
                    npcTalkCanvas.gameObject.SetActive(false);
                    UIManager.instance.CharSelUI.gameObject.SetActive(true);
    
            }
                else if (npcInterCanvas != null && npcInterCanvas.gameObject.activeSelf)
                {
                    npcTalkCanvas.gameObject.SetActive(true);
                }
                //Debug.Log($"상호작용 온");
            }
        }
    }
}