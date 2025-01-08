// Hänga Gubbe - C# Console App

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Spectre.Console;

public interface IGame
{
    void Play();
}

public class Hangman : IGame
{
    private string wordToGuess;
    private HashSet<char> guessedLetters;
    private int remainingAttempts;
    private List<string> guessHistory;

    public Hangman(string word, int attempts)
    {
        wordToGuess = word.ToUpper();
        guessedLetters = new HashSet<char>();
        remainingAttempts = attempts;
        guessHistory = new List<string>();
    }

    public void Play()
    {
        while (remainingAttempts > 0 && !IsWordGuessed())
        {
            Console.Clear();
            DisplayWord();
            Console.WriteLine($"\nRemaining attempts: {remainingAttempts}");
            Console.Write("Guess a letter: ");

            char guessedLetter;
            if (!char.TryParse(Console.ReadLine()?.ToUpper(), out guessedLetter))
            {
                Console.WriteLine("Invalid input. Please enter a single letter.");
                Console.ReadKey();
                continue;
            }

            if (guessedLetters.Contains(guessedLetter))
            {
                Console.WriteLine("You've already guessed that letter. Try another.");
            }
            else
            {
                if (wordToGuess.Contains(guessedLetter))
                {
                    Console.WriteLine($"Good guess! The letter '{guessedLetter}' is in the word.");
                }
                else
                {
                    Console.WriteLine($"The letter '{guessedLetter}' is not in the word.");
                    remainingAttempts--;
                }
                guessedLetters.Add(guessedLetter);
                guessHistory.Add(guessedLetter.ToString());
            }
            Console.ReadKey();
        }

        Console.Clear();
        if (IsWordGuessed())
        {
            Console.WriteLine($"Congratulations! You've guessed the word: {wordToGuess}");
        }
        else
        {
            Console.WriteLine($"Game Over. The word was: {wordToGuess}");
        }
        SaveGameHistory();
    }

    private void DisplayWord()
    {
        foreach (var letter in wordToGuess)
        {
            if (guessedLetters.Contains(letter))
            {
                AnsiConsole.Markup("[green]" + letter + " [/]");
            }
            else
            {
                AnsiConsole.Markup("[red]_ [/]");
            }
        }
        Console.WriteLine();
    }

    private bool IsWordGuessed()
    {
        return wordToGuess.All(letter => guessedLetters.Contains(letter));
    }

    private void SaveGameHistory()
    {
        var history = new { Word = wordToGuess, Guesses = guessHistory, AttemptsLeft = remainingAttempts };
        string json = JsonSerializer.Serialize(history, new JsonSerializerOptions { WriteIndented = true });
        System.IO.File.WriteAllText("game_history.json", json);
        Console.WriteLine("Game history saved to game_history.json.");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        AnsiConsole.Markup("[bold yellow]Welcome to Hangman![/]\n");
        Console.Write("Enter a word to guess (hidden from player): ");

        string wordToGuess = Console.ReadLine();
        Console.Clear();

        Hangman game = new Hangman(wordToGuess, 6);
        game.Play();

        AnsiConsole.Markup("[bold green]Thanks for playing Hangman![/]\n");
    }
}
