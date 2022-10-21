# Division Calculator 
Calculator that can only divide.  
_Test task_.

## Requirements
- **Input field**
  - Valid characters for input are digits and `/`.
  - Input is invalid if it contains non-valid characters.
  - Input should be saved.
  - Input should be restored on app start.


- **Evaluate button**
  - If **Evaluate button** is clicked and input is valid then print an evaluation result in the **Input field**.
  - Evaluation algorithm should be simple.
  - If **Evaluate button** is clicked and input is invalid then print "Error" in the input field.
  - If **Evaluate button** is clicked and input is invalid then show the **Error popup**.


- **Error popup**:
  - Error popup has a **New equation button**.
    - If the **New equation button** is clicked then clear input field.
    - If the **New equation button** is clicked then close Error popup.
  - Error popup has a **Quit button**.
    - If the **Quit button** is clicked then clear saved input.
    - If the **Quit button** is clicked then close the app.

## Feedback
Mark 4-:
- [X] `ICalculatorPresenter` depends on ISorryDialogPresenter.
- [X] `ICalculatorPresenter` took over part of the logic related to data storage.