# Roslyn Analyzers

### Analyzers

#### Asynchronous analyzer

![](res/roslyn.png)

Support diagnostics (declaration): 
- Analyze if method has `Async` suffix
- Analyze if method has `CancellationToken` parameter
- Analyze if method's `CancellationToken` has name `cancellationToken`
- Analyze if `CancellationToken` is last parameter in method

Support diagnostics (invocation): 
- Analyze if `invoked` method has `Async` suffix
- Analyze if method has `CancellationToken` parameter
  - checks if `CancellationToken` parameter is present in scope and report possibility to use it
  - add possibility to enhance declaration and use `CancellationToken.None`

Contains CodeFixes (declaration):
- Rename method name without `Async` suffix
- Add missing `CancellationToken cancellationToken` parameter
- Rename `CancellationToken` parameter to `cancellationToken` when name is not matched
- Reorder method parameters and put `CancellationToken` as last parameter

Contains CodeFixes (invocation):
- Rename method name without `Async` suffix
- Add missing `CancellationToken cancellationToken` parameter
  - Add cancellation token to declaration and invocation (`CancellationToken.None`)
  - Add cancellation token to declaration and invocation (reuse `cancellationToken` from method scope)


##### Analyzed methods

Declaration:
- `async void` method declarations
- `Task` method declarations
- `async Task` method declarations
- `Task<T>` method declarations
- `async Task<T>` method declarations

Invocation: 
- `async void` method invocations
- `Task` method invocations
- `async Task` method invocations
- `Task<T>` method invocations
- `async Task<T>` method invocations

##### Examples `Declaration` vs. `Invocation`

_Example Declaration:_

```
public Task MyMethod() // => Declaration analyzers analyzers will rise diagnostics
{
}
```

_Example Invocation:_

```
public Task MyMethod()
{
      return AnotherAsyncMethod(); // => invocation analyzers analyzers will rise diagnostics
}
```


#### TO DO

- refactorings to remove code duplicity
- improve `Texts`
- add another `analyzers` and `codefixes`
  - reorder `Func<T,....>` generics when `CancellationToken` is detected
  - reorder `Action<T,....>` generics when `CancellationToken` is detected
- improve automatic `simplification`
- add test code :D