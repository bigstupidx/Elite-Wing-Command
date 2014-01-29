using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MissionType))]
public class MissionType : Editor
{
	string choice;
	string[] _choices = new [] { "foo", "foobar" };
	int _choiceIndex = 0;
	public string Choice { get { return choice; } set { choice = value; }}

	void OnGUI()
	{
		// Choose an option from the list
		_choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);
		// Update the selected option on the underlying instance of SomeClass
		var missionType = target as MissionType;
		missionType.choice = _choices[_choiceIndex];
	}
}

