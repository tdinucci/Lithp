# Lithp
This is a toy interpreter for a Lisp-like language.  It's currently a work in progress (and at a very early stage) and will probably never become production ready. 

At the moment it can execute programs like the one shown below.

#### Example Program
```lisp
(def finput 10)

(func factorial (params n)
  (if (= n 0)
      (return 1)
      (return (* n (call factorial((- n 1))))) 
    ) 
)

(func countdown(params from to)
(
    (print from)
    (if (> from to) 
        (call countdown((- from 1) to)))

    (print (concat "Unwinding..." from))
))

(print (concat "The factorial of " finput " is " (call factorial(finput))))
(call countdown(5 1))
```
#### Output
```
The factorial of 10 is 3628800
5
4
3
2
1
Unwinding...1
Unwinding...2
Unwinding...3
Unwinding...4
Unwinding...5
```
