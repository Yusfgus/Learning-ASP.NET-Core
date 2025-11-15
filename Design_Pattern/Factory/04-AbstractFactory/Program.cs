using Shared;

namespace Design_Pattern.Factory.AbstractFactory;

class Program
{
    public static void Main(string[] args)
    {
        Utils.printTitle("Design Pattern", width: 80, color: ConsoleColor.Red);
        Utils.printTitle("Factory ( Abstract Factory )", width: 70, color: ConsoleColor.Blue);

        (IAppetizer Appetizer, IMainCourse MainCourse, IDessert Dessert) meal = new();

        int choice;

        Console.Clear();
        Console.WriteLine("Families (Combos)");
        Console.WriteLine($" ├ [1] Our Special");
        Console.WriteLine($" ├ [2] Diet Meal (Low in Carbs / Sugar");

        if (int.TryParse(Console.ReadLine(), out choice))
        {
            IMealFactory mealFactory = null;
            switch (choice)
            {
                case 1:
                    mealFactory = new SpecialComboFactory();
                    break;
                case 2:
                    mealFactory = new DietMealFactory();
                    break;
                default:
                    break;
            }

            meal.Appetizer = mealFactory?.PrepareAppetizer();
            meal.MainCourse = mealFactory?.PrepareMainCourse();
            meal.Dessert = mealFactory?.PrepareDessert();
        }

        meal.Appetizer?.Serve();
        meal.MainCourse?.Serve();
        meal.Dessert?.Serve();
    }

}

