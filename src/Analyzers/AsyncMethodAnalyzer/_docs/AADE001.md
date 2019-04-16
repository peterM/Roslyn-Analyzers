# Diagnostic ID: AADE001

| Property               | Value                                           |
| ---------------------- | ------------------------------------------------|
| Id                     | AADE001                                         |
| Category				 | `Design`										   |
| Title                  | Reorder `CancellationToken` as last           |
| Severity               | `Info`                                         |
| Official Documentation |                                                 |

<br />

_Method signature:_

```
public async void MyTaskMethodAsync(CancellationToken cancellationToken, int a)

public Task MyTaskMethodAsync(CancellationToken cancellationToken, int a)
public Task<T> MyTaskTMethodAsync(CancellationToken cancellationToken, int a)

public async Task MyTaskMethodAsync(CancellationToken cancellationToken, int a)
public async Task<T> MyTaskTMethodAsync(CancellationToken cancellationToken, int a)
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
Reorder `CancellationToken` parameter as last method parameter.