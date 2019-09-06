using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
	// 200x200 px window will apear in the center of the screen.
	private Rect windowRect = new Rect ((Screen.width - 200)/2, (Screen.height - 200)/2, 200, 200);
	// Only show it if needed.
	private bool show = false;
	private int _score = 0;

	void OnGUI () 
	{
		if(show)
			windowRect = GUI.Window (0, windowRect, DialogWindow, "Game Over");
	}

	// This is the actual window.
	void DialogWindow (int windowID)
	{
		float y = windowRect.height - 25;

		var centredStyle = GUI.skin.GetStyle("Label"); 
		centredStyle.alignment = TextAnchor.UpperCenter;
		GUI.Label(new Rect(5,windowRect.height/2 - 10, windowRect.width - 15, 30), "Score:  " + _score, centredStyle);
		GUI.Label(new Rect(5,windowRect.height/2 + 10, windowRect.width - 15, 30), "Play again?", centredStyle);

		if(GUI.Button(new Rect(5,y, windowRect.width/2 - 10, 20), "Restart"))
		{
			SceneManager.LoadScene (0);
			show = false;
		}

		if(GUI.Button(new Rect(windowRect.width/2 + 5,y, windowRect.width/2 - 10, 20), "Exit"))
		{
			Application.Quit();
			show = false;
		}
	}

	// To open the dialogue from outside of the script.
	public void Open(int score)
	{
		_score = score;
		show = true;
	}
}