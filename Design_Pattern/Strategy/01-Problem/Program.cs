using System;
using System.Threading;
using Shared;

namespace Design_Pattern.Strategy.Problem;

public class Program
{
    public static void Main()
    {
        Utils.printTitle("Design Patter", 80, ConsoleColor.Red);
        Utils.printTitle("Strategy ( Problem )", 70, ConsoleColor.Blue);

        while (true)
        {
            string origin = "Montreal";
            string destination = "New York";

            Console.WriteLine($"{origin} → {destination}");
            Console.Write("Route preference [1] walking, [2] cycling, [3] driving, [4] transit, [5] flights): ");

            if (Enum.TryParse(Console.ReadLine(), out RoutePreference routePreference) &&
                Enum.IsDefined(typeof(RoutePreference), routePreference))
            {
                Route route;
                switch (routePreference)
                {
                    case RoutePreference.Walking:
                        FindFastestRoute();
                        route = GetWalkingRoute(origin, destination);
                        break;
                    case RoutePreference.Cycling:
                        FindFastestRoute();
                        route = GeCyclingRoute(origin, destination);
                        break;
                    case RoutePreference.Driving:
                        FindFastestRoute();
                        route = GetDrivingRoute(origin, destination);
                        break;
                    case RoutePreference.Transit:
                        FindFastestRoute();
                        route = GetTransitRoute(origin, destination);
                        break;
                    case RoutePreference.Flight:
                        FindFastestRoute();
                        route = GetFlightRoute(origin, destination);
                        break;
                    default:
                        FindFastestRoute();
                        route = GetDrivingRoute(origin, destination);
                        break;
                }
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

    static Route GetWalkingRoute(string origin, string destination)
    {
        // complex process
        return new Route
        {
            Title = "via US-9 N",
            Origin = origin,
            Destination = destination,
            RoutePreference = RoutePreference.Driving,
            DistanceInKM = 601,
            Duration = TimeSpan.FromHours(123).Add(TimeSpan.FromMinutes(0)),
            Directions = new()
            {
                origin,
                "Head northeast on Boulevard René-Lévesque O S/Bd René-Lévesque toward Blvd Robert-Bourassa",
                "Turn right onto Côte du Beaver Hall",
                "Continue onto Rue du Square-Victoria",
                "Turn left onto Rue Saint-Jacques",
                "Turn right onto Pl. d'Armes/Rue Saint-Sulpice Continue to follow Rue Saint-Sulpice",
                "......",
                "Turn right onto QC-221 S Entering New York",
                "....",
                "Turn left onto City Hall Pk Path",
                destination
            }
        };
    }

    static Route GeCyclingRoute(string origin, string destination)
    {
        return new Route
        {
            Title = "via Empire State Trl",
            Origin = origin,
            Destination = destination,
            RoutePreference = RoutePreference.Driving,
            DistanceInKM = 696,
            Duration = TimeSpan.FromHours(37).Add(TimeSpan.FromMinutes(0)),
            Directions = new()
            {
                origin,
                "Head southeast on Blvd Robert-Bourassa toward Boulevard René-Lévesque O/Bd René-Lévesque",
                "Turn left onto Boulevard René-Lévesque O/Bd René-Lévesque",
                "Continue onto Rue du Square-Victoria",
                "......",
                "Turn left onto NY-100 S/NY-133 W/Saw Mill River Rd",
                "....",
                "Slight left toward 12th Ave/Empire State Trl/Hudson River Greenway",
                "Continue onto City Hall Pk Path",
                destination
            }
        };
    }

    static Route GetDrivingRoute(string origin, string destination)
    {
        return new Route
        {
            Title = "via l-87 S",
            Origin = origin,
            Destination = destination,
            RoutePreference = RoutePreference.Driving,
            DistanceInKM = 596,
            Duration = TimeSpan.FromHours(6).Add(TimeSpan.FromMinutes(15)),
            Directions = new()
            {
                origin,
                "Head southeast on Blvd Robert-Bourassa toward Boulevard René-Lévesque O/Bd René-Lévesque [3 min.] (1.1 km)",
                "Follow A 15 S, I-87 S and NJ-17 S to Holland Tunnel in Jersey City, United States [5 hr 43 min.] (590 km)",
                "Continue on Holland Tunnel to your destination in Manhattan, New York [10 min.] (4.6 km)",
                destination
            }
        };
    }

    static Route GetTransitRoute(string origin, string destination)
    {
        return new Route
        {
            Title = "Greyhound",
            Origin = origin,
            Destination = destination,
            RoutePreference = RoutePreference.Driving,
            DistanceInKM = 598,
            Duration = TimeSpan.FromHours(9).Add(TimeSpan.FromMinutes(20)),
            Directions = new()
            {
                origin,
                "Montreal Greyhound",
                "Port Authority Bus Terminal (9 hr 20 min)",
                destination
            }
        };
    }

    static Route GetFlightRoute(string origin, string destination)
    {
        return new Route
        {
            Title = "Montreal QC - NEW York, USA",
            Origin = origin,
            Destination = destination,
            RoutePreference = RoutePreference.Driving,
            DistanceInKM = 538,
            Duration = TimeSpan.FromHours(1).Add(TimeSpan.FromMinutes(20)),
            Directions = new()
            {
                origin,
                "Montréal-Pierre Elliott Trudeau International Airport (YUL)",
                "John F. Kennedy International Airport (JFK)",
                destination
            }
        };
    }

    static void FindFastestRoute()
    {
        Console.Clear();
        Console.Write($"Finding fastest route considering distance, weather, and safety");
        Thread.Sleep(750);
        for(int i=0; i<10; ++i)
        {
            Console.Write('.');
            Thread.Sleep(350);
        }
        Console.Clear();
    }

}