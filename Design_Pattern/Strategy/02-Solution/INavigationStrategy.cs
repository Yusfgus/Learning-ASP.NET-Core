namespace Design_Pattern.Strategy.Solution;

public interface INavigationStrategy
{
    public Route Navigate(string origin, string destination);
}
