using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWriter : MonoBehaviour
{
    private static TextWriter instance;

    private List<TextWriterSingle> textWriterSingleList;

    private void Awake() {
        instance = this;
        textWriterSingleList = new List<TextWriterSingle>();   
    }

    public static TextWriterSingle AddWriter_Static(TextMeshProUGUI textui, string textToWrite, float timePerCharacter, bool invisibleCharacter, bool removeWriterBeforeAdd){
        if(removeWriterBeforeAdd){
            instance.RemoveWriter(textui);
        }
        return instance.AddWriter(textui, textToWrite, timePerCharacter, invisibleCharacter);
    }

    private TextWriterSingle AddWriter(TextMeshProUGUI textui, string textToWrite, float timePerCharacter, bool invisibleCharacter){
        TextWriterSingle textWriterSingle = new TextWriterSingle(textui, textToWrite, timePerCharacter, invisibleCharacter);
        textWriterSingleList.Add(textWriterSingle);
        return textWriterSingle;
    }

    private void RemoveWriter(TextMeshProUGUI textUI){
        for(int i =0; i < textWriterSingleList.Count; i++){
            if(textWriterSingleList[i].GetUIGUI() == textUI){
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    public static void RemoveWriter_Static(TextMeshProUGUI textUI){
        instance.RemoveWriter(textUI);
    }

    private void Update() {
        for(int i =0; i < textWriterSingleList.Count; i++){
            bool destroyInstance = textWriterSingleList[i].Update();
            if(destroyInstance){
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    

    public class TextWriterSingle{
        private TextMeshProUGUI textui;
        private int characterIndex;
        private string textToWrite;
        private float timePerCharacter;
        private float timer;
        private bool invisibleCharacter;

        public TextWriterSingle(TextMeshProUGUI textui, string textToWrite, float timePerCharacter, bool invisibleCharacter){
            this.textui = textui;
            this.textToWrite = textToWrite;
            this.timePerCharacter = timePerCharacter;
            this.invisibleCharacter = invisibleCharacter;
            characterIndex = 0;
        }
        public bool Update() {
            if(textui != null){
                timer -= Time.deltaTime;
                while(timer <= 0f){
                    timer += timePerCharacter;
                    characterIndex++;
                    string text = textToWrite.Substring(0, characterIndex);
                    if(invisibleCharacter){
                        text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                    }
                    textui.text = text;
                    if(characterIndex >= textToWrite.Length){
                        textui = null;
                        return true;
                    }
                }
            }
            return false;
        }

        public TextMeshProUGUI GetUIGUI(){
            return textui;
        }

        public bool IsActive(){
            return characterIndex < textToWrite.Length;
        }

        public void WriteAllAndDestroy(){
            textui.text = textToWrite;
            characterIndex = textToWrite.Length;
            TextWriter.RemoveWriter_Static(textui);
        }
    }
}
