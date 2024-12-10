using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class QuizManager : MonoBehaviour {

	public Image questionImg; // Image to display the question
	public Button optionButton1;
	public Button optionButton2;
	public Button optionButton3;
	public AudioClip correctAnswerSound; // Sound for correct answer
	public AudioClip wrongAnswerSound;   // Sound for wrong answer
	private AudioSource audioSource; // AudioSource to play the sound

	// Question Data
	[System.Serializable]
	public class QuizQuestion
	{
		public Sprite questionImage; // The image for the question
		public string correctAnswer; // The correct answer
		public string[] options;     // Options for the question
	}

	private List<QuizQuestion> questions = new List<QuizQuestion>();
	private int currentQuestionIndex = 0;

	void Start()
	{
		audioSource = GetComponent<AudioSource>(); // Ensure AudioSource is attached

		// Debug log to check if audio clips are loaded properly
		if (correctAnswerSound == null)
		{
			Debug.LogError("Correct Answer Sound not assigned!");
		}
		if (wrongAnswerSound == null)
		{
			Debug.LogError("Wrong Answer Sound not assigned!");
		}

		PrepareQuestions(); // Dynamically add questions
		LoadQuestion(); // Load the first question
	}

	void PrepareQuestions()
	{
		// Add questions to the list
		questions.Add(new QuizQuestion
		{
			questionImage = Resources.Load<Sprite>("Images/apple"), // Image for Apple
			correctAnswer = "Apple",
			options = new string[] { "Apple", "Banana", "Strawberry" }
		});

		questions.Add(new QuizQuestion
		{
			questionImage = Resources.Load<Sprite>("Images/banana"), // Image for Banana
			correctAnswer = "Banana",
			options = new string[] { "Strawberry", "Banana", "Mango" }
		});

		questions.Add(new QuizQuestion
		{
			questionImage = Resources.Load<Sprite>("Images/strawberry"), // Image for Strawberry
			correctAnswer = "Strawberry",
			options = new string[] { "Cherry", "Strawberry", "Tomato" }
		});

		questions.Add(new QuizQuestion
		{
			questionImage = Resources.Load<Sprite>("Images/orange"), // Image for Orange
			correctAnswer = "Orange",
			options = new string[] { "Orange", "Mango", "Cherry" }
		});

		questions.Add(new QuizQuestion
		{
			questionImage = Resources.Load<Sprite>("Images/watermelon"), // Image for Watermelon
			correctAnswer = "Watermelon",
			options = new string[] { "Watermelon", "Apple", "Tomato" }
		});

		questions.Add(new QuizQuestion
		{
			questionImage = Resources.Load<Sprite>("Images/cherry"), // Image for Cherry
			correctAnswer = "Cherry",
			options = new string[] { "Cherry", "Apple", "Strawberry" }
		});

		questions.Add(new QuizQuestion
		{
			questionImage = Resources.Load<Sprite>("Images/mango"), // Image for Mango
			correctAnswer = "Mango",
			options = new string[] { "Peach", "Mango", "Orange" }
		});

		questions.Add(new QuizQuestion
		{
			questionImage = Resources.Load<Sprite>("Images/tomato"), // Image for Tomato
			correctAnswer = "Tomato",
			options = new string[] { "Tomato", "Apple", "Cherry" }
		});

		questions.Add(new QuizQuestion
		{
			questionImage = Resources.Load<Sprite>("Images/onion"), // Image for Onion
			correctAnswer = "Onion",
			options = new string[] { "Onion", "Tomato", "Potato" }
		});

	}

	void LoadQuestion()
	{
		Debug.Log("Load");

		if (currentQuestionIndex >= questions.Count)
		{
			SceneManager.LoadScene("QuizScene");
			return;
		}

		QuizQuestion question = questions[currentQuestionIndex];

		// Set the question image
		questionImg.sprite =question.questionImage;
		// Set the options
		optionButton1.GetComponentInChildren<Text>().text = question.options[0]; 
		optionButton2.GetComponentInChildren<Text>().text = question.options[1];
		optionButton3.GetComponentInChildren<Text>().text = question.options[2];
		ResetButtonColors();

	}

	public void CheckAnswer(Text selectedAnswer)
	{
		Debug.Log("Selected Answer: " + selectedAnswer.text); // Log the selected answer
		if (selectedAnswer.text == questions[currentQuestionIndex].correctAnswer)
		{
			Debug.Log("Correct!"); // Log success
			audioSource.PlayOneShot(correctAnswerSound);
			// Highlight correct button (Optional: you can use the button color or background color for highlighting)
			HighlightCorrectButton();
			currentQuestionIndex++;
			Invoke("LoadQuestion", 1.5f); // Load next question after a delay
		}
		else
		{
			Debug.Log("Wrong Answer!");
			audioSource.PlayOneShot(wrongAnswerSound);
			// Play wrong answer sound
			Debug.Log("Wrong Answer2!");
			// Highlight the wrong button with red color
			HighlightWrongButton(selectedAnswer.text);
			Debug.Log("Wrong Answer3!");
			// Wait for 1.5 seconds before loading the next question
			Invoke("LoadQuestion", 1.5f);
			Debug.Log("Wrong Answer4!");
		}
	}
	void ResetButtonColors()
	{
		Debug.Log("RESET");

		// Reset the color of the buttons to the original state (for example, white)
		optionButton1.GetComponent<Image>().color = Color.blue;
		optionButton2.GetComponent<Image>().color = Color.blue;
		optionButton3.GetComponent<Image>().color = Color.blue;
	}
	void HighlightCorrectButton()
	{
		Debug.Log("Correct Button!");

		string correctAnswer = questions[currentQuestionIndex].correctAnswer;

		// Return the button that corresponds to the correct answer
		if (optionButton1.GetComponentInChildren<Text>().text == correctAnswer)
		{
			optionButton1.GetComponent<Image>().color = Color.green;
		}
		else if (optionButton2.GetComponentInChildren<Text>().text == correctAnswer)
		{
			optionButton2.GetComponent<Image>().color = Color.green;
		}
		else
		{
			optionButton3.GetComponent<Image>().color = Color.green;
		}
	}

	void HighlightWrongButton(string wrongAnswer)
	{
		Debug.Log("Wrong Button!");

		// Highlight the wrong answer button in red
		if (optionButton1.GetComponentInChildren<Text>().text == wrongAnswer)
		{
			optionButton1.GetComponent<Image>().color = Color.red;
		}
		else if (optionButton2.GetComponentInChildren<Text>().text == wrongAnswer)
		{
			optionButton2.GetComponent<Image>().color = Color.red;
		}
		else
		{
			optionButton3.GetComponent<Image>().color = Color.red;
		}
	}

}
