namespace DnDWorldCreate.Utilities
{
    public static class RandomNameGenerator
    {
        public static string GenerateFantasyName()
        {
            string[] prefixes = { "Ael", "Ar", "Bel", "Cal", "Dae", "El", "Fal", "Gel", "Hel", "Il", "Jul", "Kal", "Lil", "Mor", "Nel", "Ori", "Pael", "Quil", "Ral", "Sar", "Tal", "Ur", "Val", "Wil", "Xal", "Yar", "Zel" };
            string[] infixes = { "an", "ar", "bal", "ban", "bel", "bor", "cal", "dan", "dar", "del", "dor", "el", "fal", "far", "fel", "gan", "gar", "gol", "gor", "lil", "lon", "lum", "mor", "nal", "nor", "rak", "ran", "ril", "ron", "sal", "sar", "sel", "sor", "tan", "tel", "tir", "tor", "val", "van", "var", "ven", "vor", "yal", "yar", "yel", "yil", "yor", "yr" };
            string[] suffixes = { "a", "ae", "ael", "ar", "e", "el", "er", "i", "ian", "iel", "ion", "ir", "o", "on", "or", "u", "ur", "us", "yr" };

            Random random = new Random();
            string prefix = prefixes[random.Next(prefixes.Length)];
            string infix = infixes[random.Next(infixes.Length)];
            string suffix = suffixes[random.Next(suffixes.Length)];

            return $"{prefix}{infix}{suffix}";
        }
        public static string GenerateFantasyFamilyName()
        {
            string[] prefixes = { "Al", "Bar", "Car", "Dun", "Eb", "Fir", "Gor", "Han", "Ir", "Jun", "Kar", "Lar", "Mael", "Nar", "Oc", "Pel", "Quar", "Ras", "San", "Tyr", "Uv", "Vor", "Wol", "Xan", "Yor", "Zum" };
            string[] infixes = { "ast", "bryn", "carn", "dorn", "eth", "fyr", "garn", "holt", "ist", "karn", "lyr", "mor", "nar", "orn", "pryth", "qarn", "ryst", "stern", "tor", "vyr", "wyn", "xarn", "yth", "zorn" };
            string[] suffixes = { "bane", "crest", "dane", "eye", "ford", "glen", "harrow", "keep", "lake", "mont", "ridge", "shire", "storm", "vale", "watch", "wood", "yard" };

            Random random = new Random();
            string prefix = prefixes[random.Next(prefixes.Length)];
            string infix = infixes[random.Next(infixes.Length)];
            string suffix = suffixes[random.Next(suffixes.Length)];

            return $"{prefix}{infix}{suffix}";
        }

        public static string GenerateRandomPersonalityTrait(Func<string, bool> isTraitUsed, int maxTraits)
        {
            string[][] traits = {
    new[] { "kind", "compassionate", "considerate", "warmhearted", "empathetic" },
    new[] { "brave", "fearless", "bold", "courageous", "valiant" },
    new[] { "ambitious", "driven", "motivated", "determined", "aspiring" },
    new[] { "intelligent", "smart", "clever", "wise", "knowledgeable" },
    new[] { "modest", "humble", "unassuming", "down-to-earth", "reserved" },
    new[] { "creative", "imaginative", "innovative", "artistic", "resourceful" },
    new[] { "patient", "calm", "composed", "tolerant", "even-tempered" },
    new[] { "optimistic", "positive", "upbeat", "hopeful", "cheerful" },
    new[] { "sociable", "friendly", "outgoing", "amiable", "gregarious" },
    new[] { "assertive", "confident", "decisive", "self-assured", "strong-willed" },
    new[] { "perceptive", "insightful", "observant", "intuitive", "astute" },
    new[] { "adventurous", "daring", "risk-taking", "audacious", "enterprising" },
    new[] { "meticulous", "detail-oriented", "precise", "thorough", "careful" },
    new[] { "curious", "inquisitive", "questioning", "eager to learn", "open-minded" },
    new[] { "loyal", "faithful", "trustworthy", "reliable", "devoted" },
    new[] { "charismatic", "charming", "captivating", "magnetic", "alluring" }
};

            string[] intensities = { "mildly", "moderately", "very", "extremely", "somewhat", "quite", "exceptionally", "remarkably", "significantly", "notably" };


            Random random = new Random();
            string generatedTrait;
            string selectedTrait;

            int attempts = 0;
            do
            {
                int randomCategory = random.Next(traits.Length);
                int randomTrait = random.Next(traits[randomCategory].Length);

                selectedTrait = traits[randomCategory][randomTrait];
                attempts++;
            } while (isTraitUsed(selectedTrait) && attempts < maxTraits);

            if (attempts >= maxTraits)
            {
                return null;
            }

            int randomIntensity = random.Next(intensities.Length);
            string selectedIntensity = intensities[randomIntensity];

            generatedTrait = $"{selectedIntensity} {selectedTrait}";

            return generatedTrait;
        }
    }
}
