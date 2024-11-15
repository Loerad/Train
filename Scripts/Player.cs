using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public const float SPEED = 5.0f;
	public const float JUMPVELOCITY = 4.5f;
	public float lookSensitivity = 0.001f;
	private Camera3D cam;
	private MeshInstance3D body;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
		body = GetChild(1) as MeshInstance3D;
		cam = body.GetChild(0) as Camera3D;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Quit")) //change to activate the pause menu
		{
			Save.SaveGame();
			GetTree().Quit();
		}
	}
	//handles mouse movement
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured)
		{
			body.RotateY(-mouseMotion.Relative.X * lookSensitivity);
			cam.RotateX(-mouseMotion.Relative.Y * lookSensitivity);
			cam.Rotation = Rotation with { X = Math.Clamp(cam.Rotation.X, Mathf.DegToRad(-66.5f) , Mathf.DegToRad(63.4f)) }; //https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_basics.html#common-pitfalls
		}
	}
	//base godot movement script
	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("Jump") && IsOnFloor())
		{
			velocity.Y = JUMPVELOCITY;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("Left", "Right", "Forward", "Backward");
		Vector3 direction = (body.Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized(); //apart from acting on the mesh insead of the origin
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * SPEED;
			velocity.Z = direction.Z * SPEED;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, SPEED);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, SPEED);
		}
		
		Velocity = velocity;
		MoveAndSlide();
		
	}
}
