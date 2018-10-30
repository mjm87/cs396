using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation {

    private List<Conversable> people = new List<Conversable>();
    private List<Statement> thread = new List<Statement>();
    private Conversable founder;

    public Conversation(Conversable founder){
       this.founder = founder;
       people.Add(founder);
    }

    public void Speak(string text, Conversable speaker){

        // add new speech blurb to the conversation thread
        thread.Add( new Statement(text, speaker));

        // notify any listeners (barring the speaker)
        //foreach(Conversable listener in people){
        for(int i = 0; i < people.Count; i++){
            Conversable listener = people[i];
            if(listener != speaker){
                listener.ListenTo(text, speaker, this);
            }
        }
    }

    // Add a person to the conversation
    // i.e a subscribe
    public void Join(Conversable person) {
        people.Add(person);
    }

    // Removing a person from the conversation
    // i.e. an unsubscribe
    public void Leave(Conversable person) {
        people.Remove(person);
    }

    // Grab the thread to read through
    // allows newcomers to 
    public List<Statement> GetThread(){
        return thread;
    }

    public bool HasSpokenIn(Conversable person){
        foreach(Statement stmt in thread){
            if(stmt.GetSpeaker() == person) {
                return true;
            }
        }
        // otherwise this person hasn't spoken yet
        return false;
    }

    // TODO: consider an overloaded Speak(string text, Conversable speaker, Conversable to){}
    //       that clarifies who was the intended recipient of the comment / speech

    // Consider an invite->join style addition approach

}

// a unit of speech like a "text message"
// or "speech blurb" that contains whatever
// was said and whom said it
public class Statement {
    
    private string text;
    private Conversable speaker;
    private System.DateTime timestamp;

    public Statement(string text, Conversable speaker){
        this.text = text;    
        this.speaker = speaker;
        timestamp = System.DateTime.Now;
    }

    public string GetText() { return text; }
    public Conversable GetSpeaker() { return speaker; }
    public System.DateTime GetTimeStamp() { return timestamp; }
}