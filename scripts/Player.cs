using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 400;
	private string _currDirection = "down";
    private AnimatedSprite2D _animSprite;

	public override void _Ready()
    {
        // Cache the node reference once at startup (Better performance)
        _animSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }
	public void GetInput()
	{

		Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
		Velocity = inputDirection * Speed;

		if (inputDirection.X > 0)
        {
            _currDirection = "right";
        }
        else if (inputDirection.X < 0)
        {
            _currDirection = "left";
        }
        else if (inputDirection.Y > 0)
        {
            _currDirection = "down";
        }
        else if (inputDirection.Y < 0)
        {
            _currDirection = "up";
        }

        if (inputDirection != Vector2.Zero)
        {
            PlayAnimation(1);
        }
        else
        {
            PlayAnimation(0);
        }
	}

	public void PlayAnimation(int movement)
    {
        _animSprite.FlipH = false;

        switch (_currDirection)
        {
            case "right":
                if (movement == 1) _animSprite.Play("SideWalk");
                else _animSprite.Play("SideIdle");
                break;

            case "left":
                _animSprite.FlipH = true;
                if (movement == 1) _animSprite.Play("SideWalk");
                else _animSprite.Play("SideIdle");
                break;

            case "down":
                if (movement == 1) _animSprite.Play("FrontWalk");
                else _animSprite.Play("DownIdle");
                break;

            case "up":
                if (movement == 1) _animSprite.Play("BackWalk");
                else _animSprite.Play("Idle"); 
                break;
        }
    }

	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
	}
}
