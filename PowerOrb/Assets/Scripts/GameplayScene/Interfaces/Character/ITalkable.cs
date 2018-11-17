using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITalkable {

   void TalkDialogue();
   void TalkDialogue(Dialogue SomeDialogue);
   void TalkDialogue(string name, string sentence, int secondsToWait);
}
