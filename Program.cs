using System.Text.Json;

class Program
{

    public List<Recipe> recipes;

    public Program()
    {
        recipes = new List<Recipe>();
    }

    static void Main(string[] args)
    {
        Program program = new Program();
        program.Run(args);
    }

    void Run(string[] args)
    {
        Console.WriteLine("Welcome to the Recipe Manager!");
        Console.WriteLine("#1 to add a recipe\n#2 to remove a recipe\n#3 to modify a recipe\n#4 to view a recipe\n#5 to save a recipe file\n#6 to load a recipe file\n#0 to exit");
        string? input = Console.ReadLine();

        // Input decision statements
        if(input == "0") Console.WriteLine("Exiting the Recipe Manager. Goodbye!");

        else{
            if(input == "1") AddRecipe();

            else if(input == "2") RemoveRecipe();

            else if(input == "3") ModfyRecipe();

            else if(input == "4"){
                Console.WriteLine($"Which recipe would you like to view?");
                for(int i = 0; i < recipes.Count; i++) Console.WriteLine($"{i + 1}: {recipes[i].Name}");
                int recipeIndex = int.Parse(GetInput()) - 1;
                DisplayRecipe(recipes[recipeIndex]);
            }

            else if(input == "5") SaveFile();

            else if(input == "6") LoadFile();

            else Console.WriteLine("Invalid input. Please try again.");

            // Recursion rarrrr
            Run(args);
        }
    }

    // Display a recipe
    void DisplayRecipe(Recipe recipe)
    {
        Console.WriteLine($"Recipe: {recipe.Name}");
        Console.WriteLine("Ingredients:");
        foreach (var ingredient in recipe.Ingredients)
        {
            Console.WriteLine($"- {ingredient}");
        }
        Console.WriteLine("Instructions:");
        Console.WriteLine(recipe.Instructions);
    }

    // Add recipe to list
    void AddRecipe()
    {
        Console.WriteLine("Enter the name of the recipe:");
        string? recipeName = GetInput();

        Console.WriteLine("Enter the ingredients for the recipe (comma separated):");
        List<string> ingredientsInput = GetInput().Split(",").ToList();

        Console.WriteLine("Enter the instructions for the recipe:");
        string? recipeInstructions = GetInput();

        Recipe newRecipe = new Recipe(recipeName, ingredientsInput, recipeInstructions);
        recipes.Add(newRecipe);
    }

    // Remove recipe from list
    void RemoveRecipe()
    {
        for(int i = 0; i < recipes.Count; i++) Console.WriteLine($"{i + 1}: {recipes[i].Name}");
        Console.WriteLine("Enter the number of the recipe to remove:");
        int recipeIndex = int.Parse(GetInput()) - 1;
        recipes.RemoveAt(recipeIndex);
    }

    // Change recipe in list
    void ModfyRecipe()
    {
        for(int i = 0; i < recipes.Count; i++) Console.WriteLine($"{i + 1}: {recipes[i].Name}");
        Console.WriteLine("Enter the number of the recipe to modify:");
        int recipeIndex = int.Parse(GetInput()) - 1;

        Console.WriteLine("Enter the new name of the recipe:");
        string recipeName = GetInput();

        Console.WriteLine("Enter the new ingredients for the recipe (comma separated):");
        List<string> ingredientsInput = GetInput().Split(",").ToList();

        Console.WriteLine("Enter the new instructions for the recipe:");
        string recipeInstructions = GetInput();

        recipes[recipeIndex].Name = recipeName;
        recipes[recipeIndex].Ingredients = ingredientsInput;
        recipes[recipeIndex].Instructions = recipeInstructions;
    }

    // Save recipes to file
    void SaveFile()
    {
        Console.WriteLine("Enter the filename to save the recipes:");
        string filename = GetInput();

        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var recipe in recipes) writer.WriteLine(JsonSerializer.Serialize(recipe));
        }
        Console.WriteLine("Recipes saved successfully.");
    }

    // Load recipes from file
    void LoadFile()
    {
        Console.WriteLine("Enter the filename to load the recipes from:");
        string filename = GetInput();
        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }
        else
        {
            recipes.Clear();
            using (StreamReader reader = new StreamReader(filename))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    Recipe? recipe = JsonSerializer.Deserialize<Recipe>(line);
                    if (recipe != null) recipes.Add(recipe);
                }
            }
            Console.WriteLine("Recipes loaded successfully.");
        }
    }

    // Get user input and handle null inputs so program doesn't break
    string GetInput()
    {
        string? input = Console.ReadLine();
        if (input == null) return "";
        return input;
    }
}