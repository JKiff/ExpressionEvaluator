Calculator project for MSSA (Microsoft Software & Systems Academy)
The assignment itself was much more basic, just operating on two different numbers
I wanted to extend it by allowing the user to enter an expression with parentheses and multiple operations and operands
The Expression class turns an acceptable string into a list of ExpressionMembers (Operators, Numbers, and Groupers i.e. parentheses) 
it follows order of operations and solves the innermost parentheses sets first
As of now, there's no validation, so it will likely break if you give it a string that's not properly formatted or has mismatching parentheses
Only addition, subtraction, multiplication, and divsion operators are implemented
