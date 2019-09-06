using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
	// 400x200 px window will apear in the center of the screen.
	private Rect windowRect = new Rect ((Screen.width - 400)/2, (Screen.height - 200)/2, 400, 200);
	// Only show it if needed.
	private bool show = false;
	private int _score = 0;

	void OnGUI () 
	{
		if (show) {
			GUI.backgroundColor = Color.black;
			GUI.color = Color.cyan;
			windowRect = GUI.Window (0, windowRect, DialogWindow, "Sprint Reloaded");
		}
	}

	// This is the actual window.
	void DialogWindow (int windowID)
	{
		float y = windowRect.height - 30;

		GUI.Label(new Rect(5,windowRect.height/2 - 50, windowRect.width - 15, 30), "<color=cyan>You drive the Red car.</color>");
		GUI.Label(new Rect(5,windowRect.height/2 - 30, windowRect.width - 15, 30), "<color=cyan>Use the arrow keys to steer your car around the track.</color>");
		GUI.Label(new Rect(5,windowRect.height/2 - 10, windowRect.width - 15, 30), "<color=cyan>Ram the other cars or use the Ctrl key to fire missiles.</color>");
		GUI.Label(new Rect(5,windowRect.height/2 + 10, windowRect.width - 15, 30), "<color=cyan>Look for health and missile powerups!</color>");

		if(GUI.Button(new Rect((windowRect.width - 150)/2,y, 150, 25), "<color=cyan>Start game</color>"))
		{
			GameLogic.instance.Go();
			show = false;
		}
	}

	// To open the dialogue from outside of the script.
	public void Open()
	{
		show = true;
	}
}