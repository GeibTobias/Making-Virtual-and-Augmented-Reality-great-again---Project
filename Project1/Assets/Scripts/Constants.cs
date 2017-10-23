using System.Collections;
using System.Collections.Generic;

public class Constants {
    public static readonly List<string> numbers = new List<string>() { "one", "two", "three", "four", "five" };
	public static readonly List<string> doneKeywords = new List<string>() { "ready", "done", "continue", "finished", "finish"};
	public static readonly List<string> nextKeywords = new List<string>() { "next", "next step", "forward", "go forward"};
	public static readonly List<string> previousKeywords = new List<string>() { "previous", "back", "go back", "backward", "go backward"};
	public static readonly List<string> exitKeywords = new List<string>() { "exit", "redo", "quit", "selection", "back to selection", "back to cocktail selection"};
}
