// using System;
// using UnityEngine;
//
// public class MoveAnimation : MonoBehaviour
// {
//     private SpriteRenderer _spriteRenderer;
//     private int _spriteIndex;
//     public MoveAnim[] moveAnimation;
//     public Movement movement;
//     private Movement.MovementMode _lastMode;
//     private float animationTimer;
//     public float animationSpeed = 0.1f;
//
//     private void Awake()
//     {
//         _spriteRenderer = GetComponent<SpriteRenderer>();
//         _lastMode = movement.movemode;
//     }
//
//     void FixedUpdate()
//     {
//         if (movement.moving)
//         {
//             animationTimer -= Time.fixedDeltaTime;
//             if (animationTimer <= 0f)
//             {
//                 AnimateSprite();
//                 animationTimer = animationSpeed; 
//             }
//             if (_lastMode != movement.movemode)
//             {
//                 _lastMode = movement.movemode;
//                 _spriteIndex = 0;
//                 CancelInvoke(nameof(AnimateSprite));
//                 InvokeRepeating(nameof(AnimateSprite), 0f, 0.1f);
//             }
//         }
//         else CancelInvoke(nameof(AnimateSprite));
//     }
//
//     private void AnimateSprite()
//     {
//         Sprite[] sprites = null;
//         switch (movement.movemode)
//         {
//             case Movement.MovementMode.Up:
//                 sprites = moveAnimation[0].movementSprites;
//                 break;
//             case Movement.MovementMode.Right:
//                 sprites = moveAnimation[1].movementSprites;
//                 break;
//             case Movement.MovementMode.Left:
//                 sprites = moveAnimation[2].movementSprites;
//                 break;
//             case Movement.MovementMode.Stand:
//                 sprites = moveAnimation[3].movementSprites;
//                 break;
//             case Movement.MovementMode.Lose:
//                 sprites = moveAnimation[4].movementSprites;
//                 break;
//         }
//
//         _spriteIndex++;
//
//         if (_spriteIndex >= sprites.Length) {
//             _spriteIndex = 0;
//         }
//
//         if (sprites != null && _spriteIndex < sprites.Length) {
//             _spriteRenderer.sprite = sprites[_spriteIndex];
//         }
//     }
// }
// [Serializable]
// public class MoveAnim
// {
//     public Sprite[] movementSprites;
// }