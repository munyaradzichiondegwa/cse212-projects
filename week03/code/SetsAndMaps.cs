using System.Text.Json;

public static class SetsAndMaps
{
    // Problem 1: Find Pairs
    public static string[] FindPairs(string[] words)
    {
        var result = new List<string>();
        var seen = new HashSet<string>(words);

        foreach (var word in words)
        {
            if (word[0] == word[1]) continue; // skip like "aa"

            string reversed = new string(new[] { word[1], word[0] });

            if (seen.Contains(reversed))
            {
                result.Add($"{word} & {reversed}");
                seen.Remove(word);
                seen.Remove(reversed);
            }
        }
        return result.ToArray();
    }

    // Problem 2: Summarize Degrees
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(',');
            if (fields.Length > 3)
            {
                string degree = fields[3].Trim();
                if (!degrees.ContainsKey(degree))
                    degrees[degree] = 0;
                degrees[degree]++;
            }
        }
        return degrees;
    }

    // Problem 3: Anagrams
    public static bool IsAnagram(string word1, string word2)
    {
        string Normalize(string s) =>
            new string(s.ToLower().Replace(" ", "").ToCharArray());

        var a = Normalize(word1);
        var b = Normalize(word2);

        if (a.Length != b.Length) return false;

        var dict = new Dictionary<char, int>();

        foreach (char c in a)
        {
            if (!dict.ContainsKey(c)) dict[c] = 0;
            dict[c]++;
        }

        foreach (char c in b)
        {
            if (!dict.ContainsKey(c)) return false;
            dict[c]--;
            if (dict[c] < 0) return false;
        }

        return dict.Values.All(v => v == 0);
    }

    // Problem 5: Earthquake JSON Data
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

        var results = new List<string>();
        if (featureCollection?.Features != null)
        {
            foreach (var f in featureCollection.Features)
            {
                if (f.Properties?.Place != null && f.Properties.Mag.HasValue)
                {
                    results.Add($"{f.Properties.Place} - Mag {f.Properties.Mag.Value}");
                }
            }
        }

        return results.ToArray();
    }
}
