using System.Collections.Generic;
using Lithp.Lex;
using Lithp.SExp;

namespace Lithp
{
    class Program
    {
        private const char StringDelim = '"';
        private const string Space = "\" \"";

        private static readonly string Code =
            $@"
(def finput 10)

(func factorial(params n)
(
    (if (= n 0)
        (return 1)
        (return (* n (call factorial((- n 1))))) 
    )) 
)

(func countdown(params from to)
(
    (print (concat from {StringDelim}---{StringDelim} finput))
    (if (> from to) 
        (call countdown((- from 1) to))
    )

    (print (concat {StringDelim}Unwinding...{StringDelim} from))
))

(func sayHello(params name)
(
    (print {StringDelim}Hello{StringDelim})
))

(call sayHello ())
(print (concat {StringDelim}The factorial of {StringDelim} finput {StringDelim} is {StringDelim} (call factorial(finput))))
(call countdown(10 3))

";

        static void Main()
        {
            var customFunctionTable = new CustomFunctionTable();
            var scopeManager = new ScopeManager(new Scope());
            var sysFuncTable = new SystemFunctionTable(scopeManager, customFunctionTable);

            var lexer = new Lexer(Code.ToCharArray());
            var parser = new Parser(lexer, sysFuncTable);
            //new Interpreter(lexer, new Scope()).Run();

            new Interpreter(parser, scopeManager, sysFuncTable, customFunctionTable).Execute();
        }
    }
}