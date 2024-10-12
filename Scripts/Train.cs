using Godot;
using System;

public partial class Train : RigidBody3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//LinearVelocity = LinearVelocity with { X = 1f };
	}

    public override void _PhysicsProcess(double delta)
    {
        ApplyCentralForce(new Vector3(4,0,0));
		
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
