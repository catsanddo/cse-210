# Command-Line Calculator

This project provides a command-line based calculator.
You can type math expressions into a REPL environment and the evaluated result will be printed.
If you pass a file as an argument to the program it will interpret it as a single expression and print the result out.

e.g.
```
./FinalProject.exe bf.calc
```

## Unique Features
This is more than just a simple calculator.
It supports features such as variable assignment, control flow, built-in and even user defined functions.
This program provides a full Turin-Complete programming environment.
The example file `bf.calc` provides an implementation of [brainf***](https://esolangs.org/wiki/Brainfuck) which serves as a proof by simulation of its computational class.

The following bf program will print "Hello World!":
```
++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.
```


### Types
The following types are supported:
- doubles
- arrays of doubles
- functions
- built-in functions
- nil

There is no boolean type.
Instead `0`, `nil`, and an empty array are considered falsey.
All other values are truthy.

String literals are supported as arrays of ASCII codes (e.g. `"hello"` becomes `{104, 101, 108, 108, 111}`)
Escape sequences are supported for '\' '"' and LF newlines.

Standard decimal literals are supported (e.g. `123.45` or `.5`).
ASCII code literals are also supported (e.g. `'A'` becomes 65).

Functions are first-class, but they are not closures.
A functions cannot capture values defined outside of their own scope.

They are defined like this:
```
{a b} a + b
```

This defines a function with two parameters, `a` and `b`, which computes their sum.
The body of a function is a single expression of assignment or lower precedence.

### Operators
`and` `or` `until`

`+` `-` `*` `/` `^` `<` `<=` `>` `>=` `==` `!=` `!` `<<` `[]` `()` `?:` `=` `,`

Most of these operators behave as you would expect, but some added clarity follows.

The `^` operator indicates a power operation.
The left hand is raised to the right hand power.
Fractional powers are supported.

All of the traditional boolean operators will evaluate to a `1` for true or a `0` for false.
`and` and `or` posses the same short-circuiting behavior commonly found in other languages.

`<<` is the append operator.
The left and right operands can be a number or an array (e.g. `1 << 2` is valid).
The result is the appended array (e.g. `"123" << 52` becomes `{49, 50, 51, 52}`).

`[]` is the index operator.
It should be fairly familiar syntax.
Arrays use zero-based indexing.
You can mutate an array by assigning to array indexes.

`()` is the call operator zero or more comma-separated arguments can be passed to a function this way.
This is pretty standard syntax.
Arguments are evaluated left to right.

`?:` is the classic ternary operator.
It provides an if-else construct.
They can be nested to provide an if-elseif-else construct.
```
age >= 18 ? (
  can_vote = 1,
  can_drive = 1
) : age >= 16 ? (
  can_vote = 0,
  can_drive = 1
) : (
  can_vote = 0,
  can_drive = 1
)
```

`until` is the only looping construct.
It evaluates its left operand *until* the right operand evaluates to a truthy value.
If the the right operand is initially falsey, the left operand is never evaluated.
This expression evaluates to the final value of the left operand or nil if it is never evaluated.

`=` is the assignment operator.
It evaluates to the new value of the assigned identifier.
Assigning to an index return the entire array, not just the individual element.
Assignment will implicitly declare undefined identifiers.

Every program is a single expression.
In order to combine multiple expressions into a single program, the `,` operator is used.
It has the same behavior as in C; first the left hand is evaluated, then the right hand is evaluated and returned.

#### Precedance table
| Precedence | Operators                   | Fixity  | Associativity |
| ---        | ---                         | ---     | ---           |
| 1          | `[]` `()`                   | postfix | left-to-right |
| 2          | `-` `!`                     | prefix  | left-to-right |
| 3          | `<<`                        | infix   | left-to-right |
| 4          | `^`                         | infix   | right-to-left |
| 5          | `*` `/`                     | infix   | left-to-right |
| 6          | `+` `-`                     | infix   | left-to-right |
| 7          | `<` `<=` `>` `>=` `==` `!=` | infix   | left-to-right |
| 8          | `and` `or`                  | infix   | left-to-right |
| 9          | `?:`                        | mixfix  | right-to-left |
| 10         | `until`                     | infix   | left-to-right |
| 11         | `=`                         | infix   | right-to-left |
| 12         | `,`                         | infix   | left-to-right |

### Scoping Semantics
The program has lexical scoping.
The body expression of a function is the only thing which creates a new scope.
Because all variable declarations are implicit, it is unclear whether a variable name which shadows a global value is a new declaration or an assignment to the global variable.
To eliminate this ambiguity, it is impossible to shadow names in the global space.
A function `A` called by function `B` *is* allowed to shadow names declared in `B` (and always does).

```
global_value = 42

change_it = {} global_value = 0

change_it()
```

At the end of this program, `global_value` will be set to 0 by the call to `function`.

### Built-ins
All of the built-ins are just bound to regular variables in the global namespace.

There is one built-in constant: `pi`.

Built-in functions:
| Name            | Description                                                |
| ---             | ---                                                        |
| `sqrt(n)`       | Takes the square root of `n`                               |
| `abs(n)`        | Takes the absolute value of `n`                            |
| `dim(n)`        | Creates a zero-initialized array of `n` elements           |
| `len(array)`    | Gets the length of `array`                                 |
| `sin(n)`        | Takes the sin of `n`                                       |
| `cos(n)`        | Takes the cos of `n`                                       |
| `tan(n)`        | Takes the tan of `n`                                       |
| `asin(n)`       | Takes the asin of `n`                                      |
| `acos(n)`       | Takes the acos of `n`                                      |
| `atan(n)`       | Takes the atan of `n`                                      |
| `input()`       | Return the ASCII code of one character from standard input |
| `output(n)`     | Output n as an ASCII code or an array of ASCII codes       |
| `quit()`        | Quits the program immediately                              |

## Credits

Design by: Travis Scoville

Implementation by: Michael and Travis Scoville
