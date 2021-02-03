using UnityEngine;

public class OnlinePlayer : Player {
    
    protected override void Update() {
        
    }

    protected override void FixedUpdate()
    {
        SetAnimation();
    }

    private void SetAnimation() {
        Animator.SetFloat(HorizontalDirection, MoveHorizontal);
        Animator.SetFloat(VerticalDirection, MoveVertical);
    }

    public override void SetPosition(Vector2 position) {
        gameObject.transform.position = position;
    }

    public override void SetDirection(Vector2 direction) {
        MoveHorizontal = direction.x;
        MoveVertical = direction.y;
    }
}