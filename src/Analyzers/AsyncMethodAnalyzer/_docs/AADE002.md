# Diagnostic ID: AADE002

| Property               | Value                                           |
| ---------------------- | ------------------------------------------------|
| Id                     | AADE002                                         |
| Category				 | `Design`										   |
| Title                  | Add missing `CancellationToken` parameter     |
| Severity               | `Info`                                         |
| Official Documentation |                                                 |

<br />

_Method signature:_

```
public async void MyTaskMethodAsync(int a)

public Task MyTaskMethodAsync(int a)
public Task<T> MyTaskTMethodAsync(int a)

public async Task MyTaskMethodAsync(int a)
public async Task<T> MyTaskTMethodAsync(int a)
```

_Expected method signature:_

```
public async void MyTaskMethodAsync(int a, CancellationToken cancellationToken)

public Task MyTaskMethodAsync(int a, CancellationToken cancellationToken)
public Task<T> MyTaskTMethodAsync(int a, CancellationToken cancellationToken)

public async Task MyTaskMethodAsync(int a, CancellationToken cancellationToken)
public async Task<T> MyTaskTMethodAsync(int a, CancellationToken cancellationToken)
```

<br/>

#### Severity

__Info:__

 - `async void` methods
 - `Task` methods
 - `Task<T>` methods
 - `async Task` methods
 - `async Task<T>` methods

#### Code Fixes
Adds `CancellationToken` parameter as last method parameter.