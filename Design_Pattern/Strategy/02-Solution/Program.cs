using System;
using System.Threading;
using Shared;

namespace Design_Pattern.Strategy.Solution;

public class Program
{
    public static void Main()
    {
        Utils.printTitle("Design Patter", 80, ConsoleColor.Red);
        Utils.printTitle("Strategy ( Solution )", 70, ConsoleColor.Blue);


        while (true)
        {
            string origin = "Montreal";
            string destination = "New York";

            Console.WriteLine($"{origin} → {destination}");
            Console.Write("Route preference [1] walking, [2] cycling, [3] driving, [4] transit, [5] flights): ");

            if (Enum.TryParse(Console.ReadLine(), out RoutePreference routePreference) &&
                Enum.IsDefined(typeof(RoutePreference), routePreference))
            {
                NavigationContext navigationContext = new NavigationContext(new DrivingNavigationStrategy());

                switch (routePreference)
                {
                    case RoutePreference.Walking:
                        FindFastestRoute();
                        navigationContext.SwitchNavigationStrategy(new WalkingNavigationStrategy());
                        break;
                    case RoutePreference.Cycling:
                        FindFastestRoute();
                        navigationContext.SwitchNavigationStrategy(new CyclingNavigationStrategy());
                        break;
                    case RoutePreference.Driving:
                        FindFastestRoute();
                        navigationContext.SwitchNavigationStrategy(new DrivingNavigationStrategy());
                        break;
                    case RoutePreference.Transit:
                        FindFastestRoute();
                        navigationContext.SwitchNavigationStrategy(new TransitNavigationStrategy());
                        break;
                    case RoutePreference.Flight:
                        FindFastestRoute();
                        navigationContext.SwitchNavigationStrategy(new FlightNavigationStrategy());
                        break;
                    default:
                        break;
                }

                Route route = navigationContext.Navigate(origin, destination);
                Console.WriteLine(route);
            }
            else
            {
                break;
            }
            Console.ReadKey();
            Console.Clear();
        }
    }


    static void FindFastestRoute()
    {
        Console.Clear();
        Console.Write($"Finding fastest route considering distance, weather, and safety");
        Thread.Sleep(750);
        Loading.Spinner(3500);
        Console.Clear();
    }

}