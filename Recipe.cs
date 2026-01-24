class Recipe
{
    public string Name { get; set; } = "Default Recipe";
    public List<string> Ingredients { get; set; } = ["Default ingredient1", "Default ingredient2"];
    public string Instructions { get; set; } = "Default instructions";

    public Recipe(string name, List<string> ingredients, string instructions)
    {
        Name = name;
        Ingredients = ingredients;
        Instructions = instructions;
    }

}