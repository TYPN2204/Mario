using System;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private int _spriteIndex;
    public MoveAnimation[] moveAnimation;
    public Movement movement;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (movement.moving)
        {
            InvokeRepeating(nameof(AnimateSprite), 0f, 1f);
        }
        else CancelInvoke(nameof(AnimateSprite));
    }
    
    private void AnimateSprite()
    {
        Sprite[] sprites = null;

        switch (movement.movemode)
        {
            case Movement.MovementMode.Up:
                sprites = moveAnimation[0].movementSprites;
                break;
            case Movement.MovementMode.Right:
                sprites = moveAnimation[1].movementSprites;
                break;
            case Movement.MovementMode.Left:
                sprites = moveAnimation[2].movementSprites;
                break;
        }
        _spriteIndex++;
        
        if (_spriteIndex >= sprites.Length) {
            _spriteIndex = 0;
        }

        if (_spriteIndex < sprites.Length && _spriteIndex >= 0) {
            _spriteRenderer.sprite = sprites[_spriteIndex];
        }
    }

}

[Serializable]
public class MoveAnimation
{
    public Sprite[] movementSprites;
}