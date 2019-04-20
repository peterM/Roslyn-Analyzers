![](res/roslyn.png)



<br />



### Analyzers 

<br />

__Support diagnostics (declaration):__
- Analyze if method has `Async` suffix
- Analyze if method has `CancellationToken` parameter
- Analyze if method's `CancellationToken` has name `cancellationToken`
- Analyze if `CancellationToken` is last parameter in method

__Contains CodeFixes (declaration):__
- Rename method name without `Async` suffix
- Add missing `CancellationToken cancellationToken` parameter
- Rename `CancellationToken` parameter to `cancellationToken` when name is not matched
- Reorder method parameters and put `CancellationToken` as last parameter

__Support diagnostics (invocation):__
- Analyze if `invoked` method has `Async` suffix
- Analyze if `invoked` method has `CancellationToken` parameter
  - checks if `CancellationToken` parameter is present in scope where asynchronous method is `invoked` and add possibility to use it
  - add possibility to enhance declaration and use `CancellationToken.None`

__Contains CodeFixes (invocation):__
- Rename method name without `Async` suffix
- Add missing `CancellationToken cancellationToken` parameter
  - Add cancellation token to declaration and invocation (`CancellationToken.None`)
  - Add cancellation token to declaration and invocation (reuse `cancellationToken` from method scope where is invoked)


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

<br/>
<br/>

#### TO DO

- refactorings to remove code duplicity
- improve `Texts`
- add another `analyzers` and `codefixes`
  - reorder `Func<T,....>` generics when `CancellationToken` is detected
  - reorder `Action<T,....>` generics when `CancellationToken` is detected
  - detect and convert `async void` methods to `Task` methods
- improve automatic `simplification`
- add test code :D