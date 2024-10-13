using Godot;
using System;

public partial class Train : RigidBody3D
{
	// Called when the node enters the scene tree for the first time.
	float velocity = 0;
	float angle = 0;
	const float TURNINGVELOCITY = 1.5f; //the speed of the turn based off the velocity of the train
	public override void _Ready()
	{
		//LinearVelocity = LinearVelocity with { X = 1f };
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionPressed("Train EStop"))
		{
			if (velocity > 0)
			{
				velocity -= 0.2f;
			}
			else if (velocity < 0)
			{
				velocity += 0.2f;
			}
		}
		if (Input.IsActionPressed("Train Forward"))
		{
			velocity += 0.01f;
		}
		else if (Input.IsActionPressed("Train Backward"))
		{
			velocity -= 0.01f;
		}
		if (Input.IsActionPressed("Train Left"))
		{
			if (velocity != 0)
			{
				angle += 0.001f;
			}
		}
		else if (Input.IsActionPressed("Train Right"))
		{
			if (velocity != 0)
			{
				angle -= 0.001f;
			}
		}
		ApplyForce(velocity * Transform.Basis.X);
		RotateY(Mathf.Clamp(angle, -MaxAngle(velocity), MaxAngle(velocity)));
		

	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	private float MaxAngle(float currentVelocity)
	{
		return Mathf.Pi / 6f * Mathf.Pow(TURNINGVELOCITY, -Mathf.Abs(currentVelocity));
	}


}
