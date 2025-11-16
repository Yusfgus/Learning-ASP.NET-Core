using System;

namespace Design_Pattern.Strategy.Solution;

public class WalkingNavigationStrategy : INavigationStrategy
{
    public Route Navigate(string origin, string destination)
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
}