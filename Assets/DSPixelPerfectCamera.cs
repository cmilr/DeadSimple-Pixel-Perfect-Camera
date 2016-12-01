using UnityEngine;
using UnityEngine.Assertions;

public class DSPixelPerfectCamera : MonoBehaviour
{
	public float FinalUnitSize 		{ get { return finalUnitSize; } }
	public int   PixelsPerUnit 		{ get { return pixelsPerUnit; } }
	public int   VertUnitsOnScreen 	{ get { return verticalUnitsOnScreen; } }

	[SerializeField]
	private int pixelsPerUnit;
	[SerializeField]
	private int verticalUnitsOnScreen;
	private float finalUnitSize;
	private new Camera camera;

	void Awake()
	{
		camera = gameObject.GetComponent<Camera>();
		Assert.IsNotNull(camera);

		SetOrthographicSize(verticalUnitsOnScreen);
	}

	void SetOrthographicSize(int unitsOnScreen)
	{
		// get device's screen height and divide by the number of units 
		// that we want to fit on the screen vertically. this gets us
		// the basic size of a unit on the the current device's screen.
		var tempUnitSize = Screen.height / unitsOnScreen;

		// with a basic rough unit size in-hand, we now round it to the
		// nearest power of pixelsPerUnit (ex; 16px.) this will guarantee
		// our sprites are pixel perfect, as they can now be evenly divided
		// into our final device's screen height.
		finalUnitSize = GetNearestMultiple(tempUnitSize, pixelsPerUnit);

		// ultimately, we are using the standard pixel art formula for 
		// orthographic cameras, but approaching it from the view of:
		// how many standard Unity units do we want to fit on the screen?
		// formula: cameraSize = ScreenHeight / (DesiredSizeOfUnit * 2)
		camera.orthographicSize = Screen.height / (finalUnitSize * 2.0f);
	}

	int GetNearestMultiple(int value, int multiple)
	{
		int rem = value % multiple;
		int result = value - rem;
		if (rem > (multiple / 2))
			result += multiple;

		return result;
	}
}