using System;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using System.Collections.Generic;

public enum SpeechCommand
{
	None,
	One,
	Two,
	Three,
	Four,
	Five,
	Next,
	Previous,
	Done,
	Exit
}

public class CommandDetectedEventArgs : EventArgs
{
	public SpeechCommand Command { get; set; }
}

public class KeywordDetector : MonoBehaviour
{
	public GameObject instructionsText;

	public event EventHandler<CommandDetectedEventArgs> CommandDetected;

	#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

	[SerializeField]
	private List<string> m_Keywords;
		
	private KeywordRecognizer m_Recognizer;

	void Start ()
	{
            
		IEnumerable<string> enumerable = Constants.numbers.Union (Constants.nextKeywords).Union (Constants.previousKeywords).Union (Constants.doneKeywords).Union (Constants.exitKeywords);
		m_Keywords = enumerable.ToList ();

		m_Recognizer = new KeywordRecognizer (m_Keywords.ToArray ());
		m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
		m_Recognizer.Start ();
	}

	private void OnPhraseRecognized (PhraseRecognizedEventArgs args)
	{
		StringBuilder builder = new StringBuilder ();
		builder.AppendFormat ("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
		builder.AppendFormat ("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
		builder.AppendFormat ("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
		Debug.Log (builder.ToString ());

		instructionsText.GetComponents<TextMesh> () [0].text = args.text;
			
		CommandDetectedEventArgs cdArgs = new CommandDetectedEventArgs ();
		if (Constants.numbers.Contains (args.text)) {
			if (Constants.numbers [0].Contains (args.text)) {
				cdArgs.Command = SpeechCommand.One;
			} else if (Constants.numbers [1].Contains (args.text)) {
				cdArgs.Command = SpeechCommand.Two;
			} else if (Constants.numbers [2].Contains (args.text)) {
				cdArgs.Command = SpeechCommand.Three;
			} else if (Constants.numbers [3].Contains (args.text)) {
				cdArgs.Command = SpeechCommand.Four;
			} else if (Constants.numbers [4].Contains (args.text)) {
				cdArgs.Command = SpeechCommand.Five;
			}
		} else if (Constants.nextKeywords.Contains (args.text)) {
			cdArgs.Command = SpeechCommand.Next;
		} else if (Constants.previousKeywords.Contains (args.text)) {
			cdArgs.Command = SpeechCommand.Previous;
		} else if (Constants.doneKeywords.Contains (args.text)) {
			cdArgs.Command = SpeechCommand.Done;
		} else if (Constants.exitKeywords.Contains (args.text)) {
			cdArgs.Command = SpeechCommand.Exit;
		}
		
		if (cdArgs.Command != SpeechCommand.None) {
			OnCommandDetected (this, cdArgs);
		}
	}

	protected virtual void OnCommandDetected (object sender, CommandDetectedEventArgs e)
	{
		if (CommandDetected != null)
			CommandDetected (this, e);
	}

	#endif
}