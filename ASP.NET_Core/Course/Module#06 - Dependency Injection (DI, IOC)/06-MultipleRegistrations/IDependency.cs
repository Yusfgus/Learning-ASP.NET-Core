interface IDependency
{
    string DoSomething();
}

class DependencyV1 : IDependency
{
    public string DoSomething()
    {
        return "Doing something V1";
    }
}

class DependencyV2 : IDependency
{
    public string DoSomething()
    {
        return "Doing something V2";
    }
}