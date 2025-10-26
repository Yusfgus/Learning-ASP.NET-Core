using Shared;
using System.Linq.Expressions;

namespace Linq;

public class ExpressionTree
{
    public static void Compiling()
    {
        Utils.printTitle(title: "ExpressionTree ( Compiling )", color: ConsoleColor.Blue, width: 70);

        Func<int, bool> IsEven = (num) => num % 2 == 0;

        Utils.printTitle("IsEven using Delegate");
        Console.WriteLine($"10 {(IsEven(10) ? "is" : "is't")} even");

        //=============================================================================

        Expression<Func<int, bool>> IsEvenExpression = (num) => num % 2 == 0;
        Func<int, bool> IsEven02 = IsEvenExpression.Compile();

        Utils.printTitle("IsEven using Expression");
        Console.WriteLine($"10 {(IsEven02(10) ? "is" : "is't")} even");
    }

    public static void Decomposing()
    {
        Utils.printTitle(title: "ExpressionTree ( Decomposing )", color: ConsoleColor.Blue, width: 70);

        Expression<Func<int, int, bool>> IsGreaterExpression = (num1, num2) => num1 > num2;

        ParameterExpression num1Param = IsGreaterExpression.Parameters[0];
        ParameterExpression num2Param = IsGreaterExpression.Parameters[1];

        BinaryExpression body = (BinaryExpression)IsGreaterExpression.Body;

        ParameterExpression left = (ParameterExpression)body.Left;
        ParameterExpression right = (ParameterExpression)body.Right;

        ExpressionType operation = body.NodeType;

        Utils.printTitle("Is Greater Expression");
        Console.WriteLine($"({num1Param}, {num2Param}) => {left} {operation} {right}");
    }

    public static void Building()
    {
        Utils.printTitle(title: "ExpressionTree ( Building )", color: ConsoleColor.Blue, width: 70);

        // (num) => num % 2 == 0

        ParameterExpression numParam = Expression.Parameter(typeof(int), "num");  // num

        ConstantExpression twoConst = Expression.Constant(2, typeof(int)); // 2
        ConstantExpression zer0Const = Expression.Constant(0, typeof(int)); // 0

        BinaryExpression moduleExpression = Expression.Modulo(numParam, twoConst); // num % 2

        BinaryExpression IsEvenBinaryExpression = Expression.Equal(moduleExpression, zer0Const); // num % 2 == 0

        ParameterExpression[] parameters = { numParam }; // (num)

        Expression<Func<int, bool>> IsEvenExpression =
                        Expression.Lambda<Func<int, bool>>(IsEvenBinaryExpression, parameters); // (num) => num % 2 == 0
        
        Func<int, bool> IsEven02 = IsEvenExpression.Compile();

        Utils.printTitle("IsEven using Expression");
        Console.WriteLine($"10 {(IsEven02(10) ? "is" : "is't")} even");
    }
}
