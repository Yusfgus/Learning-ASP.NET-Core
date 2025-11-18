namespace Design_Pattern.FavorCompositionOverInheritance.After;

public class Tomato : ITopping
{
    public string Title => "Tomato";
    public decimal Price => 3m;
}

public class Chicken : ITopping
{
    public string Title => "Chicken";
    public decimal Price => 6m;
}

public class Cheese : ITopping
{
    public string Title => "Cheese";
    public decimal Price => 4m;
}

public class BlackOlive : ITopping
{
    public string Title => "Black Olive";
    public decimal Price => 2m;
}

public class GreenPaper : ITopping
{
    public string Title => "Green Paper";
    public decimal Price => 2.5m;
}

public class Salami : ITopping
{
    public string Title => "Salami";
    public decimal Price => 2.5m;
}