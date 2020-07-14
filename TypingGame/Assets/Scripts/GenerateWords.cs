﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GenerateWords : MonoBehaviour
{
    public string[] Words= { "hama", "khaled", "nooba" };
    public GameObject character;
    public GameObject word;
    private byte currentWordId = 0;
    private byte currentPlaceInWord = 0;
    private byte correctChar=0;
    void Start()
    {

        nextWord();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (!Input.GetKeyDown(KeyCode.Delete))
            {
                if (Words[currentWordId].Substring(currentPlaceInWord,1)  == Input.inputString)
                {
                    word.transform.GetChild(currentPlaceInWord).GetComponent<Image>().color = Color.green;
                    correctChar++;
                }
                else
                {
                    word.transform.GetChild(currentPlaceInWord).GetComponent<Image>().color = Color.red;
                }
                word.transform.GetChild(currentPlaceInWord).GetChild(0).GetComponent<TextMeshProUGUI>().text = Input.inputString;
                currentPlaceInWord++;
                if ((currentPlaceInWord == Words[currentWordId].Length) && (correctChar == Words[currentWordId].Length))
                {
                    correctChar = 0;
                    currentWordId++;
                    StartCoroutine(newWord());
                }
            }
            else
            {
                if (currentPlaceInWord != 0)
                {
                    currentPlaceInWord--;
                    if (word.transform.GetChild(currentPlaceInWord).GetChild(0).GetComponent<TextMeshProUGUI>().text == Words[currentWordId].Substring(currentPlaceInWord, 1))
                    {
                        correctChar--;
                        Debug.Log(correctChar);
                    }
                    word.transform.GetChild(currentPlaceInWord).GetComponent<Image>().color = Color.gray;
                    word.transform.GetChild(currentPlaceInWord).GetChild(0).GetComponent<TextMeshProUGUI>().text = Words[currentWordId].Substring(currentPlaceInWord, 1);
                    
                }
            }
        }
    }
    private void nextWord()
    {
        for(int i = 0; i < word.transform.childCount; i++)
        {
            Destroy(word.transform.GetChild(i).gameObject);
        }
        currentPlaceInWord = 0;
        for (int i = 0; i < Words[currentWordId].Length; i++)
        {
            GameObject obj = Instantiate(character, word.transform);
            obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Words[currentWordId].Substring(i, 1);
        }
    }
    private IEnumerator newWord()
    {
        yield return new WaitForSeconds(0.5f);
        nextWord();
    }
}
