using Lithp.Lex;

namespace Lithp
{
    class Program
    {
        private const char StringDelim = '"';
        private const string Space = "\" \"";

        private static readonly string Code =
            $@"
(def finput 10)

(func factorial (n)
  (if (= n 0)
      (return 1)
      (return (* n (call factorial((- n 1))))) 
    ) 
)

(func countdown(from to)
(
    (print from)
    (if (> from to) 
        (call countdown((- from 1) to)))

    (print (concat {StringDelim}Unwinding...{StringDelim} from))
))

(print (concat {StringDelim}The factorial of {StringDelim} finput {StringDelim} is {StringDelim}(call factorial(finput))))
(call countdown(5 1))
";
       
        static void Main()
        {
            var lexer = new Lexer(Code.ToCharArray());
            new Interpreter(lexer, new Scope()).Run();
        }
    }
}