using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SoLocalizationLanguageDeleter : EditorWindow {

	void OnGUI()
	{
		GUILayout.Label("All deletions are final.");
		var toDelete = new HashSet<string>() as ICollection<string>;
		foreach(string language in SoLocalizationText.AllLanguages)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label(SoLocalization.GetLanguageDisplayName(language));
			if(GUILayout.Button("Drop"))
			{
				toDelete.Add(language);
			}
			GUILayout.EndHorizontal();
		}
		foreach(var text in SoLocalizationText.AllTexts)
		{
			foreach(var language in toDelete)
			{
				text.DropLanguage(language);
			}
			EditorUtility.SetDirty(text);
		}
	}

	[MenuItem("Tools/Drop Languages")]
	public static void OpenWindow()
	{
		EditorWindow.GetWindow<SoLocalizationLanguageDeleter>();
	}
}
