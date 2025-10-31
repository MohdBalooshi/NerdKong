NerdKong (English-only) Starter — Step 1 & 2
============================================

Unity: 6000.2.6f2
Goal: Single-player prototype of the quiz flow (offline) with timer + scoring.
Multiplayer and backend will be added next.

Folder layout to place in your project:
- Assets/NerdKong/Scripts/Data/
- Assets/NerdKong/Scripts/Match/SinglePlayer/
- Assets/NerdKong/Scripts/Match/UI/
- Assets/NerdKong/Scripts/Utils/
- Assets/Resources/QuestionPacks/

Included scripts:
- Data/Question.cs
- Data/QuestionPack.cs
- Utils/JsonLoader.cs
- Match/SinglePlayer/SinglePlayerMatchController.cs
- Match/UI/QuestionUI.cs
- Match/UI/AnswerButton.cs

Question data:
- Resources/QuestionPacks/general_knowledge_pack.json

Quick setup in Unity (5–10 minutes):
1) Create a new Scene named 'Match' and save it in Assets/NerdKong/Scenes/ (or anywhere).
2) Add a Canvas (Scale With Screen Size). Inside, create:
   - TMP Text: QuestionText
   - TMP Text: CounterText
   - TMP Text: TimerText
   - TMP Text: ScoreText
   - TMP Text: RevealText
   - Four Buttons (with child TMP Text): AnswerA, AnswerB, AnswerC, AnswerD
3) Add a GameObject 'UI' and attach 'QuestionUI' component.
   - Drag the references:
     - questionText -> QuestionText
     - counterText -> CounterText
     - timerText -> TimerText
     - scoreText -> ScoreText
     - revealText -> RevealText
     - answerButtons -> add 4 entries and assign the 'AnswerButton' component on each button.
       (Each button object should also have the 'AnswerButton' script with fields wired: Button, TMP_Text, Image)
4) Add a GameObject 'Game' and attach 'SinglePlayerMatchController'.
   - Set 'questionUI' to your 'UI' object.
   - Ensure 'questionPackPath' remains 'QuestionPacks/general_knowledge_pack' (default).
5) Press Play. You should see 5 random questions from the pack, 10 seconds each, scoring faster answers higher.

Notes:
- Colors on AnswerButton use simple Color.green/Color.red for clarity. Style later as you like.
- This prototype loads JSON from Resources for simplicity. We'll migrate to Addressables after multiplayer is in.
- Next steps: Photon Fusion rooms, host-driven timers, PlayFab login + leaderboards, and content Addressables.

Have fun!
