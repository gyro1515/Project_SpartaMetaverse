using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace MetaverseSession
{
    public class PlayerController : BaseController
    {
        private GameManager gameManager;
        private Camera _camera;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            Vector3 tmpV3 = Vector2.Lerp((Vector2)_camera.transform.position, (Vector2)transform.position, 0.05f);
            tmpV3.z = _camera.transform.position.z;
            _camera.transform.position = tmpV3;
            // 카메라 클램프
            CameraControl.CameraClamp(_camera, gameManager.tilemap.localBounds);
            //CameraControl.CameraClamp(_camera, gameManager.tilemap);

        }
        public void Init(GameManager gameManager)
        {
            this.gameManager = gameManager;
            _camera = Camera.main;
        }
        protected override void HandleAction()
        {
        }

        
        // 아래는 Player Input Component에서 불러와줌
        void OnMove(InputValue inputValue)
        {
            movementDirection = inputValue.Get<Vector2>();
            movementDirection = movementDirection.normalized;
        }
        void OnJump(InputValue inputValue)
        {

        }
        public void SetCharacter(CharacterBase charBase)
        {
            //Debug.Log("SetCharacter");
            SpriteRenderer tmpSP = GetComponentInChildren<SpriteRenderer>();
            tmpSP.sprite = charBase.characterRenderer.sprite;
            Animator tmpAnim = GetComponentInChildren<Animator>();
            tmpAnim.runtimeAnimatorController = charBase.characterAnimator.runtimeAnimatorController;
            characterRenderer = tmpSP;
            characterAnimator = tmpAnim;
            // 변경 전 참조 값은 알아서 삭제가 될까?
        }
        
    }
}