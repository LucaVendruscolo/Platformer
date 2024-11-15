using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using dotenv.net;

public class OpenAIController : MonoBehaviour
{
    public TMP_Text motivationalText;
    public string apiKey;
    private OpenAIAPI api;
    private List<ChatMessage> messages;

    // Static variable to hold the last motivational message
    public static string lastMotivationalMessage = "";

    void Start()
    {
        
        if (string.IsNullOrEmpty(apiKey))
        {
            Debug.LogError("API Key is missing! Please provide a valid API Key.");
            return;
        }

        Debug.Log("Initializing OpenAI API with provided API key.");
        api = new OpenAIAPI(apiKey);
        
        InitializeMotivation();
    }

    private void InitializeMotivation()
    {
        messages = new List<ChatMessage> {
            new ChatMessage(ChatMessageRole.System, "You are a coach who makes fun of the player when they die. Provide short, belittling messages that makes fun of the player.")
        };
        
        motivationalText.text = "";
        Debug.Log("Motivation initialized: Ready to provide motivational messages.");
    }

    public async void DisplayMotivationalMessage()
    {
        Debug.Log("Attempting to retrieve a motivational message from OpenAI.");

        messages.Add(new ChatMessage(ChatMessageRole.User, "The player has died. Give a motivational message. Make it 40 characters or less. Make fun of the player whilst doing it."));

        try
        {
            var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
            {
                Model = Model.ChatGPTTurbo,
                Temperature = 0.9,
                MaxTokens = 50,
                Messages = messages
            });

            if (chatResult.Choices != null && chatResult.Choices.Count > 0)
            {
                string responseContent = chatResult.Choices[0].Message.Content;
                motivationalText.text = responseContent;
                
                // Save the message to the static variable
                lastMotivationalMessage = responseContent;

                Debug.Log("Received motivational message: " + responseContent);
            }
            else
            {
                Debug.LogWarning("No choices returned from the API.");
            }

            messages.RemoveAt(messages.Count - 1);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error retrieving motivational message: " + e.Message);
            motivationalText.text = "Could not retrieve message. Please check your API settings.";

            // Use fallback message in case of error
            lastMotivationalMessage = "Could not retrieve message. Please check your API settings.";
        }
    }
}
