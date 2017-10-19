using System;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class KeywordScript : MonoBehaviour
{
	public GameObject instructionsText;

	#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

		[SerializeField]
		private string[] m_Keywords = { "one", "two", "three", "four", "five",
			"ready", "done", "continue", "finished",
			"next", "next step", "forward", "go forward",
			"previous", "back", "go back", "backward", "go backward",
			"finish", "finished",
			"exit", "redo", "quit", "selection", "back to selection", "back to cocktail selection"
		};
		
		private KeywordRecognizer m_Recognizer;
		
		void Start()
		{
			m_Recognizer = new KeywordRecognizer(m_Keywords);
			m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
			m_Recognizer.Start();
		}
		
		private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
			builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
			builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
			Debug.Log(builder.ToString());
			
		instructionsText.GetComponents<TextMesh> () [0].text = args.text;
		}

	#endif
}