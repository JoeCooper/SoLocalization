using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[CustomEditor(typeof(SoLocalizationText))]
public class SoLocalizationTextEditor : Editor
{
	private int pendingLanguageIndex;

	public override void OnInspectorGUI()
	{
		var soLocalizationText = (SoLocalizationText) target;

		GUILayout.BeginHorizontal();
		GUILayout.Label("Key: " + soLocalizationText.Key);
		if(GUILayout.Button("Reset"))
		{
			soLocalizationText.ResetKey();
		}
		GUILayout.EndHorizontal();

		// CURRENT EDITOR LANGUAGE //

		string[] supportedLanguages = SoLocalizationText.AllLanguages;
		
		if(supportedLanguages.Length == 0)
		{
			GUILayout.Label("No languages have been added.");
		}
		else if(supportedLanguages.Length == 1)
		{
			SoLocalization.EditorLanguage = supportedLanguages[0];
			GUILayout.Label(string.Format("Editing in \"{0}\"", SoLocalization.GetLanguageDisplayName(SoLocalization.EditorLanguage)));
		}
		else
		{
			int currentEditorLanguageIndex = System.Array.IndexOf<string>(supportedLanguages, SoLocalization.EditorLanguage);
			
			string[] languageDisplayNames = new string[supportedLanguages.Length];
			
			for(int i = 0; i < languageDisplayNames.Length; i++)
			{
				languageDisplayNames[i] = SoLocalization.GetLanguageDisplayName(supportedLanguages[i]);
			}

			EditorGUILayout.BeginHorizontal();
			
			int newEditorLanguageIndex = EditorGUILayout.Popup("Edit in:", currentEditorLanguageIndex, languageDisplayNames);
			
			if(newEditorLanguageIndex != currentEditorLanguageIndex)
			{
				SoLocalization.EditorLanguage = supportedLanguages[newEditorLanguageIndex];
			}

			EditorGUILayout.EndHorizontal();
		}

		// THE TEXT //

		bool canEdit = System.Array.IndexOf(SoLocalization.AllLanguageCodes, SoLocalization.EditorLanguage) != -1;

		if(canEdit)
		{
			EditorStyles.textField.wordWrap = true;

			var sourceText = soLocalizationText.GetText(SoLocalization.EditorLanguage);

			string text = EditorGUILayout.TextArea(sourceText);
			if(sourceText != text)
			{
				soLocalizationText.SetText(SoLocalization.EditorLanguage, text);
				EditorUtility.SetDirty(target);
			}
		}
		
		// LANGUAGE //

		string[] addableLanguages;
		{
			var _addableLanguages = new HashSet<string>(SoLocalization.AllLanguageCodes);
			foreach(var addedLanguage in SoLocalizationText.AllLanguages)
			{
				_addableLanguages.Remove(addedLanguage);
			}
			addableLanguages = new string[_addableLanguages.Count];
			_addableLanguages.CopyTo(addableLanguages);
		}
		if(addableLanguages.Length > 0)
		{
			EditorGUILayout.BeginHorizontal();
			var addableLanguageNames = new string[addableLanguages.Length];
			for(int i = 0; i < addableLanguages.Length; i++)
			{
				addableLanguageNames[i] = SoLocalization.GetLanguageDisplayName(addableLanguages[i]);
			}
			
			pendingLanguageIndex = EditorGUILayout.Popup("Add Language:", pendingLanguageIndex, addableLanguageNames);
			if(GUILayout.Button("Add"))
			{
				string addedLanguage = addableLanguages[pendingLanguageIndex];
				soLocalizationText.SetText(addedLanguage, string.Empty);
				pendingLanguageIndex = 0;
			}
			EditorGUILayout.EndHorizontal();
		}

		EditorGUILayout.LabelField("Comment:");

		string comment = EditorGUILayout.TextArea(soLocalizationText.Comment);
		if(comment != soLocalizationText.Comment)
		{
			soLocalizationText.Comment = comment;
			EditorUtility.SetDirty(target);
		}
	}
	
	[MenuItem("Tools/Clean SoLocalization")]
	public static void Clean()
	{	
		foreach(var text in SoLocalizationText.AllTexts)
		{
			var toRemove = from language in text.Languages where SoLocalization.AllLanguageCodes.Contains(language) == false select language;
			foreach(var key in toRemove)
			{
				text.DropLanguage(key);
			}
			EditorUtility.SetDirty(text);
		}
	}

	[MenuItem("Assets/Create/SoLocalization Text")]
	public static void CreateInstance()
	{
		CreateAsset<SoLocalizationText>();
	}
	
	public static void CreateAsset<T> () where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T> ();
		
		string path = AssetDatabase.GetAssetPath (Selection.activeObject);
		if (path == "") 
		{
			path = "Assets";
		} 
		else if (Path.GetExtension (path) != "") 
		{
			path = path.Replace (Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
		}
		
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "/New " + typeof(T).ToString() + ".asset");
		
		AssetDatabase.CreateAsset (asset, assetPathAndName);
		
		AssetDatabase.SaveAssets ();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
	}
}
