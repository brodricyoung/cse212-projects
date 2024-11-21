using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using Microsoft.VisualBasic;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        var normalSet = new HashSet<string>();
        var reverseSet = new HashSet<string>();
        string reverseWord;
        // adds each word in words to a set and the reverse of it into another set
        foreach (string word in words) {
            reverseWord = $"{word[1]}{word[0]}";
            if (word != reverseWord) {
                normalSet.Add(word);
                reverseSet.Add(reverseWord);
            }
        }

        // gets only the duplicates (words that also have a reverse) found in both sets using .Intersect()
        var symmetricSet = normalSet.Intersect(reverseSet).ToHashSet();
        var returnList = new List<string>();
        // creates a list of the strings to return for each of the duplicates found 
        foreach (string word in symmetricSet) {
            reverseWord = $"{word[1]}{word[0]}";
            returnList.Add($"{word} & {reverseWord}");
            symmetricSet.Remove(reverseWord); // removes the reverse word so it doesnt loop through for that one too
        }

        return returnList.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            var degree = fields[3]; // degree is in column 4
            
            if (degrees.ContainsKey(degree)) {
                degrees[degree]++; // increment degree number if found
            }
            else {
                degrees[degree] = 1; // set degree number to 1 if it's the first occurance
            }
        }
        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // puts word1 into a dictionary with each character (excluding spaces) a key and the amount of times it occurs as the value
        Dictionary<char, int> word1Dict = new Dictionary<char, int>();
        foreach(char character in word1.ToLower()) {
            if (character != ' ') {
                if (word1Dict.ContainsKey(character)) {
                    word1Dict[character]++;
                }
                else {
                    word1Dict[character] = 1;
                }
            }
        }

        // puts word2 into a dictionary with each character (excluding spaces) a key and the amount of times it occurs as the value
        Dictionary<char, int> word2Dict = new Dictionary<char, int>();
        foreach(char character in word2.ToLower()) {
            if (character != ' ') {
                if (word2Dict.ContainsKey(character)) {
                    word2Dict[character]++;
                }
                else {
                    word2Dict[character] = 1;
                }
            } 
        }

        // Checks if the two dictionaries match
        foreach(KeyValuePair<char, int> kvp in word1Dict) {
            if (word2Dict.ContainsKey(kvp.Key)) {
                if (word2Dict[kvp.Key] != word1Dict[kvp.Key]) {  
                    return false;
                }
            }
            else {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);


        List<string> returnList = new List<string>();
        // runs through each feature and gets the properties mag and place for each, adding them to the list to be returned
        foreach (Feature feature in featureCollection.Features) {
            double mag = feature.Properties.Mag;
            string place = feature.Properties.Place;
            returnList.Add($"{place} - Mag {mag}");
        }
        return returnList.ToArray();
    }
}