

var builder = WebApplication.CreateBuilder(args);

// add custom route constraint
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("validMonth", typeof(MonthRouteConstraint));
});

var app = builder.Build();

// Integer constraint
app.MapGet("/int/{id:int}", (int id) => $"Integer {id}");

// Boolean constraint
app.MapGet("/bool/{flag:bool}", (bool flag) => $"Boolean {flag}");

// DateTime constraint
app.MapGet("/datetime/{date:datetime}", (DateTime date) => $"DateTime {date}");

// Decimal constraint
app.MapGet("/decimal/{price:decimal}", (decimal price) => $"Decimal {price}");

// Double constraint
app.MapGet("/double/{weight:double}", (double weight) => $"Double: {weight}");

// float constraint
app.MapGet("/float/{weight:float}", (float weight) => $"Float: {weight}");

// Guid constraint
app.MapGet("/guid/{id:guid}", (Guid id) => $"GUID: {id}");
    
// long constraint
app.MapGet("/long/{ticks:long}", (long ticks) => $"Long: {ticks}");

// minlength constraint
app.MapGet("/minlength/{username:minlength(4)}", (string username) => $"MinLength(4): {username}");

// maxlength constraint
app.MapGet("/maxlength/{filename:maxlength(8)}", (string filename) => $"MaxLength(8): {filename}");

// length constraint
app.MapGet("/length/{filename:length(12)}", (string filename) => $"Exact Length(12): {filename}");

// lengthrange constraint
app.MapGet("/lengthrange/{filename:length(8,16)}", (string filename) => $"Length(8-16): {filename}");

// min constraint
app.MapGet("/min/{age:min(18)}", (int age) => $"Min Age(18): {age}");

// max constraint
app.MapGet("/max/{age:max(120)}", (int age) => $"Max Age(120): {age}");

// range constraint
app.MapGet("/range/{age:range(18,120)}", (int age) => $"Range(18-120): {age}");

// alpha constraint (alphabetic letters)
app.MapGet("/alpha/{name:alpha}", (string name) => $"Alpha: {name}");

// regex constraint
app.MapGet("/regex/{ssn:regex(^\\d{{3}}-\\d{{2}}-\\d{{4}}$)}", (string ssn) => $"Regex Match (SSN): {ssn}");

// required constraint
app.MapGet("/required/{name:required}", (string name) => $"Required: {name}");

// custom constraint
app.MapGet("/sales/month/{value:validMonth}", (int value) => $"Month: {value}");

app.Run();

class MonthRouteConstraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        if(!values.TryGetValue(routeKey, out var routeValue))
            return false;

        if(int.TryParse(routeValue?.ToString(), out int month))
            return month >= 1 && month <= 12;

        return false;
    }
}
