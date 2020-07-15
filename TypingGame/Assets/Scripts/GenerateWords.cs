using UnityEngine;
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
    public GameObject key;
    public GameObject keyBoard;
    public GameObject ChangeLangBtn;
    private GameObject ChangeLangBtn_;
    private string azerty = "azertyuiopqsdfghjklmwxcvbn";
    private string querty = "qwertyuiopasdfghjklmzxcvbn";
    public string selectedLang;
    [SerializeField]
    public static string data1;
    void Start()
    {
        selectedLang = azerty;
        StartCoroutine(newWord());
        instatiateKeyboard();
        SaveSystem.SaveDic();

    }

    // Update is called once per frame

    private void instatiateKeyboard()
    {
        for (int i = 0; i < selectedLang.Length; i++)
        {
            GameObject go = Instantiate(key, keyBoard.transform);
            go.GetComponentInChildren<TextMeshProUGUI>().text = selectedLang.Substring(i, 1);
            addlistenerKeyboards(go, selectedLang.Substring(i, 1));
        }
    }
    public void addlistenerKeyboards(GameObject go, string ch)
    {
        go.GetComponent<Button>().onClick.AddListener(() => onKeyClick(ch));
    }
    public void onKeyClick(string ch)
    {
        if (Words[currentWordId].Substring(currentPlaceInWord, 1) == ch)
        {
            word.transform.GetChild(currentPlaceInWord).GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.green;
            correctChar++;
        }
        else
        {
            word.transform.GetChild(currentPlaceInWord).GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        word.transform.GetChild(currentPlaceInWord).GetChild(0).GetComponent<TextMeshProUGUI>().text = Words[currentWordId].Substring(currentPlaceInWord, 1);
        currentPlaceInWord++;
        if (currentPlaceInWord == Words[currentWordId].Length)
        {
            currentWordId++;
            StartCoroutine(newWord());
        }
    }

    private IEnumerator newWord()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < word.transform.childCount; i++)
        {
            Destroy(word.transform.GetChild(i).gameObject);
        }
        currentPlaceInWord = 0;
        for (int i = 0; i < Words[currentWordId].Length; i++)
        {
            GameObject obj = Instantiate(character, word.transform);
            obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Words[currentWordId].Substring(i, 1);
        }
        
        yield return new WaitForSeconds(2);
        hideChars();
    }
    private void hideChars()
    {
        for (int i = currentPlaceInWord; i < Words[currentWordId].Length; i++)
        {
            word.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
