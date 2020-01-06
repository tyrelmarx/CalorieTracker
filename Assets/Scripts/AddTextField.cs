using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddTextField : MonoBehaviour
{
    //text box to display curent total
    public Text total;
    //inputfield that shows calorie goal
    public InputField targetCalories;

    //used for calculating the total to display
    int finalTotal = 0;

    //used to load the saved data when the applicaiton runs
    void Start()
    {
        //load the saved data
        GetAllTextFromInputFields();
        //run the calculations once to display the current results from the loaded data at the top
        TextTotal();
    }

    //called when the user finishes editing a field in the application
    public void TextTotal()
    {
        finalTotal = NumberParse(targetCalories.text);
        SetAllTextFromInputFields();
        total.text = finalTotal.ToString();
    }

    //used to parse strings into int with error checking
    int NumberParse(string intString)
    {

        int i = 0;
        if (!Int32.TryParse(intString, out i))
        {
            i = 0;
        }
        return i;
    }

    //calculate the total from the text fields and save the data
    void SetAllTextFromInputFields()
    {
        //loop through each inputfield and calculate the total
        foreach (InputField inputField in gameObject.GetComponentsInChildren<InputField>())
        {
            foreach (Text text in inputField.GetComponentsInChildren<Text>())
            {
                PlayerPrefs.SetInt(inputField.name, NumberParse(text.text));
                finalTotal -= NumberParse(text.text);
            }
        }
        PlayerPrefs.SetInt("DailyCalories", NumberParse(targetCalories.text));
    }

    //save all data from text boxes into the PlayerPrefs
    void GetAllTextFromInputFields()
    {
        //loop through each field and save the data
        foreach (InputField inputField in gameObject.GetComponentsInChildren<InputField>())
        {
            if (PlayerPrefs.GetInt(inputField.name) != 0)
            {
                inputField.text = PlayerPrefs.GetInt(inputField.name).ToString();
            }

        }
        targetCalories.text = PlayerPrefs.GetInt("DailyCalories").ToString();
    }

    public void ClearButton()
    {
        foreach (InputField inputField in gameObject.GetComponentsInChildren<InputField>())
        {
            inputField.text = "";
            TextTotal();
        }
    }

    void Update()
    {
        // Make sure user is on Android platform
        if (Application.platform == RuntimePlatform.Android)
        {

            // Check if Back was pressed this frame
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                // Quit the application
                Application.Quit();
            }
        }
    }
}
