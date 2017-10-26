using System;
using UnityEngine;

public class KeyboardInput
{
    public event EventHandler<CommandDetectedEventArgs> CommandDetected;

    // Update is called once per frame
    public void isKeyPressed () {

        CommandDetectedEventArgs cdArgs = new CommandDetectedEventArgs();
        cdArgs.Command = SpeechCommand.None;

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Debug.Log("Right arrow key was released.");
            cdArgs.Command = SpeechCommand.One;
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            Debug.Log("Right arrow key was released.");
            cdArgs.Command = SpeechCommand.Two;
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            Debug.Log("Right arrow key was released.");
            cdArgs.Command = SpeechCommand.Three;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Debug.Log("Right arrow key was released.");
            cdArgs.Command = SpeechCommand.Next;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Debug.Log("Left arrow key was released.");
            cdArgs.Command = SpeechCommand.Previous;
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            Debug.Log("X key was released.");
            cdArgs.Command = SpeechCommand.Exit;
        }
        
        if(Input.GetKeyUp(KeyCode.D))
        {
            Debug.Log("D key was released.");
            cdArgs.Command = SpeechCommand.Done;
        }


        if (cdArgs.Command != SpeechCommand.None)
        {
            OnCommandDetected(this, cdArgs);
        }
    }


    protected virtual void OnCommandDetected(object sender, CommandDetectedEventArgs e)
    {
        if (CommandDetected != null)
            CommandDetected(this, e);
    }
}
