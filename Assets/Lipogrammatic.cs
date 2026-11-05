using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class Lipogrammatic : MonoBehaviour {

   public KMBombInfo Bomb;
   public KMAudio Audio;
   
   public TextMesh Sentence;
   public KMSelectable[] Buttons;
   public AudioClip[] Sounds;

   private List<string> phrases = new List<string>
   {
      {"I'm not sure what these symbols are, but I suspect they have something to do with occult."},
      {"Memory is a fragile thing but so is everything else when a bomb goes off, so pay attention!"},
      {"This seems to be some kind of maze, probably stolen off of a restaurant placemat."},
      {"It is amazing what you can accomplish if you do not care who gets the credit."},
      {"If we knew what we were doing, it wouldn't be called research, would it?"},
      {"Experience is the name everyone gives to their mistakes."},
      {"I never met a man so stupid I could not learn something from him."},
      {"I don't mind making jokes, but I don't want to look like one."},
      {"That's one small step for man; one giant leap for mankind."},
      {"One half of the world cannot understand the pleasures of the other."},
      {"It's not that I'm afraid to die; I just don't want to be there when it happens."},
      {"Not right now! I'm about to be the best ever at this game!"},
      {"All good things must come to an end."},
      {"A journey of a thousand miles begins with a single step."},
      {"If you want something done right, you have to do it yourself."},
      {"Keep your friends close, and your enemies closer."},
      {"On a long enough timeline, the survival rate for everyone will drop to zero."},
      {"Well, here's another nice mess you've gotten me into!"},
      {"How are you gentlemen!! All your base are belong to us."},
      {"You only live once, but if you do it right, once is enough."},
      {"Be the change that you wish to see in the world."},
      {"If you tell the truth, you don't have to remember anything."},
      {"Let us work together to make the future a place we want to visit."},
      {"You have to be odd to be number one."},
      {"Ask, and it shall be given you; seek, and you shall find."},
      {"Welcome to the dangerous and challenging world of bomb defusing."},
      {"One small oversight and it could all be over!"},
      {"Needy modules cannot be disarmed, but pose a recurrent hazard."},
      {"There is only one thing worse than being talked about, and that is not being talked about."},
      {"Just because some of us can read and write and do a little math, that doesn't mean we deserve to conquer the universe."},
      {"Common battery types can be found within enclosures on the sides of the bomb casing."},
      {"To be or not to be; that is the question."},
      {"To err is human; to forgive, divine."},
      {"The greatest wealth is to live content with little."},
      {"Those who dare to fail miserably can achieve greatly."},
      {"Ask not what your country can do for you, but what you can do for your country."},
      {"Whenever you find yourself on the side of the majority, it is time to pause and reflect."},
      {"Be yourself; everyone else is already taken."},
      {"I've failed over and over and over again in my life and that is why I succeed."},
      {"If you think you can do a thing or think you can't do a thing, you're right."},
      {"It always seems impossible until it's done."},
      {"I would like to die on Mars. Just not on impact."},
      {"Too often we enjoy the comfort of opinion without the discomfort of thought."},
      {"Keep Talking and Nobody Explodes, from Wikipedia, the free encyclopedia."},
      {"I came, I saw, I conquered."},
      {"Winners never quit, and quitters never win."},
      {"Insanity: doing the same thing over and over again and expecting different results."},
      {"The unexamined life is not worth living."},
      {"It's not whether you get knocked down; it's whether you get up."},
      {"Turn your wounds into wisdom."},
      {"A man who stands for nothing will fall for anything."},
      {"Stumbling is not falling."},
      {"Where do you want to go today?"},
      {"The ultimate test of ultimate destiny!"},
      {"Pop Quiz! I hope you've been studying."},
      {"Lost in the garbage dump of dead modules, it thought it'd never see the light of day again..."},
      {"Burying your problems won't solve them."},
      {"Don't let the path make you forget where the end is."},
      {"Is it crazy how saying sentences backwards creates backwards sentences saying how crazy it is?"},
      {"Bamboozling button grid."},
      {"Big or small; cube or ball; you only live once, but you die dice."},
      {"The conceptual jump from nothingness to emptiness is huge."},
      {"The average carrot weighs three hundred and seventy-five carats."},
      {"It will never be the future."},
      {"The human species will not go extinct in your lifetime."},
      {"\"There is no word in the English language that\" starts and ends with T."},
      {"Their are three erors in this sentence."},
      {"You can live the entire rest of your life without breathing."},
      {"Where are your fingers?"},
      {"We can resolve this problem by realizing that oranges are apples."},
      {"Watch out! There's a skeleton... inside your body!"},
      {"You always find something in the last place you look."},
      {"There's never time to do it right, but there's always time to do it over."},
      {"Every strike brings you closer to the next solve."},
      {"Explore, Dream, Discover."},
      {"There's no joy in living your whole life on the ground."},
   };

   private List<string> usedLetters = new List<string>
   {
      {"ABCDEGHILMNOPRSTUVWY"},
      {"ABEFGHILMNOPRSTUVWY"},
      {"ABCDEFHIKLMNOPRSTUYZ"},
      {"ACDEFGHILMNOPRSTUWYZ"},
      {"ABCDEFGHIKLNORSTUW"},
      {"ACEGHIKMNOPRSTVXY"},
      {"ACDEFGHILMNOPRSTUV"},
      {"ABDEGIJKLMNOSTUW"},
      {"ADEFGHIKLMNOPRST"},
      {"ACDEFHLNOPRSTUW"},
      {"ABDEFHIJMNOPRSTUW"},
      {"ABEGHIMNORSTUVW"},
      {"ACDEGHILMNOSTU"},
      {"ABDEFGHIJLMNOPRSTUWY"},
      {"ADEFGHILMNORSTUVWY"},
      {"ACDEFIKLMNOPRSUY"},
      {"ADEFGHILMNOPRSTUVWYZ"},
      {"ACEGHILMNORSTUVWY"},
      {"ABEGHLMNORSTUWY"},
      {"BCDEFGHILNORSTUVY"},
      {"ABCDEGHILNORSTUWY"},
      {"ABDEFGHILMNORTUVY"},
      {"ACEFGHIKLMNOPRSTUVW"},
      {"ABDEHMNORTUVY"},
      {"ABDEFGHIKLNOSTUVY"},
      {"ABCDEFGHILMNORSTUW"},
      {"ABCDEGHILMNORSTUV"},
      {"ABCDEHILMNOPRSTUYZ"},
      {"ABDEGHIKLNORSTUWY"},
      {"ABCDEFHIJLMNOQRSTUVW"},
      {"ABCDEFGHILMNOPRSTUWY"},
      {"ABEHINOQRSTU"},
      {"ADEFGHIMNORSTUV"},
      {"ACEGHILNORSTVW"},
      {"ABCDEFGHILMNORSTVWY"},
      {"ABCDFHKNORSTUWY"},
      {"ACDEFHIJLMNOPRSTUVWY"},
      {"ABDEFIKLNORSTUVY"},
      {"ACDEFGHILMNORSTUVWY"},
      {"ACDEFGHIKNORTUY"},
      {"ABDEILMNOPSTUWY"},
      {"ACDEIJKLMNOPRSTUW"},
      {"CDEFGHIJMNOPRSTUWY"},
      {"ABCDEFGHIKLMNOPRSTWXY"},
      {"ACDEIMNOQRSUW"},
      {"ADEINQRSTUVW"},
      {"ACDEFGHILMNOPRSTUVXY"},
      {"ADEFGHILMNORSTUVWX"},
      {"CDEGHIKNOPRSTUWY"},
      {"DIMNORSTUWY"},
      {"ADFGHILMNORSTWY"},
      {"ABFGILMNOSTU"},
      {"ADEGHNORTUWY"},
      {"ADEFHILMNOSTUY"},
      {"BDEGHINOPQSTUVYZ"},
      {"ABDEFGHILMNOPRSTUVY"},
      {"BEGHILMNOPRSTUVWY"},
      {"ADEFGHIKLMNOPRSTUWY"},
      {"ABCDEGHIKNORSTWYZ"},
      {"ABDGILMNORTUZ"},
      {"ABCDEGILMNORSTUVY"},
      {"ACEFGHIJLMNOPRSTU"},
      {"ACDEFGHINORSTUVWY"},
      {"BEFHILNRTUVW"},
      {"ACEFGHILMNOPRSTUWXY"},
      {"ADEGHILNORSTUW"},
      {"ACEHINORST"},
      {"ABCEFGHILNORSTUVWY"},
      {"AEFGHINORSUWY"},
      {"ABCEGHILMNOPRSTVWYZ"},
      {"ABCDEHIKLNORSTUWY"},
      {"ACDEFGHIKLMNOPSTUWY"},
      {"ABDEGHILMNORSTUVWY"},
      {"BCEGHIKLNORSTUVXY"},
      {"ACDEILMOPRSVX"},
      {"DEFGHIJLNORSTUVWY"},
   };

   private char forbiddenLetter;
   private List<int> usedPhrases;
   private List<char> unseenLetters;
   private int generatedPhrases;
   private int selectedLetter;
   private int stage;

   static int ModuleIdCounter = 1;
   int ModuleId;
   private bool ModuleSolved;

   void Awake() {
      ModuleId = ModuleIdCounter++;
      foreach (KMSelectable button in Buttons) {
          button.OnInteract += delegate () { ButtonPress(button); return false; };
      }
   }

   void ButtonPress(KMSelectable button) {
      if (ModuleSolved)
         return;
      for (int i = 0; i < Buttons.Length; i++) {
         if (button == Buttons[i]) {
            Buttons[i].AddInteractionPunch();
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, button.transform);
            StartCoroutine(ButtonPush(i));
            if (i == 0) {
               if (unseenLetters.Count == 1) {
                  Debug.LogFormat("[Lipogrammatic #{0}] Assaying sustaining? Striking and aborting this conundrum.", ModuleId);
                  Debug.LogFormat("[Lipogrammatic #{0}] ⸻⸻⸻⸻⸻⸻⸻⸻⸻⸻⸻⸻", ModuleId); // Line to separate attempts
                  Strike();
               }
               else
                  NewPhrase();
            } else if (i == 1) {
               if (stage == 0) {
                  if (unseenLetters.Count == 1) {
                     Debug.LogFormat("[Lipogrammatic #{0}] Good submit. Advancing to glyph input.", ModuleId);
                     Transition();
                  }
                  else {
                     Debug.LogFormat("[Lipogrammatic #{0}] Trying to submit? Striking and aborting this conundrum.", ModuleId);
                     Debug.LogFormat("[Lipogrammatic #{0}] ⸻⸻⸻⸻⸻⸻⸻⸻⸻⸻⸻⸻", ModuleId); // Line to separate attempts
                     Strike();
                  }
               } else {
                  if (selectedLetter == "ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(unseenLetters[0])) {
                     Debug.LogFormat("[Lipogrammatic #{0}] Found which glyph was missing. Good job!", ModuleId);
                     Solve();
                  } else {
                     Debug.LogFormat("[Lipogrammatic #{0}] Submitting \"{1}\"? Striking and aborting this conundrum.", ModuleId, "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[selectedLetter]);
                     Debug.LogFormat("[Lipogrammatic #{0}] ⸻⸻⸻⸻⸻⸻⸻⸻⸻⸻⸻⸻", ModuleId); // Line to separate attempts
                     Strike();
                  }
               }
            } else {
               if (i == 2) {
                  selectedLetter--;
                  if (selectedLetter == -1)
                     selectedLetter = 25;
               } else {
                  selectedLetter++;
                  selectedLetter %= 26;
               }
               ChangeSentence("ABCDEFGHIJKLMNOPQRSTUVWXYZ"[selectedLetter].ToString());
            }
         }
      }
   }

   void Start() {
      Generate();
   }

   void Generate() {
      Sentence.fontSize = 150;
      Buttons[0].gameObject.SetActive(true);
      Buttons[2].gameObject.SetActive(false);
      Buttons[3].gameObject.SetActive(false);
      stage = 0;
      forbiddenLetter = "ABCDFGHJKLMPQVWXYZ"[Rnd.Range(0, 18)];
      Debug.LogFormat("[Lipogrammatic #{0}] Glyph to avoid using is {1}.", ModuleId, forbiddenLetter);
      usedPhrases = new List<int> {};
      unseenLetters = new List<char> {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
      generatedPhrases = 0;
      NewPhrase();
   }

   void NewPhrase() {
      generatedPhrases++;
      int phraseIndex;
      if (generatedPhrases > 8)
         do phraseIndex = Rnd.Range(0, phrases.Count); while (usedPhrases.Contains(phraseIndex) || usedLetters[phraseIndex].Contains(forbiddenLetter) || unseenLetters.All(l => phrases[phraseIndex].IndexOf(l.ToString(), StringComparison.OrdinalIgnoreCase) == -1));
      else
         do phraseIndex = Rnd.Range(0, phrases.Count); while (usedPhrases.Contains(phraseIndex) || usedLetters[phraseIndex].Contains(forbiddenLetter));
      usedPhrases.Add(phraseIndex);
      ChangeSentence(phrases[phraseIndex]);
      Debug.LogFormat("[Lipogrammatic #{0}] Displaying: \"{1}\"", ModuleId, phrases[phraseIndex]);
      string displayedLetters = usedLetters[phraseIndex];
      for (int i = 0; i < displayedLetters.Length; i++) {
         if (unseenLetters.Contains(displayedLetters[i]))
            unseenLetters.Remove(displayedLetters[i]);
      }
      Debug.LogFormat("[Lipogrammatic #{0}] What stays is: {1}.", ModuleId, unseenLetters.Join(", "));
      if (unseenLetters.Count == 1)
         Debug.LogFormat("[Lipogrammatic #{0}] Only glyph staying is {1}; submitting is important.", ModuleId, unseenLetters[0]);
      else
         Debug.LogFormat("[Lipogrammatic #{0}] This contains {1} glyphs; sustain for now.", ModuleId, unseenLetters.Count);
   }

   void Transition() {
      stage = 1;
      selectedLetter = 0;
      Sentence.text = "A";
      Sentence.fontSize = 500;
      Buttons[0].gameObject.SetActive(false);
      Buttons[2].gameObject.SetActive(true);
      Buttons[3].gameObject.SetActive(true);
   }

   void ChangeSentence(string sentence) {
      if (stage == 0)
         Sentence.fontSize = 150;
      float wrap = 160f;
      Sentence.text = sentence;
      string original = sentence;
      while (getWidth(Sentence) > wrap) {
         string current = Sentence.text;
         List<int> indices = findIndicesOf(current, ' ');
         int i = indices.Count - 1;
         if (i >= 0) {
            while (getWidth(Sentence) > wrap) {
               Sentence.text = current.Substring(0, indices[i]);
               i--;
               if (i < 0)
                  break;
            }
         }
         if (i >= 0) {
            Sentence.text += "\n" + current.Substring(indices[i + 1] + 1);
         } else {
            Sentence.fontSize--;
            Sentence.text = original;
         }
         if (findIndicesOf(Sentence.text, '\n').Count > 3) {
            Sentence.fontSize--;
            Sentence.text = original;
         }
      }
   }

   float getWidth(TextMesh text) {
      return getTextWidth(text.text, text.font, text.fontSize, text.fontStyle) * text.characterSize;
   }

   float getTextWidth(string text, Font font, int fontSize, FontStyle fontStyle) {
      float width = 0;
      foreach (string line in text.Split('\n', '\r')) {
         float lineWidth = 0;
         foreach (char c in line) {
            CharacterInfo characterInfo;
            if (font.GetCharacterInfo(c, out characterInfo, fontSize, fontStyle)) {
               lineWidth += characterInfo.advance;
            }
         }
         if (lineWidth > width)
            width = lineWidth;
      }
      return width * 0.1f;
   }

   List<int> findIndicesOf(string str, char chr) {
      List<int> indices = new List<int>();
      for (int i = 0; i < str.Length; i++) {
         if (str[i] == chr)
            indices.Add(i);
      }
      return indices;
   }

   void Solve() {
      ModuleSolved = true;
      StartCoroutine(SolveSound());
      GetComponent<KMBombModule>().HandlePass();
      Sentence.text = "\u2713";
      Sentence.color = new Color(0f, 1f, 0f, 1f);
   }

   void Strike() {
      Audio.PlaySoundAtTransform("Bad", transform);
      GetComponent<KMBombModule>().HandleStrike();
      Generate();
   }

   IEnumerator ButtonPush(int i) {
      for (int j = 0; j < 5; j++) {
         Buttons[i].transform.localPosition -= new Vector3(0, 0.005f / 5, 0);
         yield return null;
      }
      for (int j = 0; j < 5; j++) {
         Buttons[i].transform.localPosition += new Vector3(0, 0.005f / 5, 0);
         yield return null;
      }
   }

   IEnumerator SolveSound() {
      Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
      yield return new WaitForSeconds(1f);
      Audio.PlaySoundAtTransform("Good", transform);
   }

#pragma warning disable 414
   private readonly string TwitchHelpMessage = @"!{0} sustain [Sustains] | !{0} submit [Submits] | !{0} submit (glyph) [Submits said glyph]";
#pragma warning restore 414

   IEnumerator ProcessTwitchCommand (string Command) {
      Command = Command.Trim().ToLowerInvariant();
      yield return null;
      if (Command == "sustain") {
         Buttons[0].OnInteract();
         yield break;
      }
      if (Command == "submit") {
         if (stage == 0) {
            Buttons[1].OnInteract();
            yield break;
         } else {
            yield return "sendtochaterror You can't simply submit; assign a glyph to your command.";
            yield break;
         }
      }
      if (!Regex.IsMatch(Command, @"^\s*submit\s+[a-z]\s*$")) {
         yield return "sendtochaterror Invalid command.";
         yield break;
      }
      if (stage == 0) {
         yield return "sendtochaterror You can't submit a glyph if a topic is shown.";
         yield break;
      }
      string[] Commands = Command.Split(' ');
      int submitIndex = "abcdefghijklmnopqrstvuwxyz".IndexOf(Commands[1]);
      while (selectedLetter != submitIndex) {
         Buttons[3].OnInteract();
         yield return new WaitForSeconds(.1f);
      }
      Buttons[1].OnInteract();
   }

   IEnumerator TwitchHandleForcedSolve () {
      if (stage == 0) {
         while (unseenLetters.Count > 1) {
            Buttons[0].OnInteract();
            yield return new WaitForSeconds(.5f);
         }
         Buttons[1].OnInteract();
      }
      while (selectedLetter != "ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(forbiddenLetter)) {
         Buttons[3].OnInteract();
         yield return new WaitForSeconds(.1f);
      }
      Buttons[1].OnInteract();
   }
}
